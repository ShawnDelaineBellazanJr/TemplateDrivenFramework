using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using StrangeLoopPlatform.Core.Interfaces;
using System.Text.Json;

namespace StrangeLoopPlatform.Infrastructure.Services;

/// <summary>
/// Service that automatically loads OpenAPI specifications as Semantic Kernel plugins
/// </summary>
public class OpenApiPluginService : IOpenApiPluginService
{
    private readonly ILogger<OpenApiPluginService> _logger;
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, string> _pluginUrls = new();

    public OpenApiPluginService(ILogger<OpenApiPluginService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<KernelPlugin> LoadOpenApiAsPluginAsync(Kernel kernel, string openApiUrl, string pluginName)
    {
        try
        {
            _logger.LogInformation("Loading OpenAPI specification from {OpenApiUrl} as plugin {PluginName}", openApiUrl, pluginName);

            // Get the OpenAPI specification
            var openApiSpec = await GetOpenApiSpecAsync(openApiUrl);

            // Parse the OpenAPI spec and create functions manually
            var functions = await ParseOpenApiAndCreateFunctionsAsync(kernel, openApiSpec, openApiUrl);

            // Create the plugin from functions
            var plugin = kernel.ImportPluginFromFunctions(pluginName, functions);

            // Store the URL for potential refresh
            _pluginUrls[pluginName] = openApiUrl;

            _logger.LogInformation("Successfully loaded OpenAPI specification as plugin {PluginName} with {FunctionCount} functions", pluginName, functions.Count);
            return plugin;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load OpenAPI specification as plugin {PluginName}", pluginName);
            throw;
        }
    }

    public async Task<KernelPlugin> LoadLiveApiAsPluginAsync(Kernel kernel, string baseUrl = "http://localhost:5166")
    {
        try
        {
            var openApiUrl = $"{baseUrl.TrimEnd('/')}/swagger/v1/swagger.json";
            var pluginName = "live_api";

            _logger.LogInformation("Loading live API from {OpenApiUrl} as plugin {PluginName}", openApiUrl, pluginName);

            return await LoadOpenApiAsPluginAsync(kernel, openApiUrl, pluginName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load live API as plugin");
            throw;
        }
    }

    public async Task<bool> RefreshPluginAsync(Kernel kernel, string pluginName, string openApiUrl)
    {
        try
        {
            _logger.LogInformation("Refreshing plugin {PluginName} with updated OpenAPI specification", pluginName);

            // Remove the existing plugin if it exists
            if (kernel.Plugins.TryGetPlugin(pluginName, out var existingPlugin))
            {
                // Note: In current SK version, plugins are immutable, so we need to remove and recreate
                // This is a limitation - in a production system, you might want to implement a custom plugin manager
                _logger.LogWarning("Plugin {PluginName} already exists. Removing and recreating...", pluginName);
            }

            // Load the updated plugin
            await LoadOpenApiAsPluginAsync(kernel, openApiUrl, pluginName);

            _logger.LogInformation("Successfully refreshed plugin {PluginName}", pluginName);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to refresh plugin {PluginName}", pluginName);
            return false;
        }
    }

    public async Task<string> GetOpenApiSpecAsync(string openApiUrl)
    {
        try
        {
            _logger.LogInformation("Fetching OpenAPI specification from {OpenApiUrl}", openApiUrl);

            var response = await _httpClient.GetAsync(openApiUrl);
            response.EnsureSuccessStatusCode();

            var openApiSpec = await response.Content.ReadAsStringAsync();

            // Validate that it's valid JSON
            try
            {
                JsonDocument.Parse(openApiSpec);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Invalid JSON in OpenAPI specification: {ex.Message}");
            }

            _logger.LogInformation("Successfully fetched OpenAPI specification from {OpenApiUrl}", openApiUrl);
            return openApiSpec;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch OpenAPI specification from {OpenApiUrl}", openApiUrl);
            throw;
        }
    }

    private async Task<List<KernelFunction>> ParseOpenApiAndCreateFunctionsAsync(Kernel kernel, string openApiSpec, string baseUrl)
    {
        var functions = new List<KernelFunction>();
        
        try
        {
            var openApiDoc = JsonDocument.Parse(openApiSpec);
            var paths = openApiDoc.RootElement.GetProperty("paths");

            foreach (var path in paths.EnumerateObject())
            {
                var pathValue = path.Value;
                var pathString = path.Name;

                foreach (var operation in pathValue.EnumerateObject())
                {
                    var method = operation.Name.ToUpper();
                    var operationValue = operation.Value;

                    // Extract operation details
                    var summary = operationValue.TryGetProperty("summary", out var summaryProp) 
                        ? summaryProp.GetString() ?? "API endpoint" 
                        : "API endpoint";

                    var operationId = operationValue.TryGetProperty("operationId", out var opIdProp) 
                        ? opIdProp.GetString() 
                        : $"{method.ToLower()}_{pathString.Replace("/", "_").Replace("{", "").Replace("}", "")}";

                    // Create function that calls the API endpoint
                    var function = kernel.CreateFunctionFromMethod(
                        method: (string input) => CallApiEndpointAsync($"{baseUrl.TrimEnd('/')}{pathString}", method, input),
                        description: summary,
                        functionName: operationId);

                    functions.Add(function);
                }
            }

            _logger.LogInformation("Parsed OpenAPI specification and created {FunctionCount} functions", functions.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to parse OpenAPI specification");
            throw;
        }

        return functions;
    }

    private async Task<string> CallApiEndpointAsync(string url, string method, string input)
    {
        try
        {
            var content = new StringContent(input, System.Text.Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod(method), url) { Content = content };
            
            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Serialize(new
            {
                statusCode = (int)response.StatusCode,
                content = result,
                headers = response.Headers.ToDictionary(h => h.Key, h => h.Value.FirstOrDefault())
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to call API endpoint: {Url}", url);
            return JsonSerializer.Serialize(new { error = ex.Message });
        }
    }

    // IOpenApiPluginService required interface stubs
    public async Task<KernelPlugin> LoadOpenApiPluginAsync(string openApiUrl, string? pluginName = null)
    {
        _logger.LogWarning("LoadOpenApiPluginAsync is not implemented. Returning empty plugin.");
        return KernelPluginFactory.CreateFromFunctions(pluginName ?? "stub_openapi_plugin", new List<KernelFunction>());
    }

    public async Task<KernelPlugin> LoadOpenApiPluginFromFileAsync(string filePath, string? pluginName = null)
    {
        _logger.LogWarning("LoadOpenApiPluginFromFileAsync is not implemented. Returning empty plugin.");
        return KernelPluginFactory.CreateFromFunctions(pluginName ?? "stub_openapi_plugin_file", new List<KernelFunction>());
    }

    public async Task<KernelPlugin> LoadOpenApiPluginFromContentAsync(string openApiContent, string? pluginName = null)
    {
        _logger.LogWarning("LoadOpenApiPluginFromContentAsync is not implemented. Returning empty plugin.");
        return KernelPluginFactory.CreateFromFunctions(pluginName ?? "stub_openapi_plugin_content", new List<KernelFunction>());
    }

    public IEnumerable<KernelPlugin> GetLoadedOpenApiPlugins()
    {
        _logger.LogWarning("GetLoadedOpenApiPlugins is not implemented. Returning empty list.");
        return Enumerable.Empty<KernelPlugin>();
    }
} 