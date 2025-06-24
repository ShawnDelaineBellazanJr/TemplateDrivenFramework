using System.Text.Json.Serialization;

namespace StrangeLoopPlatform.Core.Models;

/// <summary>
/// Represents the state of a self-improvement process in the autonomous AI system.
/// This class tracks the progress, status, and artifacts of an ongoing improvement
/// cycle, enabling process monitoring, persistence, and resumption capabilities.
/// 
/// Key responsibilities:
/// - Track process lifecycle and phase progression
/// - Store process artifacts and generated content
/// - Maintain process metadata and context
/// - Enable process monitoring and control
/// 
/// This model is essential for the self-evolving architecture as it provides
/// the foundation for process management and autonomous improvement tracking.
/// </summary>
public class ProcessState
{
    /// <summary>
    /// Unique identifier for the process instance.
    /// This ID is used for process identification, tracking, and management
    /// throughout the improvement lifecycle.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable title describing the improvement process.
    /// This title provides context about what the process aims to achieve
    /// and helps with process identification and management.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of the improvement goals and objectives.
    /// This description provides comprehensive context about what the
    /// process is trying to accomplish and why.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Current status of the process in the improvement lifecycle.
    /// This status indicates where the process is in its execution
    /// and what phase it's currently in.
    /// </summary>
    public ProcessStatus Status { get; set; } = ProcessStatus.Created;

    /// <summary>
    /// Current phase of the improvement workflow.
    /// The process progresses through four main phases: Planning, Coding, Testing, and Reflecting.
    /// Each phase involves different activities and agent collaborations.
    /// </summary>
    public ProcessPhase CurrentPhase { get; set; } = ProcessPhase.Planning;

    /// <summary>
    /// When the process was created and started.
    /// This timestamp provides process lifecycle tracking and enables
    /// time-based analysis and monitoring.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the process was last updated or modified.
    /// This timestamp helps track process activity and enables
    /// change detection and monitoring.
    /// </summary>
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When the process was completed (if applicable).
    /// This timestamp indicates when the improvement cycle finished
    /// and provides completion tracking.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Progress percentage of the process (0-100).
    /// This value indicates how much of the improvement cycle
    /// has been completed and provides progress tracking.
    /// </summary>
    public int ProgressPercentage { get; set; } = 0;

    /// <summary>
    /// Specific goals and objectives for the improvement process.
    /// These goals define what the process aims to achieve and
    /// provide direction for the improvement activities.
    /// </summary>
    public List<string> Goals { get; set; } = new();

    /// <summary>
    /// Constraints and limitations to consider during improvement.
    /// These constraints define boundaries and limitations that
    /// the improvement process must respect.
    /// </summary>
    public List<string> Constraints { get; set; } = new();

    /// <summary>
    /// Priority level of the improvement process.
    /// This priority helps with process scheduling and resource allocation
    /// in multi-process environments.
    /// </summary>
    public string Priority { get; set; } = "Medium";

    /// <summary>
    /// Maximum time allowed for the improvement process.
    /// This time limit ensures processes don't run indefinitely
    /// and enables resource management.
    /// </summary>
    public TimeSpan? TimeLimit { get; set; }

    /// <summary>
    /// Artifacts and outputs generated during the improvement process.
    /// These artifacts include generated code, test results, documentation,
    /// and other outputs from the improvement activities.
    /// </summary>
    public Dictionary<string, object> Artifacts { get; set; } = new();

    /// <summary>
    /// Error messages and issues encountered during the process.
    /// These errors provide debugging information and help identify
    /// problems that occurred during improvement.
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Warning messages and potential issues identified during the process.
    /// These warnings provide insights into potential problems
    /// and areas that may need attention.
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// Additional context and metadata for the process.
    /// This context provides additional information that may be useful
    /// for process understanding, debugging, or analysis.
    /// </summary>
    public Dictionary<string, object> Context { get; set; } = new();

    /// <summary>
    /// Tags for categorizing and organizing the process.
    /// These tags enable process organization, filtering, and
    /// search capabilities.
    /// </summary>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Performance metrics and statistics for the process.
    /// These metrics provide insights into process performance,
    /// resource usage, and efficiency.
    /// </summary>
    public ProcessMetrics Metrics { get; set; } = new();

