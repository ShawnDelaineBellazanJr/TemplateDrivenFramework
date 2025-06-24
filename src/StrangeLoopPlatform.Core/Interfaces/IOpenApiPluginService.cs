using Microsoft.SemanticKernel;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Service for automatically loading live Swagger/OpenAPI specifications as Semantic Kernel plugins.
/// This interface provides functionality to parse OpenAPI specs and create SK functions
/// that can invoke the documented API endpoints programmatically.
/// 
/// Key responsibilities:
/// - Parse OpenAPI/Swagger specifications
/// - Convert API operations to SK functions
/// - Handle authentication and parameter mapping
/// - Provide runtime API integration capabilities
/// 
/// This service enables the system to dynamically integrate with external APIs
/// by automatically creating SK functions from their OpenAPI documentation.
/// 
/// CRITICAL INTERFACE: Service for automatically loading OpenAPI specifications as Semantic Kernel plugins.
/// 
/// This interface is fundamental to the self-evolving AI architecture and enables:
/// - Live OpenAPI specification loading as SK plugins
/// - Dynamic API discovery and integration
/// - Runtime plugin generation from Swagger specs
/// - Automatic agent capability expansion
/// 
/// DO NOT DELETE: This interface is essential for the research plan implementation
/// and enables the core live API integration capabilities.
/// 
/// Architecture Role: Core service interface for OpenAPI plugin management
/// Research Plan Alignment: Phase 2 - Dynamic API Generation and Live Integration
/// 
/// Key Benefits:
/// - Enables agents to discover and use live APIs automatically
/// - Provides runtime API capability expansion
/// - Supports dynamic API evolution and versioning
/// - Enables self-modifying API capabilities
/// </summary>
public interface IOpenApiPluginService
{
    /// <summary>
    /// Loads an OpenAPI specification from a URL and creates a Semantic Kernel plugin.
    /// This method fetches the OpenAPI spec, parses it, and converts all operations
    /// into callable SK functions.
    /// </summary>
    /// <param name="openApiUrl">The URL to the OpenAPI specification (JSON or YAML)</param>
    /// <param name="pluginName">Optional name for the created plugin (defaults to API title)</param>
    /// <returns>A KernelPlugin containing all API operations as functions</returns>
    /// <exception cref="ArgumentNullException">Thrown when openApiUrl is null or empty</exception>
    /// <exception cref="HttpRequestException">Thrown when the OpenAPI spec cannot be fetched</exception>
    /// <exception cref="InvalidOperationException">Thrown when the OpenAPI spec is invalid</exception>
    Task<KernelPlugin> LoadOpenApiPluginAsync(string openApiUrl, string? pluginName = null);

    /// <summary>
    /// Loads an OpenAPI specification from a local file and creates a Semantic Kernel plugin.
    /// This method reads the OpenAPI spec file, parses it, and converts all operations
    /// into callable SK functions.
    /// </summary>
    /// <param name="filePath">Path to the local OpenAPI specification file</param>
    /// <param name="pluginName">Optional name for the created plugin (defaults to API title)</param>
    /// <returns>A KernelPlugin containing all API operations as functions</returns>
    /// <exception cref="ArgumentNullException">Thrown when filePath is null or empty</exception>
    /// <exception cref="FileNotFoundException">Thrown when the OpenAPI file doesn't exist</exception>
    /// <exception cref="InvalidOperationException">Thrown when the OpenAPI spec is invalid</exception>
    Task<KernelPlugin> LoadOpenApiPluginFromFileAsync(string filePath, string? pluginName = null);

    /// <summary>
    /// Loads an OpenAPI specification from raw content and creates a Semantic Kernel plugin.
    /// This method parses the provided OpenAPI content and converts all operations
    /// into callable SK functions.
    /// </summary>
    /// <param name="openApiContent">The raw OpenAPI specification content (JSON or YAML)</param>
    /// <param name="pluginName">Optional name for the created plugin (defaults to API title)</param>
    /// <returns>A KernelPlugin containing all API operations as functions</returns>
    /// <exception cref="ArgumentNullException">Thrown when openApiContent is null or empty</exception>
    /// <exception cref="InvalidOperationException">Thrown when the OpenAPI content is invalid</exception>
    Task<KernelPlugin> LoadOpenApiPluginFromContentAsync(string openApiContent, string? pluginName = null);

    /// <summary>
    /// Gets all currently loaded OpenAPI plugins managed by this service.
    /// This provides a way to discover what external APIs are currently
    /// integrated as SK functions.
    /// </summary>
    /// <returns>An enumerable of all loaded OpenAPI KernelPlugins</returns>
    IEnumerable<KernelPlugin> GetLoadedOpenApiPlugins();

