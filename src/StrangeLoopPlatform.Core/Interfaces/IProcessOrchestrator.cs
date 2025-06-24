using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Orchestrates the self-improvement workflow using Semantic Kernel Process Framework.
/// This interface manages the autonomous improvement cycle that enables the system
/// to evolve and enhance itself through AI-driven code generation and analysis.
/// 
/// Key responsibilities:
/// - Manage the four-phase improvement cycle (Planning, Coding, Testing, Reflecting)
/// - Coordinate multi-agent collaboration for code generation
/// - Handle process state management and persistence
/// - Provide process monitoring and control capabilities
/// 
/// This orchestrator is the core component of the self-evolving architecture,
/// enabling continuous autonomous improvement through structured workflows.
/// </summary>
public interface IProcessOrchestrator
{
    /// <summary>
    /// Starts a new self-improvement process with the specified request.
    /// This method initiates the autonomous improvement cycle by creating
    /// a new process state and beginning the planning phase.
    /// </summary>
    /// <param name="request">The improvement request containing goals and context</param>
    /// <returns>A ProcessState representing the started process</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="InvalidOperationException">Thrown when process cannot be started</exception>
    Task<ProcessState> StartProcessAsync(SelfImprovementRequest request);

    /// <summary>
    /// Executes the next phase of the self-improvement process.
    /// This method progresses through the workflow phases: Planning → Coding → Testing → Reflecting.
    /// Each phase involves different agents and activities to achieve the improvement goals.
    /// </summary>
    /// <param name="processId">The unique identifier of the process to execute</param>
    /// <returns>Updated ProcessState after phase execution</returns>
    /// <exception cref="ArgumentException">Thrown when processId is invalid</exception>
    /// <exception cref="InvalidOperationException">Thrown when process cannot be executed</exception>
    Task<ProcessState> ExecutePhaseAsync(string processId);

    /// <summary>
    /// Gets the current state of a specific process.
    /// This method provides detailed information about the process including
    /// its current phase, progress, and any generated artifacts.
    /// </summary>
    /// <param name="processId">The unique identifier of the process</param>
    /// <returns>The current ProcessState, or null if not found</returns>
    /// <exception cref="ArgumentException">Thrown when processId is invalid</exception>
    Task<ProcessState?> GetProcessStateAsync(string processId);

    /// <summary>
    /// Gets all active processes managed by this orchestrator.
    /// This method provides visibility into all ongoing self-improvement activities
    /// and their current status.
    /// </summary>
    /// <returns>An enumerable of all active ProcessState objects</returns>
    Task<IEnumerable<ProcessState>> GetActiveProcessesAsync();

    /// <summary>
    /// Cancels an active process and cleans up associated resources.
    /// This method safely terminates a process in progress and ensures
    /// proper cleanup of any temporary artifacts or resources.
    /// </summary>
    /// <param name="processId">The unique identifier of the process to cancel</param>
    /// <returns>True if the process was successfully cancelled, false if not found</returns>
    /// <exception cref="ArgumentException">Thrown when processId is invalid</exception>
    Task<bool> CancelProcessAsync(string processId);

    /// <summary>
    /// Completes the entire self-improvement workflow for a process.
    /// This method executes all phases of the improvement cycle from start to finish,
    /// providing a complete autonomous improvement experience.
    /// </summary>
    /// <param name="processId">The unique identifier of the process to complete</param>
    /// <returns>The final ProcessState after completion</returns>
    /// <exception cref="ArgumentException">Thrown when processId is invalid</exception>
    /// <exception cref="InvalidOperationException">Thrown when process cannot be completed</exception>
    Task<ProcessState> CompleteProcessAsync(string processId);
}

/// <summary>
/// Represents a request to initiate a self-improvement process.
/// This class contains all the information needed to start an autonomous
/// improvement cycle, including goals, context, and constraints.
/// </summary>
public class SelfImprovementRequest
{
    /// <summary>
    /// A descriptive title for the improvement process
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of what should be improved
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Specific goals or objectives for the improvement
    /// </summary>
    public List<string> Goals { get; set; } = new();

    /// <summary>
    /// Any constraints or limitations to consider during improvement
    /// </summary>
    public List<string> Constraints { get; set; } = new();

    /// <summary>
    /// Priority level of the improvement (Low, Medium, High, Critical)
    /// </summary>
    public string Priority { get; set; } = "Medium";

    /// <summary>
    /// Maximum time allowed for the improvement process
    /// </summary>
    public TimeSpan? TimeLimit { get; set; }

    /// <summary>
    /// Additional context or requirements for the improvement
    /// </summary>
    public Dictionary<string, object> Context { get; set; } = new();
} 