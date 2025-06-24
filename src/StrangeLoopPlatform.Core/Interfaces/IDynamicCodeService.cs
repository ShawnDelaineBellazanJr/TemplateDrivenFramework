using StrangeLoopPlatform.Core.Models;
using Microsoft.CodeAnalysis;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// CRITICAL INTERFACE: Service for dynamic C# code compilation and execution using Roslyn.
/// 
/// This interface is fundamental to the self-evolving AI architecture and enables:
/// - Runtime C# code compilation and execution
/// - Dynamic code generation and testing
/// - Secure code execution with sandboxing
/// - Self-modifying code capabilities
/// 
/// DO NOT DELETE: This interface is essential for the research plan implementation
/// and enables the core dynamic code generation capabilities.
/// 
/// Architecture Role: Core service for dynamic code execution
/// Research Plan Alignment: Phase 4 - Dynamic Code Generation and Runtime Compilation
/// 
/// Key Capabilities:
/// - Roslyn-based C# compilation
/// - Secure execution environment
/// - Test case generation and validation
/// - Assembly management and cleanup
/// - Performance monitoring and optimization
/// 
/// Security Features:
/// - Code execution sandboxing
/// - Resource usage limits
/// - Malicious code detection
/// - Assembly isolation and cleanup
/// </summary>
public interface IDynamicCodeService
{
    /// <summary>
    /// Compiles and executes C# code dynamically with full security controls.
    /// 
    /// This method is critical for enabling self-modifying code capabilities. It:
    /// - Compiles C# source code using Roslyn
    /// - Executes the compiled code in a secure environment
    /// - Provides comprehensive execution results
    /// - Ensures proper resource cleanup and security
    /// 
    /// Compilation Process:
    /// - Syntax and semantic analysis
    /// - Assembly generation and optimization
    /// - Security validation and sandboxing
    /// - Resource allocation and management
    /// 
    /// Execution Environment:
    /// - Isolated execution context
    /// - Resource usage monitoring
    /// - Exception handling and recovery
    /// - Performance measurement and logging
    /// 
    /// Security Measures:
    /// - Code analysis for malicious patterns
    /// - Resource usage limits and timeouts
    /// - Assembly isolation and cleanup
    /// - Access control and permissions
    /// 
    /// DO NOT REMOVE: Essential for dynamic code execution capabilities
    /// </summary>
    /// <param name="sourceCode">C# source code to compile and execute</param>
    /// <param name="entryPoint">Name of the method to execute (defaults to "Main")</param>
    /// <param name="parameters">Parameters to pass to the entry point method</param>
    /// <param name="timeoutSeconds">Maximum execution time in seconds (default: 30)</param>
    /// <returns>Execution result with output, errors, and performance metrics</returns>
    Task<CodeExecutionResult> CompileAndExecuteAsync(
        string sourceCode,
        string entryPoint = "Main",
        object[]? parameters = null,
        int timeoutSeconds = 30);

    /// <summary>
    /// Generates test cases for the given C# code to validate functionality.
    /// 
    /// This method provides:
    /// - Automated test case generation
    /// - Code coverage analysis
    /// - Edge case identification
    /// - Quality validation and assurance
    /// 
    /// Test Generation Process:
    /// - Code analysis and understanding
    /// - Input parameter identification
    /// - Expected output determination
    /// - Edge case and boundary testing
    /// 
    /// Test Categories:
    /// - Unit tests for individual methods
    /// - Integration tests for component interaction
    /// - Edge case tests for boundary conditions
    /// - Performance tests for optimization
    /// 
    /// Quality Assurance:
    /// - Code coverage measurement
    /// - Test effectiveness validation
    /// - Performance impact assessment
    /// - Security vulnerability detection
    /// 
    /// DO NOT REMOVE: Critical for code quality and validation
    /// </summary>
    /// <param name="sourceCode">C# source code to generate tests for</param>
    /// <param name="testFramework">Testing framework to use (default: xUnit)</param>
    /// <returns>Generated test cases with execution instructions</returns>
    Task<TestGenerationResult> GenerateTestCasesAsync(
        string sourceCode,
        string testFramework = "xUnit");

