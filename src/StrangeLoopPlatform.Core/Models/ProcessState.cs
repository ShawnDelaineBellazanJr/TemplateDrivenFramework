using System.Text.Json.Serialization;

namespace StrangeLoopPlatform.Core.Models;

/// <summary>
/// Represents the state of a self-improvement process workflow
/// </summary>
public class ProcessState
{
    /// <summary>
    /// Unique identifier for this process instance
    /// </summary>
    public string ProcessId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Current phase of the process
    /// </summary>
    public ProcessPhase CurrentPhase { get; set; } = ProcessPhase.Initialized;

    /// <summary>
    /// Current iteration number (1-based)
    /// </summary>
    public int IterationNumber { get; set; } = 1;

    /// <summary>
    /// Maximum number of iterations allowed
    /// </summary>
    public int MaxIterations { get; set; } = 10;

    /// <summary>
    /// The original request that initiated this process
    /// </summary>
    public StrangeLoopRequest OriginalRequest { get; set; } = new();

    /// <summary>
    /// Current plan being executed
    /// </summary>
    public string? CurrentPlan { get; set; }

    /// <summary>
    /// Generated code in the current iteration
    /// </summary>
    public string? GeneratedCode { get; set; }

    /// <summary>
    /// Test results from the current iteration
    /// </summary>
    public TestResults? TestResults { get; set; }

    /// <summary>
    /// Reflection and analysis from the current iteration
    /// </summary>
    public ReflectionResult? ReflectionResult { get; set; }

    /// <summary>
    /// Whether the process has converged to a solution
    /// </summary>
    public bool IsConverged { get; set; } = false;

    /// <summary>
    /// Error message if the process failed
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Timestamp when the process started
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp when the process completed
    /// </summary>
    public DateTime? CompletionTime { get; set; }

    /// <summary>
    /// Performance metrics for this process
    /// </summary>
    public ProcessMetrics Metrics { get; set; } = new();

    /// <summary>
    /// Context data shared between agents
    /// </summary>
    public Dictionary<string, object> Context { get; set; } = new();

    /// <summary>
    /// Version history of generated artifacts
    /// </summary>
    public List<ArtifactVersion> ArtifactHistory { get; set; } = new();

    /// <summary>
    /// Whether the process should continue to the next iteration
    /// </summary>
    public bool ShouldContinue => !IsConverged && IterationNumber < MaxIterations && string.IsNullOrEmpty(ErrorMessage);
}

/// <summary>
/// Represents the phases of the self-improvement process
/// </summary>
public enum ProcessPhase
{
    Initialized,
    Planning,
    Coding,
    Testing,
    Reflecting,
    Converged,
    Failed
}

/// <summary>
/// Results from testing phase
/// </summary>
public class TestResults
{
    public bool AllTestsPassed { get; set; }
    public List<TestResult> TestCases { get; set; } = new();
    public string? ErrorMessage { get; set; }
    public double PerformanceScore { get; set; }
    public TimeSpan ExecutionTime { get; set; }
}

/// <summary>
/// Individual test case result
/// </summary>
public class TestResult
{
    public string TestName { get; set; } = string.Empty;
    public bool Passed { get; set; }
    public string? ExpectedOutput { get; set; }
    public string? ActualOutput { get; set; }
    public string? ErrorMessage { get; set; }
    public TimeSpan ExecutionTime { get; set; }
}

/// <summary>
/// Results from reflection phase
/// </summary>
public class ReflectionResult
{
    public bool ShouldContinue { get; set; }
    public string Analysis { get; set; } = string.Empty;
    public List<string> Improvements { get; set; } = new();
    public List<string> Issues { get; set; } = new();
    public double QualityScore { get; set; }
    public string NextAction { get; set; } = string.Empty;
}

/// <summary>
/// Performance metrics for the process
/// </summary>
public class ProcessMetrics
{
    public int TotalIterations { get; set; }
    public TimeSpan TotalExecutionTime { get; set; }
    public TimeSpan AverageIterationTime { get; set; }
    public int TotalTokensUsed { get; set; }
    public int TotalFunctionCalls { get; set; }
    public Dictionary<string, TimeSpan> PhaseTimings { get; set; } = new();
}

/// <summary>
/// Version history of generated artifacts
/// </summary>
public class ArtifactVersion
{
    public int Version { get; set; }
    public DateTime Timestamp { get; set; }
    public string ArtifactType { get; set; } = string.Empty; // "plan", "code", "test"
    public string Content { get; set; } = string.Empty;
    public string? ChangeDescription { get; set; }
    public bool IsActive { get; set; }
} 