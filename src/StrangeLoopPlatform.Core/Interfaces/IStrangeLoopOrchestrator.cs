using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Main orchestrator interface for the strange loop platform
/// </summary>
public interface IStrangeLoopOrchestrator
{
    /// <summary>
    /// Executes a strange loop process with the given request
    /// </summary>
    /// <param name="request">The request to process</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The response from the strange loop process</returns>
    Task<StrangeLoopResponse> ExecuteAsync(StrangeLoopRequest request, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the status of a running strange loop process
    /// </summary>
    /// <param name="requestId">The ID of the request</param>
    /// <returns>The current response status</returns>
    Task<StrangeLoopResponse?> GetStatusAsync(string requestId);
    
    /// <summary>
    /// Cancels a running strange loop process
    /// </summary>
    /// <param name="requestId">The ID of the request to cancel</param>
    /// <returns>True if the process was cancelled successfully</returns>
    Task<bool> CancelAsync(string requestId);
    
    /// <summary>
    /// Gets all active strange loop processes
    /// </summary>
    /// <returns>List of active processes</returns>
    Task<IEnumerable<StrangeLoopResponse>> GetActiveProcessesAsync();
} 