    /// <summary>
    /// Whether the process is currently active and running.
    /// This flag indicates if the process is actively executing
    /// or if it's in a paused, completed, or cancelled state.
    /// </summary>
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Represents the status of a self-improvement process.
/// This enum defines the various states a process can be in
/// throughout its lifecycle.
/// </summary>
public enum ProcessStatus
{
    /// <summary>
    /// Process has been created but not yet started
    /// </summary>
    Created,

    /// <summary>
    /// Process is currently running and executing
    /// </summary>
    Running,

    /// <summary>
    /// Process has been paused and can be resumed
    /// </summary>
    Paused,

    /// <summary>
    /// Process has been cancelled and will not continue
    /// </summary>
    Cancelled,

    /// <summary>
    /// Process has completed successfully
    /// </summary>
    Completed,

    /// <summary>
    /// Process has failed and cannot continue
    /// </summary>
    Failed,

    /// <summary>
    /// Process has timed out and was terminated
    /// </summary>
    TimedOut
}

/// <summary>
/// Represents the phases of the self-improvement workflow.
/// This enum defines the four main phases that each improvement
/// process progresses through.
/// </summary>
public enum ProcessPhase
{
    /// <summary>
    /// Planning phase - analyzing requirements and creating improvement plan
    /// </summary>
    Planning,

    /// <summary>
    /// Coding phase - generating and implementing code improvements
    /// </summary>
    Coding,

    /// <summary>
    /// Testing phase - validating and testing generated improvements
    /// </summary>
    Testing,

    /// <summary>
    /// Reflecting phase - analyzing results and learning from the process
    /// </summary>
    Reflecting
}

/// <summary>
/// Represents performance metrics and statistics for a process.
/// This class tracks various performance indicators that help
/// monitor and optimize process execution.
/// </summary>
public class ProcessMetrics
{
    /// <summary>
    /// Total execution time of the process
    /// </summary>
    public TimeSpan TotalExecutionTime { get; set; }

    /// <summary>
    /// Time spent in each phase of the process
    /// </summary>
    public Dictionary<ProcessPhase, TimeSpan> PhaseExecutionTimes { get; set; } = new();

    /// <summary>
    /// Memory usage during process execution
    /// </summary>
    public long MemoryUsageBytes { get; set; }

    /// <summary>
    /// CPU usage during process execution
    /// </summary>
    public double CpuUsagePercentage { get; set; }

    /// <summary>
    /// Number of agent interactions during the process
    /// </summary>
    public int AgentInteractions { get; set; }

    /// <summary>
    /// Number of code generations performed
    /// </summary>
    public int CodeGenerations { get; set; }

    /// <summary>
    /// Number of tests executed during the process
    /// </summary>
    public int TestsExecuted { get; set; }

    /// <summary>
    /// Success rate of generated code (percentage)
    /// </summary>
    public double CodeSuccessRate { get; set; }

    /// <summary>
    /// Test pass rate (percentage)
    /// </summary>
    public double TestPassRate { get; set; }
}

/// <summary>
/// Represents metrics and statistics for semantic memory operations.
/// This class provides insights into memory usage, patterns, and
/// system learning effectiveness.
/// </summary>
public class MemoryMetrics
{
    /// <summary>
    /// Total number of memory entries stored
    /// </summary>
    public int TotalEntries { get; set; }

    /// <summary>
    /// Number of active memory entries
    /// </summary>
    public int ActiveEntries { get; set; }

    /// <summary>
    /// Total storage size in bytes
    /// </summary>
    public long TotalStorageBytes { get; set; }

    /// <summary>
    /// Average entry size in bytes
    /// </summary>
    public double AverageEntrySizeBytes { get; set; }

    /// <summary>
    /// Number of retrieval operations performed
    /// </summary>
    public int RetrievalOperations { get; set; }

    /// <summary>
    /// Number of storage operations performed
    /// </summary>
    public int StorageOperations { get; set; }

    /// <summary>
    /// Average retrieval time in milliseconds
    /// </summary>
    public double AverageRetrievalTimeMs { get; set; }

    /// <summary>
    /// Average storage time in milliseconds
    /// </summary>
    public double AverageStorageTimeMs { get; set; }

    /// <summary>
    /// Memory hit rate (percentage of successful retrievals)
    /// </summary>
    public double HitRate { get; set; }

    /// <summary>
    /// Most frequently accessed tags
    /// </summary>
    public Dictionary<string, int> TopTags { get; set; } = new();

    /// <summary>
    /// Memory usage by category
    /// </summary>
    public Dictionary<string, int> EntriesByCategory { get; set; } = new();

    /// <summary>
    /// When the metrics were last updated
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
} 