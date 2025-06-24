using Microsoft.SemanticKernel;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Service for automatically loading OpenAPI specifications as Semantic Kernel plugins
/// </summary>
public interface IOpenApiPluginService
{
    /// <summary>
    /// Loads an OpenAPI specification from a URL and registers it as a plugin
    /// </summary>
    /// <param name="kernel">The kernel to register the plugin with</param>
    /// <param name="openApiUrl">URL to the OpenAPI specification (e.g., /swagger/v1/swagger.json)</param>
    /// <param name="pluginName">Name for the plugin</param>
    /// <returns>The created plugin</returns>
    Task<KernelPlugin> LoadOpenApiAsPluginAsync(Kernel kernel, string openApiUrl, string pluginName);

    /// <summary>
    /// Loads the live Swagger/OpenAPI spec from the current API and registers it as a plugin
    /// </summary>
    /// <param name="kernel">The kernel to register the plugin with</param>
    /// <param name="baseUrl">Base URL of the API</param>
    /// <returns>The created plugin</returns>
    Task<KernelPlugin> LoadLiveApiAsPluginAsync(Kernel kernel, string baseUrl = "http://localhost:5166");

    /// <summary>
    /// Refreshes the plugin with the latest OpenAPI specification
    /// </summary>
    /// <param name="kernel">The kernel</param>
    /// <param name="pluginName">Name of the plugin to refresh</param>
    /// <param name="openApiUrl">URL to the updated OpenAPI specification</param>
    /// <returns>True if refreshed successfully</returns>
    Task<bool> RefreshPluginAsync(Kernel kernel, string pluginName, string openApiUrl);

    /// <summary>
    /// Gets the OpenAPI specification as JSON
    /// </summary>
    /// <param name="openApiUrl">URL to the OpenAPI specification</param>
    /// <returns>OpenAPI specification as JSON string</returns>
    Task<string> GetOpenApiSpecAsync(string openApiUrl);
} 