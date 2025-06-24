using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Orchestrates the self-improvement process workflow using SK Process Framework
/// </summary>
public interface IProcessOrchestrator
{
    /// <summary>
    /// Starts a new self-improvement process
    /// </summary>
    /// <param name="request">The initial request to process</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Process state with initial configuration</returns>
    Task<ProcessState> StartProcessAsync(StrangeLoopRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the next phase of the process
    /// </summary>
    /// <param name="processState">Current process state</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated process state</returns>
    Task<ProcessState> ExecuteNextPhaseAsync(ProcessState processState, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a complete self-improvement cycle
    /// </summary>
    /// <param name="request">The request to process</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Final process state with results</returns>
    Task<ProcessState> ExecuteCompleteProcessAsync(StrangeLoopRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current status of a process
    /// </summary>
    /// <param name="processId">Process identifier</param>
    /// <returns>Current process state or null if not found</returns>
    Task<ProcessState?> GetProcessStatusAsync(string processId);

    /// <summary>
    /// Cancels a running process
    /// </summary>
    /// <param name="processId">Process identifier</param>
    /// <returns>True if process was cancelled successfully</returns>
    Task<bool> CancelProcessAsync(string processId);

    /// <summary>
    /// Gets all active processes
    /// </summary>
    /// <returns>List of active process states</returns>
    Task<IEnumerable<ProcessState>> GetActiveProcessesAsync();
} 