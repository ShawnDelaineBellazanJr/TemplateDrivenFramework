namespace StrangeLoopPlatform.Core.Models;

/// <summary>
/// Represents the response from the strange loop platform
/// </summary>
public class StrangeLoopResponse
{
    /// <summary>
    /// Unique identifier for the response
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// The original request that generated this response
    /// </summary>
    public StrangeLoopRequest Request { get; set; } = new();
    
    /// <summary>
    /// The current status of the strange loop process
    /// </summary>
    public StrangeLoopStatus Status { get; set; } = StrangeLoopStatus.Pending;
    
    /// <summary>
    /// Current iteration number
    /// </summary>
    public int CurrentIteration { get; set; } = 0;
    
    /// <summary>
    /// Results from each agent in the current iteration
    /// </summary>
    public Dictionary<AgentRole, AgentResult> AgentResults { get; set; } = new();
    
    /// <summary>
    /// Generated code or artifacts
    /// </summary>
    public List<GeneratedArtifact> GeneratedArtifacts { get; set; } = new();
    
    /// <summary>
    /// Quality metrics and validation results
    /// </summary>
    public QualityMetrics QualityMetrics { get; set; } = new();
    
    /// <summary>
    /// Any errors or issues encountered
    /// </summary>
    public List<string> Errors { get; set; } = new();
    
    /// <summary>
    /// Warnings or recommendations
    /// </summary>
    public List<string> Warnings { get; set; } = new();
    
    /// <summary>
    /// Timestamp when the response was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Timestamp when the process completed
    /// </summary>
    public DateTime? CompletedAt { get; set; }
    
    /// <summary>
    /// Total duration of the process
    /// </summary>
    public TimeSpan? Duration => CompletedAt?.Subtract(CreatedAt);
}

/// <summary>
/// Represents the status of a strange loop process
/// </summary>
public enum StrangeLoopStatus
{
    Pending,
    Planning,
    Implementing,
    Testing,
    Reflecting,
    Completed,
    Failed,
    Cancelled
}

/// <summary>
/// Represents the result from a specific agent
/// </summary>
public class AgentResult
{
    /// <summary>
    /// The agent role that produced this result
    /// </summary>
    public AgentRole Role { get; set; }
    
    /// <summary>
    /// The output from the agent
    /// </summary>
    public string Output { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the agent execution was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Duration of the agent execution
    /// </summary>
    public TimeSpan Duration { get; set; }
    
    /// <summary>
    /// Any errors encountered during execution
    /// </summary>
    public string? Error { get; set; }
    
    /// <summary>
    /// Metadata about the agent execution
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new();
}

/// <summary>
/// Represents a generated artifact (code, API, etc.)
/// </summary>
public class GeneratedArtifact
{
    /// <summary>
    /// Unique identifier for the artifact
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Type of artifact (code, API, documentation, etc.)
    /// </summary>
    public ArtifactType Type { get; set; }
    
    /// <summary>
    /// Name or title of the artifact
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Content of the artifact
    /// </summary>
    public string Content { get; set; } = string.Empty;
    
    /// <summary>
    /// File path or location where the artifact should be stored
    /// </summary>
    public string? FilePath { get; set; }
    
    /// <summary>
    /// MIME type of the content
    /// </summary>
    public string MimeType { get; set; } = "text/plain";
    
    /// <summary>
    /// Whether the artifact passed validation
    /// </summary>
    public bool IsValid { get; set; }
    
    /// <summary>
    /// Timestamp when the artifact was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents the type of generated artifact
/// </summary>
public enum ArtifactType
{
    Code,
    Api,
    Documentation,
    Test,
    Configuration,
    Architecture,
    Other
}

/// <summary>
/// Represents quality metrics for the generated solution
/// </summary>
public class QualityMetrics
{
    /// <summary>
    /// Code coverage percentage
    /// </summary>
    public double CodeCoverage { get; set; }
    
    /// <summary>
    /// Security score (0-100)
    /// </summary>
    public double SecurityScore { get; set; }
    
    /// <summary>
    /// Performance score (0-100)
    /// </summary>
    public double PerformanceScore { get; set; }
    
    /// <summary>
    /// Overall quality score (0-100)
    /// </summary>
    public double OverallScore { get; set; }
    
    /// <summary>
    /// Number of critical issues found
    /// </summary>
    public int CriticalIssues { get; set; }
    
    /// <summary>
    /// Number of high priority issues found
    /// </summary>
    public int HighPriorityIssues { get; set; }
    
    /// <summary>
    /// Number of medium priority issues found
    /// </summary>
    public int MediumPriorityIssues { get; set; }
    
    /// <summary>
    /// Number of low priority issues found
    /// </summary>
    public int LowPriorityIssues { get; set; }
    
    /// <summary>
    /// Whether all quality gates passed
    /// </summary>
    public bool AllQualityGatesPassed { get; set; }
} 