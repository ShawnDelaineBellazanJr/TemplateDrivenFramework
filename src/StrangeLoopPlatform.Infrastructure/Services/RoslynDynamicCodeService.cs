using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using StrangeLoopPlatform.Core.Interfaces;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace StrangeLoopPlatform.Infrastructure.Services;

/// <summary>
/// Roslyn-based service for dynamic code generation, compilation, and execution
/// </summary>
public class RoslynDynamicCodeService : IDynamicCodeService
{
    private readonly ILogger<RoslynDynamicCodeService> _logger;
    private readonly CompilationMetrics _metrics;
    private readonly Dictionary<string, AssemblyLoadContext> _loadContexts;
    private readonly HashSet<string> _bannedNamespaces;
    private readonly HashSet<string> _bannedTypes;

    public RoslynDynamicCodeService(ILogger<RoslynDynamicCodeService> logger)
    {
        _logger = logger;
        _metrics = new CompilationMetrics();
        _loadContexts = new Dictionary<string, AssemblyLoadContext>();
        
        // Initialize banned namespaces and types for security
        _bannedNamespaces = new HashSet<string>
        {
            "System.IO",
            "System.Net",
            "System.Net.Sockets",
            "System.Diagnostics",
            "System.Reflection",
            "System.Runtime.InteropServices",
            "System.Security",
            "System.Threading",
            "System.Threading.Tasks",
            "Microsoft.Win32",
            "System.Management"
        };

        _bannedTypes = new HashSet<string>
        {
            "System.Console",
            "System.Environment",
            "System.GC",
            "System.Math",
            "System.Random"
        };
    }

