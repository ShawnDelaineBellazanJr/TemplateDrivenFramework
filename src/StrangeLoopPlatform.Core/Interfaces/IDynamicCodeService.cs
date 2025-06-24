using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Service for dynamic code generation, compilation, and execution using Roslyn
/// </summary>
public interface IDynamicCodeService
{
    /// <summary>
    /// Compiles C# code string into an executable assembly
    /// </summary>
    /// <param name="sourceCode">C# source code to compile</param>
    /// <param name="assemblyName">Name for the generated assembly</param>
    /// <param name="references">Additional assembly references</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Compilation result with success status and diagnostics</returns>
    Task<CompilationResult> CompileCodeAsync(
        string sourceCode, 
        string assemblyName, 
        IEnumerable<string>? references = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a method from a compiled assembly
    /// </summary>
    /// <param name="assemblyPath">Path to the compiled assembly</param>
    /// <param name="typeName">Full name of the type containing the method</param>
    /// <param name="methodName">Name of the method to execute</param>
    /// <param name="parameters">Parameters to pass to the method</param>
    /// <param name="timeout">Execution timeout</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Execution result with output and execution time</returns>
    Task<ExecutionResult> ExecuteMethodAsync(
        string assemblyPath,
        string typeName,
        string methodName,
        object[]? parameters = null,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Validates code for security and safety
    /// </summary>
    /// <param name="sourceCode">Source code to validate</param>
    /// <returns>Validation result with security assessment</returns>
    Task<SecurityValidationResult> ValidateCodeSecurityAsync(string sourceCode);

    /// <summary>
    /// Generates test cases for the given code
    /// </summary>
    /// <param name="sourceCode">Source code to generate tests for</param>
    /// <param name="functionName">Name of the function to test</param>
    /// <returns>Generated test cases</returns>
    Task<string> GenerateTestCasesAsync(string sourceCode, string functionName);

    /// <summary>
    /// Unloads an assembly to free memory
    /// </summary>
    /// <param name="assemblyPath">Path to the assembly to unload</param>
    /// <returns>True if unloaded successfully</returns>
    Task<bool> UnloadAssemblyAsync(string assemblyPath);

    /// <summary>
    /// Gets compilation statistics and performance metrics
    /// </summary>
    /// <returns>Compilation metrics</returns>
    CompilationMetrics GetCompilationMetrics();
}

/// <summary>
/// Result of a code compilation operation
/// </summary>
public class CompilationResult
{
    public bool Success { get; set; }
    public string? AssemblyPath { get; set; }
    public List<Diagnostic> Diagnostics { get; set; } = new();
    public TimeSpan CompilationTime { get; set; }
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Result of a method execution operation
/// </summary>
public class ExecutionResult
{
    public bool Success { get; set; }
    public object? ReturnValue { get; set; }
    public string? Output { get; set; }
    public string? ErrorMessage { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public long MemoryUsage { get; set; }
    public bool TimedOut { get; set; }
}

/// <summary>
/// Result of security validation
/// </summary>
public class SecurityValidationResult
{
    public bool IsSecure { get; set; }
    public List<SecurityIssue> Issues { get; set; } = new();
    public SecurityLevel SecurityLevel { get; set; }
    public string? Recommendation { get; set; }
}

/// <summary>
/// Security issue found during validation
/// </summary>
public class SecurityIssue
{
    public SecurityIssueType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public int LineNumber { get; set; }
    public string Code { get; set; } = string.Empty;
    public SecuritySeverity Severity { get; set; }
}

/// <summary>
/// Compilation diagnostic information
/// </summary>
public class Diagnostic
{
    public DiagnosticSeverity Severity { get; set; }
    public string Message { get; set; } = string.Empty;
    public int LineNumber { get; set; }
    public int ColumnNumber { get; set; }
    public string Code { get; set; } = string.Empty;
}

/// <summary>
/// Compilation performance metrics
/// </summary>
public class CompilationMetrics
{
    public int TotalCompilations { get; set; }
    public int SuccessfulCompilations { get; set; }
    public int FailedCompilations { get; set; }
    public TimeSpan AverageCompilationTime { get; set; }
    public TimeSpan TotalCompilationTime { get; set; }
    public long TotalMemoryUsage { get; set; }
    public Dictionary<string, int> ErrorTypes { get; set; } = new();
}

/// <summary>
/// Security issue types
/// </summary>
public enum SecurityIssueType
{
    FileSystemAccess,
    NetworkAccess,
    ReflectionUsage,
    UnsafeCode,
    PInvokeUsage,
    DynamicCodeExecution,
    ResourceExhaustion,
    InfiniteLoop
}

/// <summary>
/// Security severity levels
/// </summary>
public enum SecuritySeverity
{
    Low,
    Medium,
    High,
    Critical
}

/// <summary>
/// Security levels for code validation
/// </summary>
public enum SecurityLevel
{
    Safe,
    LowRisk,
    MediumRisk,
    HighRisk,
    Unsafe
}

/// <summary>
/// Diagnostic severity levels
/// </summary>
public enum DiagnosticSeverity
{
    Hidden,
    Info,
    Warning,
    Error
} 