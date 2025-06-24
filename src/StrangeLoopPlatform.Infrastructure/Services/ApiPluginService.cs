using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using StrangeLoopPlatform.Core.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace StrangeLoopPlatform.Infrastructure.Services;

/// <summary>
/// Service that wraps the Web API as a Semantic Kernel plugin for dynamic endpoint management
/// </summary>
public class ApiPluginService : IApiPluginService
{
    private readonly ILogger<ApiPluginService> _logger;
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, DynamicEndpoint> _dynamicEndpoints = new();
    private readonly object _lock = new();

    public ApiPluginService(ILogger<ApiPluginService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<KernelPlugin> RegisterApiAsPluginAsync(Kernel kernel)
    {
        try
        {
            _logger.LogInformation("Registering Web API as Semantic Kernel plugin");

            // Create functions for existing API endpoints
            var functions = new List<KernelFunction>
            {
                // Health check endpoint
                kernel.CreateFunctionFromMethod(
                    method: HealthCheckAsync,
                    description: "Check the health status of the API",
                    functionName: "health_check"),

                // Process Framework endpoints
                kernel.CreateFunctionFromMethod(
                    method: StartProcessAsync,
                    description: "Start a new self-improvement process",
                    functionName: "start_process"),

                kernel.CreateFunctionFromMethod(
                    method: ExecuteProcessAsync,
                    description: "Execute a self-improvement process",
                    functionName: "execute_process"),

                kernel.CreateFunctionFromMethod(
                    method: GetProcessStatusAsync,
                    description: "Get the status of a running process",
                    functionName: "get_process_status"),

                // Dynamic code compilation
                kernel.CreateFunctionFromMethod(
                    method: CompileCodeAsync,
                    description: "Compile and test C# code dynamically",
                    functionName: "compile_code"),

                // Memory operations
                kernel.CreateFunctionFromMethod(
                    method: GetMemoryMetricsAsync,
                    description: "Get semantic memory metrics",
                    functionName: "get_memory_metrics"),

                kernel.CreateFunctionFromMethod(
                    method: StoreMemoryAsync,
                    description: "Store information in semantic memory",
                    functionName: "store_memory"),

                kernel.CreateFunctionFromMethod(
                    method: RetrieveMemoryAsync,
                    description: "Retrieve information from semantic memory",
                    functionName: "retrieve_memory")
            };

            // Create the plugin
            var plugin = kernel.ImportPluginFromFunctions("strange_loop_api", functions);

            _logger.LogInformation("Successfully registered Web API as plugin with {FunctionCount} functions", functions.Count);
            return plugin;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to register Web API as plugin");
            throw;
        }
    }

    public async Task<KernelFunction> AddDynamicEndpointAsync(
        Kernel kernel,
        string endpointName,
        string endpointPath,
        string httpMethod,
        string description,
        Dictionary<string, object>? parameters = null)
    {
        try
        {
            _logger.LogInformation("Adding dynamic endpoint: {EndpointName} at {Path}", endpointName, endpointPath);

            // Create the dynamic function
            var function = kernel.CreateFunctionFromMethod(
                method: (string input) => CallDynamicEndpointAsync(endpointPath, httpMethod, input),
                description: description,
                functionName: endpointName);

            // Store the endpoint metadata
            lock (_lock)
            {
                _dynamicEndpoints[endpointName] = new DynamicEndpoint
                {
                    Name = endpointName,
                    Path = endpointPath,
                    HttpMethod = httpMethod,
                    Description = description,
                    Parameters = parameters ?? new Dictionary<string, object>()
                };
            }

            _logger.LogInformation("Successfully added dynamic endpoint: {EndpointName}", endpointName);
            return function;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add dynamic endpoint: {EndpointName}", endpointName);
            throw;
        }
    }

    public async Task<string> GenerateOpenApiSpecAsync(IEnumerable<DynamicEndpoint> endpoints)
    {
        try
        {
            var openApiDoc = new
            {
                openapi = "3.0.1",
                info = new
                {
                    title = "StrangeLoop Platform API",
                    version = "1.0.0",
                    description = "Dynamic API generated by StrangeLoop Platform"
                },
                paths = new Dictionary<string, object>
                {
                    ["/health"] = new
                    {
                        get = new
                        {
                            summary = "Health check",
                            responses = new Dictionary<string, object>
                            {
                                ["200"] = new { description = "OK" }
                            }
                        }
                    }
                }
            };

            // Add dynamic endpoints
            foreach (var endpoint in endpoints.Where(e => e.IsActive))
            {
                var operation = new
                {
                    summary = endpoint.Description,
                    parameters = endpoint.Parameters.Select(p => new
                    {
                        name = p.Key,
                        @in = "query",
                        schema = new { type = "string" }
                    }).ToList(),
                    responses = new Dictionary<string, object>
                    {
                        ["200"] = new { description = "Success" },
                        ["400"] = new { description = "Bad Request" },
                        ["500"] = new { description = "Internal Server Error" }
                    }
                };

                var pathItem = new Dictionary<string, object>
                {
                    [endpoint.HttpMethod.ToLower()] = operation
                };

                openApiDoc.paths[endpoint.Path] = pathItem;
            }

            return JsonSerializer.Serialize(openApiDoc, new JsonSerializerOptions { WriteIndented = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate OpenAPI specification");
            throw;
        }
    }

    public async Task<bool> RemoveDynamicEndpointAsync(Kernel kernel, string endpointName)
    {
        try
        {
            lock (_lock)
            {
                if (_dynamicEndpoints.Remove(endpointName))
                {
                    _logger.LogInformation("Removed dynamic endpoint: {EndpointName}", endpointName);
                    return true;
                }
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to remove dynamic endpoint: {EndpointName}", endpointName);
            return false;
        }
    }

    // API function implementations
    private async Task<string> HealthCheckAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/health");
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed");
            return JsonSerializer.Serialize(new { status = "unhealthy", error = ex.Message });
        }
    }

    private async Task<string> StartProcessAsync(string requirements)
    {
        try
        {
            var request = new { requirements, complexity = 1, maxIterations = 3 };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("/api/ProcessFramework/start", content);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start process");
            return JsonSerializer.Serialize(new { error = ex.Message });
        }
    }

    private async Task<string> ExecuteProcessAsync(string processId)
    {
        try
        {
            var response = await _httpClient.PostAsync($"/api/ProcessFramework/execute/{processId}", null);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to execute process: {ProcessId}", processId);
            return JsonSerializer.Serialize(new { error = ex.Message });
        }
    }

    private async Task<string> GetProcessStatusAsync(string processId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/ProcessFramework/status/{processId}");
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get process status: {ProcessId}", processId);
            return JsonSerializer.Serialize(new { error = ex.Message });
        }
    }

    private async Task<string> CompileCodeAsync(string code)
    {
        try
        {
            var request = new { code, testCases = new string[0] };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("/api/ProcessFramework/compile", content);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to compile code");
            return JsonSerializer.Serialize(new { error = ex.Message });
        }
    }

    private async Task<string> GetMemoryMetricsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/ProcessFramework/memory/metrics");
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get memory metrics");
            return JsonSerializer.Serialize(new { error = ex.Message });
        }
    }

    private async Task<string> StoreMemoryAsync(string key, string text)
    {
        try
        {
            var request = new { key, text };
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("/api/ProcessFramework/memory/store", content);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to store memory");
            return JsonSerializer.Serialize(new { error = ex.Message });
        }
    }

    private async Task<string> RetrieveMemoryAsync(string key)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/ProcessFramework/memory/retrieve/{key}");
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve memory: {Key}", key);
            return JsonSerializer.Serialize(new { error = ex.Message });
        }
    }

    private async Task<string> CallDynamicEndpointAsync(string path, string method, string input)
    {
        try
        {
            var content = new StringContent(input, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod(method), path) { Content = content };
            
            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to call dynamic endpoint: {Path}", path);
            return JsonSerializer.Serialize(new { error = ex.Message });
        }
    }
} 