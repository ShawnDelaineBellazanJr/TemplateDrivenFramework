using Microsoft.AspNetCore.Mvc;
using StrangeLoopPlatform.Core.Interfaces;
using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Api.Controllers;

/// <summary>
/// Controller for managing self-improvement processes using the Process Framework
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProcessFrameworkController : ControllerBase
{
    private readonly ILogger<ProcessFrameworkController> _logger;
    private readonly IProcessOrchestrator _processOrchestrator;
    private readonly ISemanticMemoryService _memoryService;
    private readonly IDynamicCodeService _dynamicCodeService;

    public ProcessFrameworkController(
        ILogger<ProcessFrameworkController> logger,
        IProcessOrchestrator processOrchestrator,
        ISemanticMemoryService memoryService,
        IDynamicCodeService dynamicCodeService)
    {
        _logger = logger;
        _processOrchestrator = processOrchestrator;
        _memoryService = memoryService;
        _dynamicCodeService = dynamicCodeService;
    }

    /// <summary>
    /// Start a new self-improvement process
    /// </summary>
    /// <param name="request">The request to process</param>
    /// <returns>Process state with initial configuration</returns>
    [HttpPost("start")]
    public async Task<ActionResult<ProcessState>> StartProcess([FromBody] StrangeLoopRequest request)
    {
        try
        {
            _logger.LogInformation("Starting new process for request: {Requirements}", request.Requirements);
            
            var processState = await _processOrchestrator.StartProcessAsync(request);
            
            return Ok(processState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting process for request: {Requirements}", request.Requirements);
            return StatusCode(500, new { error = "Failed to start process", message = ex.Message });
        }
    }

    /// <summary>
    /// Execute the next phase of a process
    /// </summary>
    /// <param name="processId">Process identifier</param>
    /// <returns>Updated process state</returns>
    [HttpPost("{processId}/next")]
    public async Task<ActionResult<ProcessState>> ExecuteNextPhase(string processId)
    {
        try
        {
            var processState = await _processOrchestrator.GetProcessStatusAsync(processId);
            if (processState == null)
            {
                return NotFound(new { error = "Process not found", processId });
            }

            _logger.LogInformation("Executing next phase for process {ProcessId}. Current phase: {Phase}", 
                processId, processState.CurrentPhase);

            var updatedState = await _processOrchestrator.ExecuteNextPhaseAsync(processState);
            
            return Ok(updatedState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing next phase for process: {ProcessId}", processId);
            return StatusCode(500, new { error = "Failed to execute next phase", message = ex.Message });
        }
    }

    /// <summary>
    /// Execute a complete self-improvement process
    /// </summary>
    /// <param name="request">The request to process</param>
    /// <returns>Final process state with results</returns>
    [HttpPost("execute")]
    public async Task<ActionResult<ProcessState>> ExecuteCompleteProcess([FromBody] StrangeLoopRequest request)
    {
        try
        {
            _logger.LogInformation("Executing complete process for request: {Requirements}", request.Requirements);
            
            var processState = await _processOrchestrator.ExecuteCompleteProcessAsync(request);
            
            return Ok(processState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing complete process for request: {Requirements}", request.Requirements);
            return StatusCode(500, new { error = "Failed to execute complete process", message = ex.Message });
        }
    }

    /// <summary>
    /// Get the current status of a process
    /// </summary>
    /// <param name="processId">Process identifier</param>
    /// <returns>Current process state</returns>
    [HttpGet("{processId}")]
    public async Task<ActionResult<ProcessState>> GetProcessStatus(string processId)
    {
        try
        {
            var processState = await _processOrchestrator.GetProcessStatusAsync(processId);
            if (processState == null)
            {
                return NotFound(new { error = "Process not found", processId });
            }

            return Ok(processState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting process status for: {ProcessId}", processId);
            return StatusCode(500, new { error = "Failed to get process status", message = ex.Message });
        }
    }

    /// <summary>
    /// Cancel a running process
    /// </summary>
    /// <param name="processId">Process identifier</param>
    /// <returns>Success status</returns>
    [HttpPost("{processId}/cancel")]
    public async Task<ActionResult> CancelProcess(string processId)
    {
        try
        {
            var success = await _processOrchestrator.CancelProcessAsync(processId);
            if (!success)
            {
                return NotFound(new { error = "Process not found or already completed", processId });
            }

            _logger.LogInformation("Process {ProcessId} cancelled successfully", processId);
            return Ok(new { message = "Process cancelled successfully", processId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling process: {ProcessId}", processId);
            return StatusCode(500, new { error = "Failed to cancel process", message = ex.Message });
        }
    }

    /// <summary>
    /// Get all active processes
    /// </summary>
    /// <returns>List of active process states</returns>
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<ProcessState>>> GetActiveProcesses()
    {
        try
        {
            var activeProcesses = await _processOrchestrator.GetActiveProcessesAsync();
            return Ok(activeProcesses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting active processes");
            return StatusCode(500, new { error = "Failed to get active processes", message = ex.Message });
        }
    }

    /// <summary>
    /// Compile and test code dynamically
    /// </summary>
    /// <param name="request">Code compilation request</param>
    /// <returns>Compilation and execution results</returns>
    [HttpPost("compile")]
    public async Task<ActionResult<object>> CompileAndTestCode([FromBody] CodeCompilationRequest request)
    {
        try
        {
            _logger.LogInformation("Compiling code for function: {FunctionName}", request.FunctionName);

            // Compile the code
            var compilationResult = await _dynamicCodeService.CompileCodeAsync(
                request.SourceCode, 
                request.AssemblyName ?? "DynamicAssembly");

            if (!compilationResult.Success)
            {
                return BadRequest(new 
                { 
                    error = "Compilation failed", 
                    diagnostics = compilationResult.Diagnostics,
                    message = compilationResult.ErrorMessage 
                });
            }

            // Execute the code if requested
            if (request.ExecuteAfterCompile && !string.IsNullOrEmpty(request.FunctionName))
            {
                var executionResult = await _dynamicCodeService.ExecuteMethodAsync(
                    compilationResult.AssemblyPath!,
                    request.TypeName ?? "Program",
                    request.FunctionName,
                    request.Parameters?.ToArray());

                return Ok(new
                {
                    compilation = compilationResult,
                    execution = executionResult
                });
            }

            return Ok(new { compilation = compilationResult });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error compiling code for function: {FunctionName}", request.FunctionName);
            return StatusCode(500, new { error = "Failed to compile code", message = ex.Message });
        }
    }

    /// <summary>
    /// Get memory metrics and statistics
    /// </summary>
    /// <returns>Memory service metrics</returns>
    [HttpGet("memory/metrics")]
    public async Task<ActionResult<MemoryMetrics>> GetMemoryMetrics()
    {
        try
        {
            var metrics = await _memoryService.GetMemoryMetricsAsync();
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting memory metrics");
            return StatusCode(500, new { error = "Failed to get memory metrics", message = ex.Message });
        }
    }

    /// <summary>
    /// Search memory for relevant entries
    /// </summary>
    /// <param name="query">Search query</param>
    /// <param name="limit">Maximum number of results</param>
    /// <returns>Relevant memory entries</returns>
    [HttpGet("memory/search")]
    public async Task<ActionResult<List<MemoryEntry>>> SearchMemory([FromQuery] string query, [FromQuery] int limit = 10)
    {
        try
        {
            var results = await _memoryService.RetrieveMemoryAsync(query, limit);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching memory for query: {Query}", query);
            return StatusCode(500, new { error = "Failed to search memory", message = ex.Message });
        }
    }

    /// <summary>
    /// Get compilation metrics
    /// </summary>
    /// <returns>Dynamic code service metrics</returns>
    [HttpGet("compile/metrics")]
    public ActionResult<CompilationMetrics> GetCompilationMetrics()
    {
        try
        {
            var metrics = _dynamicCodeService.GetCompilationMetrics();
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting compilation metrics");
            return StatusCode(500, new { error = "Failed to get compilation metrics", message = ex.Message });
        }
    }
}

/// <summary>
/// Request model for code compilation
/// </summary>
public class CodeCompilationRequest
{
    public string SourceCode { get; set; } = string.Empty;
    public string? AssemblyName { get; set; }
    public string? TypeName { get; set; }
    public string? FunctionName { get; set; }
    public List<object>? Parameters { get; set; }
    public bool ExecuteAfterCompile { get; set; } = false;
} 