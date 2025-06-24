using Microsoft.SemanticKernel;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Service for managing API endpoints as Semantic Kernel plugins
/// </summary>
public interface IApiPluginService
{
    /// <summary>
    /// Registers the entire Web API as a Semantic Kernel plugin
    /// </summary>
    /// <param name="kernel">The kernel to register the plugin with</param>
    /// <returns>The created plugin</returns>
    Task<KernelPlugin> RegisterApiAsPluginAsync(Kernel kernel);

    /// <summary>
    /// Dynamically adds a new API endpoint as a plugin function
    /// </summary>
    /// <param name="kernel">The kernel to register the function with</param>
    /// <param name="endpointName">Name of the endpoint</param>
    /// <param name="endpointPath">HTTP path for the endpoint</param>
    /// <param name="httpMethod">HTTP method (GET, POST, etc.)</param>
    /// <param name="description">Description of the endpoint</param>
    /// <param name="parameters">Parameter definitions</param>
    /// <returns>The created function</returns>
    Task<KernelFunction> AddDynamicEndpointAsync(
        Kernel kernel,
        string endpointName,
        string endpointPath,
        string httpMethod,
        string description,
        Dictionary<string, object>? parameters = null);

    /// <summary>
    /// Generates OpenAPI specification for dynamically created endpoints
    /// </summary>
    /// <param name="endpoints">List of dynamic endpoints</param>
    /// <returns>OpenAPI specification as JSON</returns>
    Task<string> GenerateOpenApiSpecAsync(IEnumerable<DynamicEndpoint> endpoints);

    /// <summary>
    /// Removes a dynamic endpoint from the plugin
    /// </summary>
    /// <param name="kernel">The kernel</param>
    /// <param name="endpointName">Name of the endpoint to remove</param>
    /// <returns>True if removed successfully</returns>
    Task<bool> RemoveDynamicEndpointAsync(Kernel kernel, string endpointName);
}

/// <summary>
/// Represents a dynamically created API endpoint
/// </summary>
public class DynamicEndpoint
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string HttpMethod { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, object> Parameters { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
} 