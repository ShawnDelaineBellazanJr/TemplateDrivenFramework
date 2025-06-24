using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using StrangeLoopPlatform.Core.Interfaces;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;
using System.Linq;

namespace StrangeLoopPlatform.Infrastructure.Services;

/// <summary>
/// Implementation of INSwagCodeGenerationService using NSwag for OpenAPI to code generation.
/// </summary>
public class NSwagCodeGenerationService : INSwagCodeGenerationService
{
    private readonly ILogger<NSwagCodeGenerationService> _logger;
    private readonly HttpClient _httpClient;

    public NSwagCodeGenerationService(ILogger<NSwagCodeGenerationService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<string> GenerateCSharpClientAsync(string openApiUrl, string namespaceName, string className)
    {
        if (string.IsNullOrWhiteSpace(openApiUrl)) throw new ArgumentNullException(nameof(openApiUrl));
        try
        {
            var document = await OpenApiDocument.FromUrlAsync(openApiUrl);
            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = className,
                CSharpGeneratorSettings = { Namespace = namespaceName }
            };
            var generator = new CSharpClientGenerator(document, settings);
            return generator.GenerateFile();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate C# client from OpenAPI URL: {Url}", openApiUrl);
            throw new InvalidOperationException($"NSwag code generation failed: {ex.Message}", ex);
        }
    }