    /// <summary>
    /// Removes an OpenAPI plugin from the service's management.
    /// This method unregisters the plugin and cleans up any associated resources.
    /// </summary>
    /// <param name="pluginName">The name of the plugin to remove</param>
    /// <returns>True if the plugin was found and removed, false otherwise</returns>
    /// <exception cref="ArgumentNullException">Thrown when pluginName is null or empty</exception>
    /// Loads an OpenAPI specification from a URL and registers it as a Semantic Kernel plugin.
    /// 
    /// This method is critical for enabling dynamic API integration. It:
    /// - Fetches OpenAPI specifications from any accessible URL
    /// - Parses the specification to extract endpoints and operations
    /// - Creates SK functions for each API operation
    /// - Registers the functions as a plugin for agent use
    /// 
    /// Implementation Notes:
    /// - Supports all OpenAPI 3.0+ specifications
    /// - Handles authentication and security headers
    /// - Provides error handling and validation
    /// - Enables runtime API capability discovery
    /// 
    /// Security Considerations:
    /// - Validates OpenAPI specification authenticity
    /// - Ensures proper access controls
    /// - Prevents malicious API integration
    /// 
    /// DO NOT REMOVE: Essential for dynamic API integration
    /// </summary>
    /// <param name="kernel">The Semantic Kernel instance to register the plugin with</param>
    /// <param name="openApiUrl">URL to the OpenAPI specification (e.g., /swagger/v1/swagger.json)</param>
    /// <param name="pluginName">Unique name for the plugin (e.g., "live_api", "external_service")</param>
    /// <returns>The created plugin containing all API functions from the OpenAPI spec</returns>
    Task<KernelPlugin> LoadOpenApiAsPluginAsync(Kernel kernel, string openApiUrl, string pluginName);

    /// <summary>
    /// Loads the live Swagger/OpenAPI spec from the current API and registers it as a plugin.
    /// 
    /// This method enables self-referential API integration, allowing the system to:
    /// - Discover its own API capabilities automatically
    /// - Enable agents to call any API endpoint
    /// - Support dynamic API evolution
    /// - Provide complete API self-awareness
    /// 
    /// Use Cases:
    /// - Self-modifying API capabilities
    /// - Dynamic endpoint discovery
    /// - Agent-API integration
    /// - Runtime capability expansion
    /// 
    /// Implementation Details:
    /// - Automatically constructs the Swagger URL
    /// - Handles local API discovery
    /// - Provides fallback mechanisms
    /// - Ensures proper error handling
    /// 
    /// DO NOT REMOVE: Critical for self-evolving API capabilities
    /// </summary>
    /// <param name="kernel">The Semantic Kernel instance to register the plugin with</param>
    /// <param name="baseUrl">Base URL of the API (defaults to localhost:5166)</param>
    /// <returns>The created plugin containing all live API functions</returns>
    Task<KernelPlugin> LoadLiveApiAsPluginAsync(Kernel kernel, string baseUrl = "http://localhost:5166");

    /// <summary>
    /// Refreshes the plugin with the latest OpenAPI specification.
    /// 
    /// This method enables dynamic API evolution by:
    /// - Updating plugins with new API capabilities
    /// - Supporting API versioning and changes
    /// - Enabling runtime capability updates
    /// - Providing seamless API evolution
    /// 
    /// Refresh Process:
    /// - Removes existing plugin if present
    /// - Fetches updated OpenAPI specification
    /// - Creates new plugin with updated functions
    /// - Ensures backward compatibility where possible
    /// 
    /// Safety Features:
    /// - Validates new specification before replacement
    /// - Ensures no active requests are affected
    /// - Provides rollback capabilities
    /// - Maintains plugin consistency
    /// 
    /// DO NOT REMOVE: Essential for dynamic API evolution
    /// </summary>
    /// <param name="kernel">The Semantic Kernel instance containing the plugin</param>
    /// <param name="pluginName">Name of the plugin to refresh</param>
    /// <param name="openApiUrl">URL to the updated OpenAPI specification</param>
    /// <returns>True if plugin was successfully refreshed, false if refresh failed</returns>
    Task<bool> RefreshPluginAsync(Kernel kernel, string pluginName, string openApiUrl);

    /// <summary>
    /// Gets the OpenAPI specification as JSON from a given URL.
    /// 
    /// This method provides:
    /// - Raw OpenAPI specification access
    /// - Specification validation and parsing
    /// - Error handling and retry logic
    /// - Content type validation
    /// 
    /// Validation Features:
    /// - Ensures valid JSON format
    /// - Validates OpenAPI structure
    /// - Handles HTTP errors gracefully
    /// - Provides detailed error information
    /// 
    /// Use Cases:
    /// - API specification analysis
    /// - Documentation generation
    /// - API capability discovery
    /// - Integration testing
    /// 
    /// DO NOT REMOVE: Essential for OpenAPI specification handling
    /// </summary>
    /// <param name="openApiUrl">URL to the OpenAPI specification</param>
    /// <returns>OpenAPI specification as JSON string</returns>
    Task<string> GetOpenApiSpecAsync(string openApiUrl);
} 