    public async Task<CompilationResult> CompileCodeAsync(
        string sourceCode, 
        string assemblyName, 
        IEnumerable<string>? references = null,
        CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;
        _metrics.TotalCompilations++;

        try
        {
            _logger.LogInformation("Starting compilation for assembly: {AssemblyName}", assemblyName);

            // Validate code security first
            var securityResult = await ValidateCodeSecurityAsync(sourceCode);
            if (!securityResult.IsSecure)
            {
                _metrics.FailedCompilations++;
                return new CompilationResult
                {
                    Success = false,
                    ErrorMessage = $"Security validation failed: {securityResult.Recommendation}",
                    CompilationTime = DateTime.UtcNow - startTime
                };
            }

            // Create syntax tree
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = await syntaxTree.GetRootAsync(cancellationToken);

            // Check for compilation errors
            var diagnostics = syntaxTree.GetDiagnostics();
            var errors = diagnostics.Where(d => d.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error).ToList();

            if (errors.Any())
            {
                _metrics.FailedCompilations++;
                return new CompilationResult
                {
                    Success = false,
                    Diagnostics = errors.Select(e => new StrangeLoopPlatform.Core.Interfaces.Diagnostic
                    {
                        Severity = (StrangeLoopPlatform.Core.Interfaces.DiagnosticSeverity)e.Severity,
                        Message = e.GetMessage(),
                        LineNumber = e.Location.GetLineSpan().StartLinePosition.Line + 1,
                        ColumnNumber = e.Location.GetLineSpan().StartLinePosition.Character + 1,
                        Code = e.Id
                    }).ToList(),
                    ErrorMessage = "Compilation failed due to syntax errors",
                    CompilationTime = DateTime.UtcNow - startTime
                };
            }

            // Prepare references
            var metadataReferences = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Task).Assembly.Location)
            };

            // Add custom references
            if (references != null)
            {
                foreach (var reference in references)
                {
                    if (File.Exists(reference))
                    {
                        metadataReferences.Add(MetadataReference.CreateFromFile(reference));
                    }
                }
            }

            // Create compilation
            var compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: metadataReferences,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                    .WithOptimizationLevel(OptimizationLevel.Release)
                    .WithNullableContextOptions(NullableContextOptions.Enable));

            // Compile to memory stream
            using var ms = new MemoryStream();
            var emitResult = compilation.Emit(ms);

            if (!emitResult.Success)
            {
                _metrics.FailedCompilations++;
                return new CompilationResult
                {
                    Success = false,
                    Diagnostics = emitResult.Diagnostics.Select(d => new StrangeLoopPlatform.Core.Interfaces.Diagnostic
                    {
                        Severity = (StrangeLoopPlatform.Core.Interfaces.DiagnosticSeverity)d.Severity,
                        Message = d.GetMessage(),
                        LineNumber = d.Location.GetLineSpan().StartLinePosition.Line + 1,
                        ColumnNumber = d.Location.GetLineSpan().StartLinePosition.Character + 1,
                        Code = d.Id
                    }).ToList(),
                    ErrorMessage = "Compilation failed during emit",
                    CompilationTime = DateTime.UtcNow - startTime
                };
            }

            // Save assembly to temporary file
            var tempPath = Path.GetTempPath();
            var assemblyPath = Path.Combine(tempPath, $"{assemblyName}_{Guid.NewGuid():N}.dll");
            await File.WriteAllBytesAsync(assemblyPath, ms.ToArray(), cancellationToken);

            _metrics.SuccessfulCompilations++;
            var compilationTime = DateTime.UtcNow - startTime;
            _metrics.TotalCompilationTime += compilationTime;

            _logger.LogInformation("Compilation successful for {AssemblyName} in {CompilationTime}ms", 
                assemblyName, compilationTime.TotalMilliseconds);

            return new CompilationResult
            {
                Success = true,
                AssemblyPath = assemblyPath,
                CompilationTime = compilationTime
            };
        }
        catch (Exception ex)
        {
            _metrics.FailedCompilations++;
            _logger.LogError(ex, "Compilation failed for assembly: {AssemblyName}", assemblyName);
            
            return new CompilationResult
            {
                Success = false,
                ErrorMessage = ex.Message,
                CompilationTime = DateTime.UtcNow - startTime
            };
        }
    }

    public async Task<ExecutionResult> ExecuteMethodAsync(
        string assemblyPath,
        string typeName,
        string methodName,
        object[]? parameters = null,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;
        var executionTimeout = timeout ?? TimeSpan.FromSeconds(30);

        try
        {
            _logger.LogInformation("Executing method {MethodName} in type {TypeName} from {AssemblyPath}", 
                methodName, typeName, assemblyPath);

            // Create isolated load context
            var loadContext = new AssemblyLoadContext($"Dynamic_{Guid.NewGuid():N}", isCollectible: true);
            _loadContexts[assemblyPath] = loadContext;

            // Load assembly
            using var fileStream = File.OpenRead(assemblyPath);
            var assembly = loadContext.LoadFromStream(fileStream);

            // Get type and method
            var type = assembly.GetType(typeName);
            if (type == null)
            {
                return new ExecutionResult
                {
                    Success = false,
                    ErrorMessage = $"Type '{typeName}' not found in assembly",
                    ExecutionTime = DateTime.UtcNow - startTime
                };
            }

            var method = type.GetMethod(methodName);
            if (method == null)
            {
                return new ExecutionResult
                {
                    Success = false,
                    ErrorMessage = $"Method '{methodName}' not found in type '{typeName}'",
                    ExecutionTime = DateTime.UtcNow - startTime
                };
            }

            // Create instance if method is not static
            object? instance = null;
            if (!method.IsStatic)
            {
                instance = Activator.CreateInstance(type);
            }

            // Execute method with timeout
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(executionTimeout);

            var memoryBefore = GC.GetTotalMemory(false);
            var task = Task.Run(() => method.Invoke(instance, parameters), cts.Token);
            
            var result = await task;
            var memoryAfter = GC.GetTotalMemory(false);

            var executionTime = DateTime.UtcNow - startTime;
            var memoryUsage = memoryAfter - memoryBefore;

            _logger.LogInformation("Method execution completed in {ExecutionTime}ms with memory usage {MemoryUsage} bytes", 
                executionTime.TotalMilliseconds, memoryUsage);

            return new ExecutionResult
            {
                Success = true,
                ReturnValue = result,
                Output = result?.ToString(),
                ExecutionTime = executionTime,
                MemoryUsage = memoryUsage
            };
        }
        catch (OperationCanceledException)
        {
            return new ExecutionResult
            {
                Success = false,
                ErrorMessage = "Method execution timed out",
                ExecutionTime = DateTime.UtcNow - startTime,
                TimedOut = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Method execution failed");
            return new ExecutionResult
            {
                Success = false,
                ErrorMessage = ex.Message,
                ExecutionTime = DateTime.UtcNow - startTime
            };
        }
    }

    public async Task<SecurityValidationResult> ValidateCodeSecurityAsync(string sourceCode)
    {
        try
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = await syntaxTree.GetRootAsync();
            var issues = new List<SecurityIssue>();

            // Check for banned namespaces
            var usingDirectives = root.DescendantNodes().OfType<UsingDirectiveSyntax>();
            foreach (var usingDirective in usingDirectives)
            {
                var namespaceName = usingDirective.Name?.ToString();
                if (!string.IsNullOrEmpty(namespaceName) && _bannedNamespaces.Contains(namespaceName))
                {
                    issues.Add(new SecurityIssue
                    {
                        Type = SecurityIssueType.NetworkAccess,
                        Description = $"Banned namespace used: {namespaceName}",
                        LineNumber = usingDirective.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                        Code = usingDirective.ToString(),
                        Severity = SecuritySeverity.High
                    });
                }
            }

            // Check for banned types
            var identifierNames = root.DescendantNodes().OfType<IdentifierNameSyntax>();
            foreach (var identifier in identifierNames)
            {
                var typeName = identifier.Identifier.ValueText;
                if (_bannedTypes.Contains(typeName))
                {
                    issues.Add(new SecurityIssue
                    {
                        Type = SecurityIssueType.DynamicCodeExecution,
                        Description = $"Banned type used: {typeName}",
                        LineNumber = identifier.GetLocation().GetLineSpan().StartLinePosition.Line + 1,
                        Code = identifier.ToString(),
                        Severity = SecuritySeverity.Medium
                    });
                }
            }

            // Check for potential infinite loops
            var whileStatements = root.DescendantNodes().OfType<WhileStatementSyntax>();
            var forStatements = root.DescendantNodes().OfType<ForStatementSyntax>();
            var foreachStatements = root.DescendantNodes().OfType<ForEachStatementSyntax>();

            if (whileStatements.Any() || forStatements.Any() || foreachStatements.Any())
            {
                issues.Add(new SecurityIssue
                {
                    Type = SecurityIssueType.InfiniteLoop,
                    Description = "Loop constructs detected - potential infinite loop risk",
                    LineNumber = 0,
                    Code = "Loop detection",
                    Severity = SecuritySeverity.Low
                });
            }

            // Determine security level
            var securityLevel = issues.Any(i => i.Severity == SecuritySeverity.Critical) ? SecurityLevel.Unsafe :
                               issues.Any(i => i.Severity == SecuritySeverity.High) ? SecurityLevel.HighRisk :
                               issues.Any(i => i.Severity == SecuritySeverity.Medium) ? SecurityLevel.MediumRisk :
                               issues.Any(i => i.Severity == SecuritySeverity.Low) ? SecurityLevel.LowRisk :
                               SecurityLevel.Safe;

            return new SecurityValidationResult
            {
                IsSecure = securityLevel == SecurityLevel.Safe || securityLevel == SecurityLevel.LowRisk,
                Issues = issues,
                SecurityLevel = securityLevel,
                Recommendation = issues.Any() ? 
                    $"Code contains {issues.Count} security issues. Review and address before execution." : 
                    "Code passed security validation."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Security validation failed");
            return new SecurityValidationResult
            {
                IsSecure = false,
                Issues = new List<SecurityIssue>
                {
                    new SecurityIssue
                    {
                        Type = SecurityIssueType.DynamicCodeExecution,
                        Description = "Security validation failed: " + ex.Message,
                        Severity = SecuritySeverity.High
                    }
                },
                SecurityLevel = SecurityLevel.Unsafe,
                Recommendation = "Security validation failed - do not execute this code."
            };
        }
    }

    public async Task<string> GenerateTestCasesAsync(string sourceCode, string functionName)
    {
        try
        {
            // Simple test case generation based on function signature
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = await syntaxTree.GetRootAsync();

            var methodDeclaration = root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(m => m.Identifier.ValueText == functionName);

            if (methodDeclaration == null)
            {
                return "// Function not found in source code";
            }

            var parameters = methodDeclaration.ParameterList.Parameters;
            var returnType = methodDeclaration.ReturnType.ToString();

            var testCode = new StringBuilder();
            testCode.AppendLine("using System;");
            testCode.AppendLine("using System.Collections.Generic;");
            testCode.AppendLine("using System.Linq;");
            testCode.AppendLine();
            testCode.AppendLine("public class GeneratedTests");
            testCode.AppendLine("{");

            // Generate test method
            testCode.AppendLine($"    public static bool Test{functionName}()");
            testCode.AppendLine("    {");
            testCode.AppendLine("        try");
            testCode.AppendLine("        {");

            // Generate parameter values based on type
            var paramValues = new List<string>();
            foreach (var param in parameters)
            {
                var paramType = param.Type?.ToString() ?? "object";
                var paramName = param.Identifier.ValueText;
                
                var defaultValue = paramType switch
                {
                    "int" => "0",
                    "string" => "\"test\"",
                    "bool" => "true",
                    "double" => "0.0",
                    "float" => "0.0f",
                    "long" => "0L",
                    "decimal" => "0.0m",
                    _ => "null"
                };

                paramValues.Add(defaultValue);
                testCode.AppendLine($"            var {paramName} = {defaultValue};");
            }

            // Generate function call
            var paramString = string.Join(", ", paramValues);
            testCode.AppendLine($"            var result = {functionName}({paramString});");
            testCode.AppendLine("            Console.WriteLine($\"Test result: {result}\");");
            testCode.AppendLine("            return true;");
            testCode.AppendLine("        }");
            testCode.AppendLine("        catch (Exception ex)");
            testCode.AppendLine("        {");
            testCode.AppendLine("            Console.WriteLine($\"Test failed: {ex.Message}\");");
            testCode.AppendLine("            return false;");
            testCode.AppendLine("        }");
            testCode.AppendLine("    }");
            testCode.AppendLine("}");

            return testCode.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate test cases for function: {FunctionName}", functionName);
            return $"// Error generating test cases: {ex.Message}";
        }
    }

    public async Task<bool> UnloadAssemblyAsync(string assemblyPath)
    {
        try
        {
            await Task.CompletedTask;
            if (_loadContexts.TryGetValue(assemblyPath, out var loadContext))
            {
                loadContext.Unload();
                _loadContexts.Remove(assemblyPath);
                
                // Force garbage collection to unload the assembly
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                _logger.LogInformation("Assembly unloaded: {AssemblyPath}", assemblyPath);
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to unload assembly: {AssemblyPath}", assemblyPath);
            return false;
        }
    }

    public CompilationMetrics GetCompilationMetrics()
    {
        if (_metrics.TotalCompilations > 0)
        {
            _metrics.AverageCompilationTime = TimeSpan.FromTicks(_metrics.TotalCompilationTime.Ticks / _metrics.TotalCompilations);
        }

        return _metrics;
    }
} 