namespace StrangeLoopPlatform.Core.Models;

/// <summary>
/// Represents a request to the strange loop platform for self-evolving AI capabilities
/// </summary>
public class StrangeLoopRequest
{
    /// <summary>
    /// Unique identifier for the request
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// The main requirement or problem to be solved
    /// </summary>
    public string Requirements { get; set; } = string.Empty;
    
    /// <summary>
    /// Non-functional requirements (performance, security, etc.)
    /// </summary>
    public string? NonFunctionalRequirements { get; set; }
    
    /// <summary>
    /// Business context and constraints
    /// </summary>
    public string? BusinessContext { get; set; }
    
    /// <summary>
    /// Technical constraints and limitations
    /// </summary>
    public string? Constraints { get; set; }
    
    /// <summary>
    /// Project complexity level
    /// </summary>
    public ComplexityLevel Complexity { get; set; } = ComplexityLevel.Medium;
    
    /// <summary>
    /// Security level requirements
    /// </summary>
    public SecurityLevel SecurityLevel { get; set; } = SecurityLevel.Enterprise;
    
    /// <summary>
    /// Maximum number of iterations allowed
    /// </summary>
    public int MaxIterations { get; set; } = 10;
    
    /// <summary>
    /// Whether to enable dynamic code generation
    /// </summary>
    public bool EnableDynamicCodeGeneration { get; set; } = true;
    
    /// <summary>
    /// Whether to enable semantic memory
    /// </summary>
    public bool EnableSemanticMemory { get; set; } = true;
    
    /// <summary>
    /// Whether to enable dynamic API generation
    /// </summary>
    public bool EnableDynamicApiGeneration { get; set; } = true;
    
    /// <summary>
    /// Timestamp when the request was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents the complexity level of a project
/// </summary>
public enum ComplexityLevel
{
    Low,
    Medium,
    High,
    Critical
}

/// <summary>
/// Represents the security level requirements
/// </summary>
public enum SecurityLevel
{
    Basic,
    Enterprise,
    Critical
} 