using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Process;
using StrangeLoopPlatform.Agents.Processes;
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
    private readonly Kernel _kernel;
    private readonly IPluginDiscoveryService _pluginDiscoveryService;
    private readonly IDynamicPluginLoader _pluginLoader;

    public ProcessFrameworkController(
        ILogger<ProcessFrameworkController> logger,
        IProcessOrchestrator processOrchestrator,
        ISemanticMemoryService memoryService,
        IDynamicCodeService dynamicCodeService,
        Kernel kernel,
        IPluginDiscoveryService pluginDiscoveryService,
        IDynamicPluginLoader pluginLoader)
    {
        _logger = logger;
        _processOrchestrator = processOrchestrator;
        _memoryService = memoryService;
        _dynamicCodeService = dynamicCodeService;
        _kernel = kernel;
        _pluginDiscoveryService = pluginDiscoveryService;
        _pluginLoader = pluginLoader;
    }

    /// <summary>
    /// Start a new self-improvement process
    /// </summary>
    /// <param name="request">The request to process</param>
    /// <returns>Initial process state</returns>
    [HttpPost("start")]
    public async Task<ActionResult<ProcessState>> StartProcess([FromBody] SelfImprovementRequest request)
    {
        try
        {
            _logger.LogInformation("Starting new process for request: {Title}", request.Title);
            
            var processState = await _processOrchestrator.StartProcessAsync(request);
            
            return Ok(processState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting process for request: {Title}", request.Title);
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
            var processState = await _processOrchestrator.GetProcessStateAsync(processId);
            if (processState == null)
            {
                return NotFound(new { error = "Process not found", processId });
            }

            _logger.LogInformation("Executing next phase for process {ProcessId}. Current phase: {Phase}", 
                processId, processState.CurrentPhase);

            var updatedState = await _processOrchestrator.ExecutePhaseAsync(processId);
            
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
    public async Task<ActionResult<ProcessState>> ExecuteCompleteProcess([FromBody] SelfImprovementRequest request)
    {
        try
        {
            _logger.LogInformation("Executing complete process for request: {Title}", request.Title);
            
            var processState = await _processOrchestrator.StartProcessAsync(request);
            var finalState = await _processOrchestrator.CompleteProcessAsync(processState.Id);
            
            return Ok(finalState);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing complete process for request: {Title}", request.Title);
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
            var processState = await _processOrchestrator.GetProcessStateAsync(processId);
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

            // Compile and execute the code
            var executionResult = await _dynamicCodeService.CompileAndExecuteAsync(
                request.SourceCode, 
                request.FunctionName ?? "Main",
                request.Parameters?.ToArray());

            if (!executionResult.IsSuccess)
            {
                return BadRequest(new 
                { 
                    error = "Compilation/execution failed", 
                    diagnostics = executionResult.Diagnostics,
                    message = executionResult.ErrorMessage 
                });
            }

            return Ok(new { execution = executionResult });
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
            var metrics = await _memoryService.GetMetricsAsync();
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting memory metrics");
            return StatusCode(500, new { error = "Failed to get memory metrics", message = ex.Message });
        }
    }

    /// <summary>
    /// Search semantic memory for relevant information
    /// </summary>
    /// <param name="query">Search query</param>
    /// <param name="limit">Maximum number of results</param>
    /// <returns>List of relevant memory entries</returns>
    [HttpGet("memory/search")]
    public async Task<ActionResult<List<MemoryEntry>>> SearchMemory([FromQuery] string query, [FromQuery] int limit = 10)
    {
        try
        {
            var results = await _memoryService.RetrieveAsync(query, limit);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching memory for query: {Query}", query);
            return StatusCode(500, new { error = "Failed to search memory", message = ex.Message });
        }
    }

    /// <summary>
    /// Get compilation performance metrics
    /// </summary>
    /// <returns>Compilation metrics and statistics</returns>
    [HttpGet("compile/metrics")]
    public async Task<ActionResult<PerformanceMetrics>> GetCompilationMetrics()
    {
        try
        {
            var metrics = await _dynamicCodeService.GetPerformanceMetricsAsync();
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting compilation metrics");
            return StatusCode(500, new { error = "Failed to get compilation metrics", message = ex.Message });
        }
    }

    [HttpPost("discover-plugins")]
    public async Task<IActionResult> DiscoverPlugins([FromBody] PluginDiscoveryRequest request)
    {
        try
        {
            var process = PluginManagementProcess.CreatePluginDiscoveryProcess();
            
            var result = await process.StartAsync(_kernel, new KernelProcessEvent
            {
                Id = "DiscoverPlugins",
                Data = request.PluginDirectory ?? "plugins"
            });

            return Ok(new { Success = true, ProcessStarted = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start plugin discovery process");
            return BadRequest(new { Success = false, Error = ex.Message });
        }
    }

    [HttpPost("generate-api-plugin")]
    public async Task<IActionResult> GenerateApiPlugin([FromBody] ApiPluginRequest request)
    {
        try
        {
            var process = PluginManagementProcess.CreatePluginDiscoveryProcess();
            
            var result = await process.StartAsync(_kernel, new KernelProcessEvent
            {
                Id = "GenerateApiPlugin",
                Data = new { ApiUrl = request.ApiUrl, OpenApiSpec = request.OpenApiSpec }
            });

            return Ok(new { Success = true, ProcessStarted = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start API plugin generation process");
            return BadRequest(new { Success = false, Error = ex.Message });
        }
    }

    [HttpPost("orchestrate-plugins")]
    public async Task<IActionResult> OrchestratePlugins([FromBody] PluginOrchestrationRequest request)
    {
        try
        {
            var process = PluginManagementProcess.CreateDynamicPluginOrchestrationProcess();
            
            var result = await process.StartAsync(_kernel, new KernelProcessEvent
            {
                Id = "StartPluginOrchestration",
                Data = request
            });

            return Ok(new { Success = true, ProcessStarted = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start plugin orchestration process");
            return BadRequest(new { Success = false, Error = ex.Message });
        }
    }

    [HttpGet("loaded-plugins")]
    public async Task<IActionResult> GetLoadedPlugins()
    {
        try
        {
            var plugins = await _pluginLoader.GetLoadedPluginsAsync(_kernel);
            return Ok(new { Success = true, Plugins = plugins });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get loaded plugins");
            return BadRequest(new { Success = false, Error = ex.Message });
        }
    }

    [HttpDelete("unload-plugin/{pluginName}")]
    public async Task<IActionResult> UnloadPlugin(string pluginName)
    {
        try
        {
            var success = await _pluginLoader.UnloadPluginAsync(_kernel, pluginName);
            return Ok(new { Success = success });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to unload plugin {PluginName}", pluginName);
            return BadRequest(new { Success = false, Error = ex.Message });
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

public record PluginDiscoveryRequest
{
    public string? PluginDirectory { get; init; }
}

public record ApiPluginRequest
{
    public string ApiUrl { get; init; } = string.Empty;
    public string? OpenApiSpec { get; init; }
}

public record PluginOrchestrationRequest
{
    public string? PluginDirectory { get; init; }
    public List<string> ApiUrls { get; init; } = new();
} 