    /// <summary>
    /// Validates C# code for syntax, semantics, and security without execution.
    /// 
    /// This method provides:
    /// - Pre-execution code validation
    /// - Security analysis and threat detection
    /// - Performance impact assessment
    /// - Code quality analysis
    /// 
    /// Validation Process:
    /// - Syntax and semantic analysis
    /// - Security vulnerability scanning
    /// - Performance impact assessment
    /// - Code quality metrics calculation
    /// 
    /// Security Analysis:
    /// - Malicious code pattern detection
    /// - Resource usage analysis
    /// - Access control validation
    /// - Threat modeling and assessment
    /// 
    /// Quality Metrics:
    /// - Code complexity analysis
    /// - Maintainability assessment
    /// - Performance optimization suggestions
    /// - Best practice compliance
    /// 
    /// DO NOT REMOVE: Essential for code validation and security
    /// </summary>
    /// <param name="sourceCode">C# source code to validate</param>
    /// <returns>Validation result with detailed analysis and recommendations</returns>
    Task<CodeValidationResult> ValidateCodeAsync(string sourceCode);

    /// <summary>
    /// Gets performance metrics and statistics for code execution.
    /// 
    /// This method provides:
    /// - Execution performance analytics
    /// - Resource usage statistics
    /// - Performance trend analysis
    /// - Optimization recommendations
    /// 
    /// Metrics Collected:
    /// - Execution time and CPU usage
    /// - Memory allocation and garbage collection
    /// - Assembly loading and compilation time
    /// - Error rates and failure patterns
    /// 
    /// Analytics Features:
    /// - Performance trend analysis
    /// - Bottleneck identification
    /// - Optimization recommendations
    /// - Resource usage optimization
    /// 
    /// Use Cases:
    /// - Performance monitoring and optimization
    /// - Resource usage analysis
    /// - System capacity planning
    /// - Continuous improvement insights
    /// 
    /// DO NOT REMOVE: Essential for performance monitoring and optimization
    /// </summary>
    /// <returns>Performance metrics and analytics data</returns>
    Task<PerformanceMetrics> GetPerformanceMetricsAsync();
}

/// <summary>
/// Represents the result of dynamic code compilation and execution.
/// 
/// This class provides comprehensive information about:
/// - Compilation success or failure
/// - Execution results and output
/// - Performance metrics and timing
/// - Error information and debugging details
/// 
/// DO NOT DELETE: Required for code execution result handling
/// </summary>
public class CodeExecutionResult
{
    /// <summary>
    /// Whether the compilation and execution was successful
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Output from the executed code (console output, return values, etc.)
    /// </summary>
    public string Output { get; set; } = string.Empty;
    
    /// <summary>
    /// Any errors that occurred during compilation or execution
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
    
    /// <summary>
    /// Execution time in milliseconds
    /// </summary>
    public long ExecutionTimeMs { get; set; }
    
    /// <summary>
    /// Memory usage in bytes
    /// </summary>
    public long MemoryUsageBytes { get; set; }
    
    /// <summary>
    /// Assembly information for the compiled code
    /// </summary>
    public string AssemblyInfo { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the execution was terminated due to timeout
    /// </summary>
    public bool WasTimeout { get; set; }
    
    /// <summary>
    /// Detailed compilation diagnostics and warnings
    /// </summary>
    public List<string> Diagnostics { get; set; } = new();
}

/// <summary>
/// Represents the result of test case generation for C# code.
/// 
/// This class provides:
/// - Generated test cases and scenarios
/// - Test execution instructions
/// - Coverage analysis and metrics
/// - Quality assessment and recommendations
/// 
/// DO NOT DELETE: Required for test generation result handling
/// </summary>
public class TestGenerationResult
{
    /// <summary>
    /// Whether test generation was successful
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Generated test cases as C# code
    /// </summary>
    public string TestCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Test execution instructions and setup requirements
    /// </summary>
    public string Instructions { get; set; } = string.Empty;
    
