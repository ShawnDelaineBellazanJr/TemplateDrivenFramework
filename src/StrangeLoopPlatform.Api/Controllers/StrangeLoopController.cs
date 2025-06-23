using Microsoft.AspNetCore.Mvc;
using StrangeLoopPlatform.Core.Interfaces;
using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Api.Controllers;

/// <summary>
/// API controller for the Strange Loop Platform
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StrangeLoopController : ControllerBase
{
    private readonly IStrangeLoopOrchestrator _orchestrator;
    private readonly ILogger<StrangeLoopController> _logger;

    public StrangeLoopController(
        IStrangeLoopOrchestrator orchestrator,
        ILogger<StrangeLoopController> logger)
    {
        _orchestrator = orchestrator;
        _logger = logger;
    }

    /// <summary>
    /// Execute a strange loop process
    /// </summary>
    /// <param name="request">The strange loop request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The strange loop response</returns>
    [HttpPost("execute")]
    [ProducesResponseType(typeof(StrangeLoopResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<StrangeLoopResponse>> ExecuteAsync(
        [FromBody] StrangeLoopRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Received strange loop execution request: {RequestId}", request.Id);

            if (string.IsNullOrWhiteSpace(request.Requirements))
            {
                return BadRequest("Requirements cannot be empty");
            }

            var response = await _orchestrator.ExecuteAsync(request, cancellationToken);

            _logger.LogInformation("Completed strange loop execution for request: {RequestId}", request.Id);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing strange loop process for request: {RequestId}", request.Id);
            return StatusCode(500, new { error = "An error occurred while executing the strange loop process", details = ex.Message });
        }
    }

    /// <summary>
    /// Get the status of a strange loop process
    /// </summary>
    /// <param name="requestId">The request ID</param>
    /// <returns>The current status</returns>
    [HttpGet("status/{requestId}")]
    [ProducesResponseType(typeof(StrangeLoopResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StrangeLoopResponse>> GetStatusAsync(string requestId)
    {
        try
        {
            var response = await _orchestrator.GetStatusAsync(requestId);
            
            if (response == null)
            {
                return NotFound($"No process found with ID: {requestId}");
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting status for request: {RequestId}", requestId);
            return StatusCode(500, new { error = "An error occurred while getting the status", details = ex.Message });
        }
    }

    /// <summary>
    /// Cancel a running strange loop process
    /// </summary>
    /// <param name="requestId">The request ID to cancel</param>
    /// <returns>Success status</returns>
    [HttpPost("cancel/{requestId}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> CancelAsync(string requestId)
    {
        try
        {
            var cancelled = await _orchestrator.CancelAsync(requestId);
            
            if (!cancelled)
            {
                return NotFound($"No active process found with ID: {requestId}");
            }

            _logger.LogInformation("Cancelled strange loop process: {RequestId}", requestId);
            return Ok(cancelled);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling process: {RequestId}", requestId);
            return StatusCode(500, new { error = "An error occurred while cancelling the process", details = ex.Message });
        }
    }

    /// <summary>
    /// Get all active strange loop processes
    /// </summary>
    /// <returns>List of active processes</returns>
    [HttpGet("active")]
    [ProducesResponseType(typeof(IEnumerable<StrangeLoopResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StrangeLoopResponse>>> GetActiveProcessesAsync()
    {
        try
        {
            var processes = await _orchestrator.GetActiveProcessesAsync();
            return Ok(processes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting active processes");
            return StatusCode(500, new { error = "An error occurred while getting active processes", details = ex.Message });
        }
    }

    /// <summary>
    /// Health check endpoint
    /// </summary>
    /// <returns>Health status</returns>
    [HttpGet("health")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public ActionResult<object> Health()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTime.UtcNow,
            service = "StrangeLoopPlatform",
            version = "1.0.0"
        });
    }
} 