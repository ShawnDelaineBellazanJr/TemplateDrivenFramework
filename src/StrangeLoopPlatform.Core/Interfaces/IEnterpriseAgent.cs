using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Interface for enterprise agents in the strange loop platform
/// </summary>
public interface IEnterpriseAgent
{
    /// <summary>
    /// The role of this agent
    /// </summary>
    AgentRole Role { get; }
    
    /// <summary>
    /// Executes the agent with the given context
    /// </summary>
    /// <param name="context">The execution context</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result from the agent execution</returns>
    Task<AgentResult> ExecuteAsync(AgentExecutionContext context, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Whether the agent is currently available for execution
    /// </summary>
    bool IsAvailable { get; }
    
    /// <summary>
    /// Gets the agent's capabilities and limitations
    /// </summary>
    /// <returns>Agent capabilities information</returns>
    AgentCapabilities GetCapabilities();
}

/// <summary>
/// Context for agent execution
/// </summary>
public class AgentExecutionContext
{
    /// <summary>
    /// The original request
    /// </summary>
    public StrangeLoopRequest Request { get; set; } = new();
    
    /// <summary>
    /// Current iteration number
    /// </summary>
    public int Iteration { get; set; }
    
    /// <summary>
    /// Results from previous agents in this iteration
    /// </summary>
    public Dictionary<AgentRole, AgentResult> PreviousResults { get; set; } = new();
    
    /// <summary>
    /// Generated artifacts from previous iterations
    /// </summary>
    public List<GeneratedArtifact> PreviousArtifacts { get; set; } = new();
    
    /// <summary>
    /// Quality metrics from previous iterations
    /// </summary>
    public QualityMetrics PreviousQualityMetrics { get; set; } = new();
    
    /// <summary>
    /// Any errors or warnings from previous iterations
    /// </summary>
    public List<string> Errors { get; set; } = new();
    
    /// <summary>
    /// Any warnings from previous iterations
    /// </summary>
    public List<string> Warnings { get; set; } = new();
    
    /// <summary>
    /// Additional context data
    /// </summary>
    public Dictionary<string, object> AdditionalContext { get; set; } = new();
}

/// <summary>
/// Information about an agent's capabilities
/// </summary>
public class AgentCapabilities
{
    /// <summary>
    /// Whether the agent can handle the given complexity level
    /// </summary>
    public bool CanHandleComplexity(ComplexityLevel complexity) => true;
    
    /// <summary>
    /// Whether the agent can handle the given security level
    /// </summary>
    public bool CanHandleSecurityLevel(SecurityLevel securityLevel) => true;
    
    /// <summary>
    /// Maximum execution time for this agent
    /// </summary>
    public TimeSpan MaxExecutionTime { get; set; } = TimeSpan.FromMinutes(5);
    
    /// <summary>
    /// Whether the agent supports dynamic code generation
    /// </summary>
    public bool SupportsDynamicCodeGeneration { get; set; } = false;
    
    /// <summary>
    /// Whether the agent supports semantic memory
    /// </summary>
    public bool SupportsSemanticMemory { get; set; } = false;
    
    /// <summary>
    /// Whether the agent supports dynamic API generation
    /// </summary>
    public bool SupportsDynamicApiGeneration { get; set; } = false;
} 