    /// <summary>
    /// Estimated code coverage percentage
    /// </summary>
    public double CodeCoverage { get; set; }
    
    /// <summary>
    /// Number of test cases generated
    /// </summary>
    public int TestCaseCount { get; set; }
    
    /// <summary>
    /// Categories of tests generated (unit, integration, edge cases, etc.)
    /// </summary>
    public List<string> TestCategories { get; set; } = new();
    
    /// <summary>
    /// Any warnings or recommendations for the tests
    /// </summary>
    public List<string> Warnings { get; set; } = new();
}

/// <summary>
/// Represents the result of code validation and analysis.
/// 
/// This class provides:
/// - Syntax and semantic validation results
/// - Security analysis and threat assessment
/// - Performance impact analysis
/// - Code quality metrics and recommendations
/// 
/// DO NOT DELETE: Required for code validation result handling
/// </summary>
public class CodeValidationResult
{
    /// <summary>
    /// Whether the code passed all validation checks
    /// </summary>
    public bool IsValid { get; set; }
    
    /// <summary>
    /// Syntax and semantic validation results
    /// </summary>
    public List<string> ValidationErrors { get; set; } = new();
    
    /// <summary>
    /// Security vulnerabilities and threats detected
    /// </summary>
    public List<string> SecurityIssues { get; set; } = new();
    
    /// <summary>
    /// Performance impact assessment and recommendations
    /// </summary>
    public string PerformanceAnalysis { get; set; } = string.Empty;
    
    /// <summary>
    /// Code quality metrics and scores
    /// </summary>
    public Dictionary<string, double> QualityMetrics { get; set; } = new();
    
    /// <summary>
    /// Recommendations for improvement
    /// </summary>
    public List<string> Recommendations { get; set; } = new();
    
    /// <summary>
    /// Estimated compilation and execution complexity
    /// </summary>
    public string Complexity { get; set; } = string.Empty;
}

/// <summary>
/// Represents performance metrics and analytics for code execution.
/// 
/// This class provides:
/// - Execution performance statistics
/// - Resource usage analytics
/// - Performance trends and patterns
/// - Optimization recommendations
/// 
/// DO NOT DELETE: Required for performance metrics handling
/// </summary>
public class PerformanceMetrics
{
    /// <summary>
    /// Average execution time in milliseconds
    /// </summary>
    public double AverageExecutionTimeMs { get; set; }
    
    /// <summary>
    /// Average memory usage in bytes
    /// </summary>
    public double AverageMemoryUsageBytes { get; set; }
    
    /// <summary>
    /// Total number of executions tracked
    /// </summary>
    public int TotalExecutions { get; set; }
    
    /// <summary>
    /// Number of successful executions
    /// </summary>
    public int SuccessfulExecutions { get; set; }
    
    /// <summary>
    /// Number of failed executions
    /// </summary>
    public int FailedExecutions { get; set; }
    
    /// <summary>
    /// Number of timeout executions
    /// </summary>
    public int TimeoutExecutions { get; set; }
    
    /// <summary>
    /// Performance trend data over time
    /// </summary>
    public List<PerformanceDataPoint> TrendData { get; set; } = new();
    
    /// <summary>
    /// Optimization recommendations based on performance analysis
    /// </summary>
    public List<string> OptimizationRecommendations { get; set; } = new();
}

/// <summary>
/// Represents a single performance data point for trend analysis.
/// 
/// DO NOT DELETE: Required for performance trend analysis
/// </summary>
public class PerformanceDataPoint
{
    /// <summary>
    /// Timestamp of the performance measurement
    /// </summary>
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// Execution time in milliseconds
    /// </summary>
    public long ExecutionTimeMs { get; set; }
    
    /// <summary>
    /// Memory usage in bytes
    /// </summary>
    public long MemoryUsageBytes { get; set; }
    
    /// <summary>
    /// Whether the execution was successful
    /// </summary>
    public bool WasSuccessful { get; set; }
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