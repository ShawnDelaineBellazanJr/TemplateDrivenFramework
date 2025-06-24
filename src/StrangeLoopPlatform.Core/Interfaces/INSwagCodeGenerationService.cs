using Microsoft.SemanticKernel;

namespace StrangeLoopPlatform.Core.Interfaces;

/// <summary>
/// Service for generating code from OpenAPI specifications using NSwag.
/// This interface provides functionality to convert OpenAPI specs into
/// executable code, supporting the "OpenAPI → Code → Test → Deploy" workflow.
/// 
/// Key responsibilities:
/// - Generate C# client code from OpenAPI specifications
/// - Create service interfaces and implementations
/// - Generate test cases for generated code
/// - Support multiple output formats and languages
/// - Integrate with the Process Framework for automated code generation
/// 
/// This service is critical for the self-evolving architecture as it enables
/// automatic code generation from API specifications, supporting the core
/// research plan objectives of autonomous code generation and deployment.
/// 
/// Architecture Role: Core service for OpenAPI-to-code generation
/// Research Plan Alignment: Phase 2 - Dynamic API Generation and Live Integration
/// </summary>
public interface INSwagCodeGenerationService
{
    /// <summary>
    /// Generates C# client code from an OpenAPI specification URL.
    /// This method fetches the OpenAPI spec and generates a complete C# client
    /// with proper error handling, authentication, and type safety.
    /// </summary>
    /// <param name="openApiUrl">URL to the OpenAPI specification</param>
    /// <param name="namespaceName">Namespace for the generated code</param>
    /// <param name="className">Name for the generated client class</param>
    /// <returns>Generated C# code as a string</returns>
    /// <exception cref="ArgumentNullException">Thrown when openApiUrl is null or empty</exception>
    /// <exception cref="HttpRequestException">Thrown when the OpenAPI spec cannot be fetched</exception>
    /// <exception cref="InvalidOperationException">Thrown when code generation fails</exception>
    Task<string> GenerateCSharpClientAsync(string openApiUrl, string namespaceName, string className);

    /// <summary>
    /// Generates C# client code from OpenAPI specification content.
    /// This method generates code directly from provided OpenAPI content,
    /// useful for local development or when the spec is already available.
    /// </summary>
    /// <param name="openApiContent">Raw OpenAPI specification content (JSON or YAML)</param>
    /// <param name="namespaceName">Namespace for the generated code</param>
    /// <param name="className">Name for the generated client class</param>
    /// <returns>Generated C# code as a string</returns>
    /// <exception cref="ArgumentNullException">Thrown when openApiContent is null or empty</exception>
    /// <exception cref="InvalidOperationException">Thrown when code generation fails</exception>
    Task<string> GenerateCSharpClientFromContentAsync(string openApiContent, string namespaceName, string className);

    /// <summary>
    /// Generates test cases for the generated client code.
    /// This method creates comprehensive unit tests that validate the generated
    /// client's functionality and integration with the target API.
    /// </summary>
    /// <param name="clientCode">The generated client code to test</param>
    /// <param name="openApiUrl">URL to the OpenAPI specification for test data</param>
    /// <param name="namespaceName">Namespace for the generated tests</param>
    /// <returns>Generated test code as a string</returns>
    /// <exception cref="ArgumentNullException">Thrown when clientCode is null or empty</exception>
    Task<string> GenerateTestCasesAsync(string clientCode, string openApiUrl, string namespaceName);

    /// <summary>
    /// Generates a complete service implementation from OpenAPI specification.
    /// This method creates both the interface and implementation classes,
    /// providing a complete service layer for the API.
    /// </summary>
    /// <param name="openApiUrl">URL to the OpenAPI specification</param>
    /// <param name="namespaceName">Namespace for the generated code</param>
    /// <param name="serviceName">Name for the generated service</param>
    /// <returns>Generated service code (interface and implementation) as a string</returns>
    /// <exception cref="ArgumentNullException">Thrown when openApiUrl is null or empty</exception>
    Task<string> GenerateServiceImplementationAsync(string openApiUrl, string namespaceName, string serviceName);

    /// <summary>
    /// Validates generated code by attempting compilation.
    /// This method ensures the generated code is syntactically correct and
    /// can be compiled successfully.
    /// </summary>
    /// <param name="generatedCode">The code to validate</param>
    /// <returns>Validation result with compilation status and any errors</returns>
    /// <exception cref="ArgumentNullException">Thrown when generatedCode is null or empty</exception>
    Task<CodeGenerationResult> ValidateGeneratedCodeAsync(string generatedCode);

    /// <summary>
    /// Generates code with custom NSwag settings.
    /// This method allows fine-grained control over the code generation process,
    /// including output format, naming conventions, and code style.
    /// </summary>
    /// <param name="openApiUrl">URL to the OpenAPI specification</param>
    /// <param name="settings">Custom NSwag generation settings</param>
    /// <returns>Generated code as a string</returns>
    /// <exception cref="ArgumentNullException">Thrown when openApiUrl or settings is null</exception>
    Task<string> GenerateCodeWithSettingsAsync(string openApiUrl, NSwagGenerationSettings settings);

    /// <summary>
    /// Gets the current NSwag version and supported features.
    /// This method provides information about the code generation capabilities
    /// and any limitations of the current NSwag version.
    /// </summary>
    /// <returns>NSwag version information and supported features</returns>
    Task<NSwagVersionInfo> GetVersionInfoAsync();
}

/// <summary>
/// Result of code generation validation including compilation status and errors.
/// </summary>
public class CodeGenerationResult
{
    /// <summary>
    /// Whether the generated code compiled successfully
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Any compilation errors or warnings
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Any compilation warnings
    /// </summary>
    public List<string> Warnings { get; set; } = new();

    /// <summary>
    /// The generated code that was validated
    /// </summary>
    public string GeneratedCode { get; set; } = string.Empty;
}

/// <summary>
/// Custom settings for NSwag code generation.
/// </summary>
public class NSwagGenerationSettings
{
    /// <summary>
    /// Target language for code generation
    /// </summary>
    public string TargetLanguage { get; set; } = "CSharp";

    /// <summary>
    /// Namespace for the generated code
    /// </summary>
    public string Namespace { get; set; } = "Generated";

    /// <summary>
    /// Class name for the generated client
    /// </summary>
    public string ClassName { get; set; } = "ApiClient";

    /// <summary>
    /// Whether to generate synchronous methods
    /// </summary>
    public bool GenerateSyncMethods { get; set; } = false;

    /// <summary>
    /// Whether to generate exception classes
    /// </summary>
    public bool GenerateExceptionClasses { get; set; } = true;

    /// <summary>
    /// Whether to generate data contracts
    /// </summary>
    public bool GenerateDataContracts { get; set; } = true;

    /// <summary>
    /// Whether to generate nullable reference types
    /// </summary>
    public bool GenerateNullableReferenceTypes { get; set; } = true;

    /// <summary>
    /// Custom type mappings
    /// </summary>
    public Dictionary<string, string> TypeMappings { get; set; } = new();
}

/// <summary>
/// Information about the NSwag version and supported features.
/// </summary>
public class NSwagVersionInfo
{
    /// <summary>
    /// NSwag version number
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Supported OpenAPI versions
    /// </summary>
    public List<string> SupportedOpenApiVersions { get; set; } = new();

    /// <summary>
    /// Supported target languages
    /// </summary>
    public List<string> SupportedLanguages { get; set; } = new();

    /// <summary>
    /// Available code generation features
    /// </summary>
    public List<string> Features { get; set; } = new();
} 