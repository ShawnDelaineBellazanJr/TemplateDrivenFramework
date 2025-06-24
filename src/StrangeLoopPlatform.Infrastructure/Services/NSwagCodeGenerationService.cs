using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using StrangeLoopPlatform.Core.Interfaces;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace StrangeLoopPlatform.Infrastructure.Services;

/// <summary>
/// Implementation of INSwagCodeGenerationService using NSwag for OpenAPI to code generation.
/// </summary>
public class NSwagCodeGenerationService : INSwagCodeGenerationService
{
    private readonly ILogger<NSwagCodeGenerationService> _logger;
    private readonly HttpClient _httpClient;

    public NSwagCodeGenerationService(ILogger<NSwagCodeGenerationService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<string> GenerateCSharpClientAsync(string openApiUrl, string namespaceName, string className)
    {
        if (string.IsNullOrWhiteSpace(openApiUrl)) throw new ArgumentNullException(nameof(openApiUrl));
        try
        {
            var document = await OpenApiDocument.FromUrlAsync(openApiUrl);
            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = className,
                CSharpGeneratorSettings = { Namespace = namespaceName }
            };
            var generator = new CSharpClientGenerator(document, settings);
            return generator.GenerateFile();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate C# client from OpenAPI URL: {Url}", openApiUrl);
            throw new InvalidOperationException($"NSwag code generation failed: {ex.Message}", ex);
        }
    }

    public async Task<string> GenerateCSharpClientFromContentAsync(string openApiContent, string namespaceName, string className)
    {
        if (string.IsNullOrWhiteSpace(openApiContent)) throw new ArgumentNullException(nameof(openApiContent));
        try
        {
            var document = await OpenApiDocument.FromJsonAsync(openApiContent);
            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = className,
                CSharpGeneratorSettings = { Namespace = namespaceName }
            };
            var generator = new CSharpClientGenerator(document, settings);
            return generator.GenerateFile();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate C# client from OpenAPI content");
            throw new InvalidOperationException($"NSwag code generation failed: {ex.Message}", ex);
        }
    }

    public async Task<string> GenerateTestCasesAsync(string clientCode, string openApiUrl, string namespaceName)
    {
        if (string.IsNullOrWhiteSpace(clientCode)) throw new ArgumentNullException(nameof(clientCode));
        // Stub: In a real implementation, parse the client code and OpenAPI spec to generate tests
        // For now, return a placeholder
        return $"// Test cases for {namespaceName}\n// TODO: Implement test generation logic.";
    }

    public async Task<string> GenerateServiceImplementationAsync(string openApiUrl, string namespaceName, string serviceName)
    {
        if (string.IsNullOrWhiteSpace(openApiUrl)) throw new ArgumentNullException(nameof(openApiUrl));
        // Stub: In a real implementation, generate both interface and implementation
        // For now, generate the client and wrap it in a service class
        var clientCode = await GenerateCSharpClientAsync(openApiUrl, namespaceName, serviceName + "Client");
        var serviceCode = $"// Service implementation for {serviceName}\n// TODO: Implement service logic wrapping the generated client.";
        return clientCode + "\n" + serviceCode;
    }

    public async Task<CodeGenerationResult> ValidateGeneratedCodeAsync(string generatedCode)
    {
        if (string.IsNullOrWhiteSpace(generatedCode)) throw new ArgumentNullException(nameof(generatedCode));
        // Stub: In a real implementation, use Roslyn to compile and validate code
        // For now, just check if code is non-empty
        var result = new CodeGenerationResult
        {
            IsValid = !string.IsNullOrWhiteSpace(generatedCode),
            GeneratedCode = generatedCode
        };
        if (!result.IsValid)
        {
            result.Errors.Add("Generated code is empty.");
        }
        return result;
    }

    public async Task<string> GenerateCodeWithSettingsAsync(string openApiUrl, NSwagGenerationSettings settings)
    {
        if (string.IsNullOrWhiteSpace(openApiUrl)) throw new ArgumentNullException(nameof(openApiUrl));
        if (settings == null) throw new ArgumentNullException(nameof(settings));
        try
        {
            var document = await OpenApiDocument.FromUrlAsync(openApiUrl);
            var csharpSettings = new CSharpClientGeneratorSettings
            {
                ClassName = settings.ClassName,
                GenerateClientClasses = true,
                GenerateDtoTypes = settings.GenerateDataContracts,
                GenerateExceptionClasses = settings.GenerateExceptionClasses,
                CSharpGeneratorSettings = {
                    Namespace = settings.Namespace,
                    GenerateNullableReferenceTypes = settings.GenerateNullableReferenceTypes
                }
            };
            var generator = new CSharpClientGenerator(document, csharpSettings);
            return generator.GenerateFile();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate code with custom NSwag settings");
            throw new InvalidOperationException($"NSwag code generation failed: {ex.Message}", ex);
        }
    }

    public async Task<NSwagVersionInfo> GetVersionInfoAsync()
    {
        // Stub: NSwag does not provide a direct API for version info at runtime
        // For now, return hardcoded info
        return new NSwagVersionInfo
        {
            Version = typeof(OpenApiDocument).Assembly.GetName().Version?.ToString() ?? "unknown",
            SupportedOpenApiVersions = new List<string> { "2.0", "3.0", "3.1" },
            SupportedLanguages = new List<string> { "CSharp", "TypeScript" },
            Features = new List<string> { "Client generation", "DTO generation", "Exception classes" }
        };
    }
} 