    public async Task<string> GenerateCSharpClientFromContentAsync(string openApiContent, string namespaceName, string className)
    {
        if (string.IsNullOrWhiteSpace(openApiContent)) throw new ArgumentNullException(nameof(openApiContent));
        try
        {
            var document = await OpenApiDocument.FromJsonAsync(openApiContent);
            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = className,
                CSharpGeneratorSettings = { Namespace = namespaceName }
            };
            var generator = new CSharpClientGenerator(document, settings);
            return generator.GenerateFile();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate C# client from OpenAPI content");
            throw new InvalidOperationException($"NSwag code generation failed: {ex.Message}", ex);
        }
    }

    public async Task<string> GenerateTestCasesAsync(string clientCode, string openApiUrl, string namespaceName)
    {
        if (string.IsNullOrWhiteSpace(clientCode)) throw new ArgumentNullException(nameof(clientCode));
        // Stub: In a real implementation, parse the client code and OpenAPI spec to generate tests
        // For now, return a placeholder
        return $"// Test cases for {namespaceName}\n// TODO: Implement test generation logic.";
    }

    public async Task<string> GenerateServiceImplementationAsync(string openApiUrl, string namespaceName, string serviceName)
    {
        if (string.IsNullOrWhiteSpace(openApiUrl)) throw new ArgumentNullException(nameof(openApiUrl));
        // Stub: In a real implementation, generate both interface and implementation
        // For now, generate the client and wrap it in a service class
        var clientCode = await GenerateCSharpClientAsync(openApiUrl, namespaceName, serviceName + "Client");
        var serviceCode = $"// Service implementation for {serviceName}\n// TODO: Implement service logic wrapping the generated client.";
        return clientCode + "\n" + serviceCode;
    }

    public async Task<CodeGenerationResult> ValidateGeneratedCodeAsync(string generatedCode)
    {
        if (string.IsNullOrWhiteSpace(generatedCode)) throw new ArgumentNullException(nameof(generatedCode));
        // Stub: In a real implementation, use Roslyn to compile and validate code
        // For now, just check if code is non-empty
        var result = new CodeGenerationResult
        {
            IsValid = !string.IsNullOrWhiteSpace(generatedCode),
            GeneratedCode = generatedCode
        };
        if (!result.IsValid)
        {
            result.Errors.Add("Generated code is empty.");
        }
        return result;
    }

    public async Task<string> GenerateCodeWithSettingsAsync(string openApiUrl, NSwagGenerationSettings settings)
    {
        if (string.IsNullOrWhiteSpace(openApiUrl)) throw new ArgumentNullException(nameof(openApiUrl));
        if (settings == null) throw new ArgumentNullException(nameof(settings));
        try
        {
            var document = await OpenApiDocument.FromUrlAsync(openApiUrl);
            var csharpSettings = new CSharpClientGeneratorSettings
            {
                ClassName = settings.ClassName,
                GenerateClientClasses = true,
                GenerateDtoTypes = settings.GenerateDataContracts,
                GenerateExceptionClasses = settings.GenerateExceptionClasses,
                CSharpGeneratorSettings = {
                    Namespace = settings.Namespace,
                    GenerateNullableReferenceTypes = settings.GenerateNullableReferenceTypes
                }
            };
            var generator = new CSharpClientGenerator(document, csharpSettings);
            return generator.GenerateFile();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate code with custom NSwag settings");
            throw new InvalidOperationException($"NSwag code generation failed: {ex.Message}", ex);
        }
    }

    public async Task<NSwagVersionInfo> GetVersionInfoAsync()
    {
        // Stub: NSwag does not provide a direct API for version info at runtime
        // For now, return hardcoded info
        return new NSwagVersionInfo
        {
            Version = typeof(OpenApiDocument).Assembly.GetName().Version?.ToString() ?? "unknown",
            SupportedOpenApiVersions = new List<string> { "2.0", "3.0", "3.1" },
            SupportedLanguages = new List<string> { "CSharp", "TypeScript" },
            Features = new List<string> { "Client generation", "DTO generation", "Exception classes" }
        };
    }

    public async Task<Assembly> GenerateAndCompileAssemblyAsync(string openApiContent, string namespaceName, string className)
    {
        if (string.IsNullOrWhiteSpace(openApiContent)) throw new ArgumentNullException(nameof(openApiContent));
        
        try
        {
            _logger.LogInformation("Generating and compiling assembly from OpenAPI content for {ClassName}", className);

            // 1. Generate C# code from OpenAPI content
            var clientCode = await GenerateCSharpClientFromContentAsync(openApiContent, namespaceName, className);

            // 2. Compile the generated code using Roslyn with proper references
            var compilation = CSharpCompilation.Create(
                assemblyName: $"{className}.dll",
                syntaxTrees: new[] { CSharpSyntaxTree.ParseText(clientCode) },
                references: GetRequiredReferences(),
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );

            // 3. Compile to assembly
            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                var errors = string.Join("\n", result.Diagnostics.Select(d => d.ToString()));
                throw new InvalidOperationException($"Compilation failed:\n{errors}");
            }

            ms.Position = 0;
            var assembly = Assembly.Load(ms.ToArray());

            _logger.LogInformation("Successfully generated and compiled assembly for {ClassName}", className);
            return assembly;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate and compile assembly from OpenAPI content");
            throw new InvalidOperationException($"NSwag assembly generation failed: {ex.Message}", ex);
        }
    }

    public async Task<(Assembly Assembly, byte[] DllBytes)> GenerateAndCompileAssemblyWithBytesAsync(string openApiContent, string namespaceName, string className)
    {
        if (string.IsNullOrWhiteSpace(openApiContent)) throw new ArgumentNullException(nameof(openApiContent));
        
        try
        {
            _logger.LogInformation("Generating and compiling assembly with bytes from OpenAPI content for {ClassName}", className);

            // 1. Generate C# code from OpenAPI content
            var clientCode = await GenerateCSharpClientFromContentAsync(openApiContent, namespaceName, className);

            // 2. Compile the generated code using Roslyn with proper references
            var compilation = CSharpCompilation.Create(
                assemblyName: $"{className}.dll",
                syntaxTrees: new[] { CSharpSyntaxTree.ParseText(clientCode) },
                references: GetRequiredReferences(),
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );

            // 3. Compile to assembly and get bytes
            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                var errors = string.Join("\n", result.Diagnostics.Select(d => d.ToString()));
                throw new InvalidOperationException($"Compilation failed:\n{errors}");
            }

            var dllBytes = ms.ToArray();
            ms.Position = 0;
            var assembly = Assembly.Load(dllBytes);

            _logger.LogInformation("Successfully generated and compiled assembly with bytes for {ClassName}", className);
            return (assembly, dllBytes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate and compile assembly with bytes from OpenAPI content");
            throw new InvalidOperationException($"NSwag assembly generation failed: {ex.Message}", ex);
        }
    }

    private static IEnumerable<MetadataReference> GetRequiredReferences()
    {
        var references = new List<MetadataReference>();

        // Core runtime assemblies
        references.Add(MetadataReference.CreateFromFile(typeof(object).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.AsyncMethodBuilderAttribute).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Collections.Generic.List<>).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Threading.Tasks.Task).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Uri).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Exception).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Attribute).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Text.Encoding).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(HttpClient).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Net.Http.HttpRequestMessage).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Net.HttpStatusCode).Assembly.Location));
        references.Add(MetadataReference.CreateFromFile(typeof(System.Text.Json.JsonSerializer).Assembly.Location));

        // Dynamically resolve and add System.Runtime.dll and other core assemblies
        var runtimeDir = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();
        string[] coreAssemblies = new[]
        {
            "System.Runtime.dll",
            "System.Private.CoreLib.dll",
            "System.Console.dll",
            "System.Linq.dll",
            "System.Linq.Expressions.dll",
            "System.Private.Uri.dll",
            "System.Net.Primitives.dll",
            "System.Private.Xml.dll",
            "System.Private.Xml.Linq.dll",
            "System.ComponentModel.Primitives.dll",
            "System.ComponentModel.TypeConverter.dll",
            "System.ObjectModel.dll",
            "System.IO.dll",
            "System.IO.FileSystem.dll",
            "System.Reflection.dll",
            "System.Reflection.Extensions.dll",
            "System.Reflection.Primitives.dll",
            "System.Threading.dll",
            "System.Threading.Tasks.dll"
        };
        foreach (var asm in coreAssemblies)
        {
            var path = Path.Combine(runtimeDir, asm);
            if (File.Exists(path))
            {
                references.Add(MetadataReference.CreateFromFile(path));
            }
        }

        // Add Newtonsoft.Json if available
        try
        {
            var newtonsoftAssembly = typeof(Newtonsoft.Json.JsonConvert).Assembly;
            references.Add(MetadataReference.CreateFromFile(newtonsoftAssembly.Location));
        }
        catch { }

        // Add System.Runtime.Serialization if available
        try
        {
            var serializationAssembly = typeof(System.Runtime.Serialization.EnumMemberAttribute).Assembly;
            references.Add(MetadataReference.CreateFromFile(serializationAssembly.Location));
        }
        catch { }

        // Add System.IO if available
        try
        {
            var ioAssembly = typeof(System.IO.TextReader).Assembly;
            references.Add(MetadataReference.CreateFromFile(ioAssembly.Location));
        }
        catch { }

        return references;
    }
} 