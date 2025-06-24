using Microsoft.SemanticKernel;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Service for managing API endpoints as Semantic Kernel plugins.
/// This interface provides functionality to dynamically expose API endpoints
/// as SK functions, enabling runtime discovery and invocation of API capabilities.
/// 
/// Key responsibilities:
/// - Convert API controllers to SK plugins
/// - Manage dynamic endpoint registration
/// - Provide plugin metadata and function discovery
/// - Enable runtime API capability exposure
/// 
/// This service is critical for the self-evolving architecture as it allows
/// the system to dynamically expose its own capabilities as callable functions.
/// </summary>
public interface IApiPluginService
{
    /// <summary>
    /// Creates a Semantic Kernel plugin from an API controller type.
    /// This method analyzes the controller's endpoints and converts them
    /// into SK functions that can be invoked programmatically.
    /// </summary>
    /// <param name="controllerType">The type of the API controller to convert</param>
    /// <returns>A KernelPlugin containing the controller's endpoints as functions</returns>
    /// <exception cref="ArgumentNullException">Thrown when controllerType is null</exception>
    /// <exception cref="ArgumentException">Thrown when controllerType is not a valid controller</exception>
    KernelPlugin CreatePluginFromController(Type controllerType);

    /// <summary>
    /// Gets all currently registered plugins managed by this service.
    /// This provides a way to discover what API capabilities are currently
    /// exposed as SK functions.
    /// </summary>
    /// <returns>An enumerable of all registered KernelPlugins</returns>
    IEnumerable<KernelPlugin> GetRegisteredPlugins();

    /// <summary>
    /// Registers a plugin with the service for management and discovery.
    /// This method stores the plugin reference for later retrieval and
    /// ensures proper lifecycle management.
    /// </summary>
    /// <param name="plugin">The KernelPlugin to register</param>
    /// <exception cref="ArgumentNullException">Thrown when plugin is null</exception>
    void RegisterPlugin(KernelPlugin plugin);

    /// <summary>
    /// Removes a plugin from the service's management.
    /// This method unregisters the plugin and cleans up any associated resources.
    /// </summary>
    /// <param name="pluginName">The name of the plugin to unregister</param>
    /// <returns>True if the plugin was found and removed, false otherwise</returns>
    /// <exception cref="ArgumentNullException">Thrown when pluginName is null or empty</exception>
    bool UnregisterPlugin(string pluginName);

    /// <summary>
    /// Registers the entire Web API as a Semantic Kernel plugin.
    /// 
    /// This method is critical for enabling agents to call API functions directly.
    /// It creates a bridge between the traditional Web API and the Semantic Kernel
    /// agent framework, enabling dynamic function calling capabilities.
    /// 
    /// Implementation Notes:
    /// - Creates functions for all major API endpoints
    /// - Handles HTTP client configuration and error handling
    /// - Integrates with the kernel's plugin system
    /// - Enables runtime function discovery and invocation
    /// 
    /// DO NOT REMOVE: Essential for agent-API integration
    /// </summary>
    /// <param name="kernel">The Semantic Kernel instance to register the plugin with</param>
    /// <returns>The created plugin containing all API functions</returns>
    Task<KernelPlugin> RegisterApiAsPluginAsync(Kernel kernel);

    /// <summary>
    /// Dynamically adds a new API endpoint as a plugin function.
    /// 
    /// This method enables runtime API extension, allowing the system to:
    /// - Create new endpoints on-the-fly
    /// - Expose new capabilities to agents immediately
    /// - Support dynamic API evolution
    /// - Enable self-modifying API capabilities
    /// 
    /// Security Considerations:
    /// - Validates endpoint parameters and security
    /// - Ensures proper access controls
    /// - Prevents malicious endpoint creation
    /// 
    /// DO NOT REMOVE: Critical for dynamic API evolution
    /// </summary>
    /// <param name="kernel">The kernel to register the function with</param>
    /// <param name="endpointName">Name of the endpoint (must be unique)</param>
    /// <param name="endpointPath">HTTP path for the endpoint (e.g., "/api/custom")</param>
    /// <param name="httpMethod">HTTP method (GET, POST, PUT, DELETE)</param>
    /// <param name="description">Human-readable description of the endpoint</param>
    /// <param name="parameters">Optional parameter definitions for the endpoint</param>
    /// <returns>The created function that can be invoked by agents</returns>
    Task<KernelFunction> AddDynamicEndpointAsync(
        Kernel kernel,
        string endpointName,
        string endpointPath,
        string httpMethod,
        string description,
        Dictionary<string, object>? parameters = null);

    /// <summary>
    /// Generates OpenAPI specification for dynamically created endpoints.
    /// 
    /// This method provides:
    /// - Live OpenAPI documentation for dynamic endpoints
    /// - API discovery and documentation
    /// - Integration with API management tools
    /// - Runtime API specification generation
    /// 
    /// Use Cases:
    /// - API documentation generation
    /// - Client SDK generation
    /// - API testing and validation
    /// - Integration with API gateways
    /// 
    /// DO NOT REMOVE: Essential for API documentation and discovery
    /// </summary>
    /// <param name="endpoints">Collection of dynamic endpoints to document</param>
    /// <returns>OpenAPI specification as JSON string</returns>
    Task<string> GenerateOpenApiSpecAsync(IEnumerable<DynamicEndpoint> endpoints);

    /// <summary>
    /// Removes a dynamic endpoint from the plugin system.
    /// 
    /// This method enables:
    /// - Runtime endpoint cleanup
    /// - Dynamic API evolution
    /// - Resource management
    /// - Security endpoint removal
    /// 
    /// Safety Features:
    /// - Validates endpoint exists before removal
    /// - Ensures no active requests are affected
    /// - Provides feedback on removal success
    /// 
    /// DO NOT REMOVE: Critical for dynamic API management
    /// </summary>
    /// <param name="kernel">The kernel containing the endpoint</param>
    /// <param name="endpointName">Name of the endpoint to remove</param>
    /// <returns>True if endpoint was successfully removed, false if not found</returns>
    Task<bool> RemoveDynamicEndpointAsync(Kernel kernel, string endpointName);
}

/// <summary>
/// Represents a dynamically created API endpoint with full metadata.
/// 
/// This class is essential for:
/// - Tracking dynamic endpoints
/// - Managing endpoint lifecycle
/// - Providing endpoint metadata
/// - Supporting API documentation
/// 
/// DO NOT DELETE: Required for dynamic endpoint management
/// </summary>
public class DynamicEndpoint
{
    /// <summary>
    /// Unique identifier for the endpoint
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// HTTP path where the endpoint is accessible
    /// </summary>
    public string Path { get; set; } = string.Empty;
    
    /// <summary>
    /// HTTP method (GET, POST, PUT, DELETE)
    /// </summary>
    public string HttpMethod { get; set; } = string.Empty;
    
    /// <summary>
    /// Human-readable description of the endpoint's purpose
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Parameter definitions for the endpoint
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new();
    
    /// <summary>
    /// When the endpoint was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Whether the endpoint is currently active and accessible
    /// </summary>
    public bool IsActive { get; set; } = true;
} 