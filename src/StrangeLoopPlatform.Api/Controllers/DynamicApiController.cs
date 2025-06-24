using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using StrangeLoopPlatform.Core.Interfaces;

namespace StrangeLoopPlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DynamicApiController : ControllerBase
{
    private readonly ILogger<DynamicApiController> _logger;
    private readonly Kernel _kernel;
    private readonly IApiPluginService _apiPluginService;

    public DynamicApiController(
        ILogger<DynamicApiController> logger,
        Kernel kernel,
        IApiPluginService apiPluginService)
    {
        _logger = logger;
        _kernel = kernel;
        _apiPluginService = apiPluginService;
    }

    /// <summary>
    /// Get all available plugin functions
    /// </summary>
    [HttpGet("plugins")]
    public async Task<ActionResult<object>> GetPlugins()
    {
        try
        {
            var plugins = _kernel.Plugins.ToList();
            var result = plugins.Select(p => new
            {
                Name = p.Name,
                Description = p.Description,
                Functions = p.Select(f => new
                {
                    Name = f.Name,
                    Description = f.Description,
                    Parameters = f.Metadata.Parameters.Select(param => new
                    {
                        Name = param.Name,
                        Description = param.Description,
                        Type = param.ParameterType?.Name ?? "string",
                        IsRequired = param.IsRequired
                    }).ToList()
                }).ToList()
            }).ToList();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get plugins");
            return StatusCode(500, new { error = "Failed to get plugins", message = ex.Message });
        }
    }

    /// <summary>
    /// Create a new dynamic API endpoint
    /// </summary>
    [HttpPost("endpoints")]
    public async Task<ActionResult<object>> CreateDynamicEndpoint([FromBody] CreateEndpointRequest request)
    {
        try
        {
            _logger.LogInformation("Creating dynamic endpoint: {Name} at {Path}", request.Name, request.Path);

            var function = await _apiPluginService.AddDynamicEndpointAsync(
                _kernel,
                request.Name,
                request.Path,
                request.HttpMethod,
                request.Description,
                request.Parameters);

            return Ok(new
            {
                message = "Dynamic endpoint created successfully",
                endpoint = new
                {
                    Name = request.Name,
                    Path = request.Path,
                    HttpMethod = request.HttpMethod,
                    Description = request.Description,
                    FunctionName = function.Name
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create dynamic endpoint: {Name}", request.Name);
            return StatusCode(500, new { error = "Failed to create dynamic endpoint", message = ex.Message });
        }
    }

    /// <summary>
    /// Generate OpenAPI specification for all endpoints
    /// </summary>
    [HttpGet("openapi")]
    public async Task<ActionResult<string>> GenerateOpenApiSpec()
    {
        try
        {
            // Get all dynamic endpoints (in a real implementation, you'd store these)
            var endpoints = new List<DynamicEndpoint>
            {
                new DynamicEndpoint
                {
                    Name = "example_endpoint",
                    Path = "/api/example",
                    HttpMethod = "POST",
                    Description = "Example dynamic endpoint"
                }
            };

            var openApiSpec = await _apiPluginService.GenerateOpenApiSpecAsync(endpoints);
            return Content(openApiSpec, "application/json");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate OpenAPI specification");
            return StatusCode(500, new { error = "Failed to generate OpenAPI specification", message = ex.Message });
        }
    }

    /// <summary>
    /// Invoke a plugin function
    /// </summary>
    [HttpPost("invoke/{pluginName}/{functionName}")]
    public async Task<ActionResult<object>> InvokeFunction(
        string pluginName,
        string functionName,
        [FromBody] Dictionary<string, object> arguments)
    {
        try
        {
            _logger.LogInformation("Invoking function: {PluginName}.{FunctionName}", pluginName, functionName);

            if (!_kernel.Plugins.TryGetPlugin(pluginName, out var plugin))
            {
                return NotFound(new { error = $"Plugin '{pluginName}' not found" });
            }

            if (!plugin.TryGetFunction(functionName, out var function))
            {
                return NotFound(new { error = $"Function '{functionName}' not found in plugin '{pluginName}'" });
            }

            var kernelArguments = new KernelArguments();
            foreach (var arg in arguments)
            {
                kernelArguments.Add(arg.Key, arg.Value);
            }

            var result = await _kernel.InvokeAsync(function, kernelArguments);
            var resultValue = result.GetValue<object>();

            return Ok(new
            {
                plugin = pluginName,
                function = functionName,
                result = resultValue,
                metadata = result.Metadata
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to invoke function: {PluginName}.{FunctionName}", pluginName, functionName);
            return StatusCode(500, new { error = "Failed to invoke function", message = ex.Message });
        }
    }

    /// <summary>
    /// Remove a dynamic endpoint
    /// </summary>
    [HttpDelete("endpoints/{endpointName}")]
    public async Task<ActionResult<object>> RemoveDynamicEndpoint(string endpointName)
    {
        try
        {
            var success = await _apiPluginService.RemoveDynamicEndpointAsync(_kernel, endpointName);
            
            if (success)
            {
                return Ok(new { message = $"Dynamic endpoint '{endpointName}' removed successfully" });
            }
            else
            {
                return NotFound(new { error = $"Dynamic endpoint '{endpointName}' not found" });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to remove dynamic endpoint: {EndpointName}", endpointName);
            return StatusCode(500, new { error = "Failed to remove dynamic endpoint", message = ex.Message });
        }
    }

    [HttpPost("reload-openapi")]
    public async Task<ActionResult<object>> ReloadOpenApiPlugin([FromServices] Kernel kernel, [FromServices] IOpenApiPluginService openApiPluginService)
    {
        try
        {
            _logger.LogInformation("Reloading live OpenAPI plugin...");
            var plugin = await openApiPluginService.LoadLiveApiAsPluginAsync(kernel);
            return Ok(new { message = "Live OpenAPI plugin loaded successfully", plugin = plugin.Name });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reload live OpenAPI plugin");
            return StatusCode(500, new { error = "Failed to reload live OpenAPI plugin", message = ex.Message });
        }
    }
}

public class CreateEndpointRequest
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string HttpMethod { get; set; } = "POST";
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, object>? Parameters { get; set; }
} 