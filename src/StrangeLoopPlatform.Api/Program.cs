using Microsoft.SemanticKernel;
using StrangeLoopPlatform.Agents;
using StrangeLoopPlatform.Core.Interfaces;
using StrangeLoopPlatform.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure HTTP client for API plugin service
builder.Services.AddHttpClient<IApiPluginService, ApiPluginService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5166/");
});

// Configure HTTP client for OpenAPI plugin service
builder.Services.AddHttpClient<IOpenApiPluginService, OpenApiPluginService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5166/");
});

// Configure Semantic Kernel for Ollama
builder.Services.AddSingleton<Kernel>(provider =>
{
    var kernel = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(
            modelId: "llama3:latest",
            apiKey: "ollama-local",
            httpClient: new HttpClient { BaseAddress = new Uri("http://localhost:11434/v1/") })
        .Build();

    return kernel;
});

// Register all enterprise agents
builder.Services.AddScoped<IEnterpriseAgent, EnterpriseArchitectAgent>();
builder.Services.AddScoped<IEnterpriseAgent, EnterpriseBusinessAnalystAgent>();
builder.Services.AddScoped<IEnterpriseAgent, EnterpriseSeniorDeveloperAgent>();
builder.Services.AddScoped<IEnterpriseAgent, EnterpriseQualityAssuranceAgent>();
builder.Services.AddScoped<IEnterpriseAgent, EnterpriseReflectorAgent>();

// Register the orchestrator
builder.Services.AddScoped<IStrangeLoopOrchestrator, StrangeLoopOrchestrator>();

// Register research plan services
builder.Services.AddScoped<IProcessOrchestrator, ProcessFrameworkOrchestrator>();
builder.Services.AddScoped<IDynamicCodeService, RoslynDynamicCodeService>();
builder.Services.AddScoped<ISemanticMemoryService, InMemorySemanticMemoryService>();

// Register API plugin services
builder.Services.AddScoped<IApiPluginService, ApiPluginService>();
builder.Services.AddScoped<IOpenApiPluginService, OpenApiPluginService>();
builder.Services.AddHttpClient<INSwagCodeGenerationService, NSwagCodeGenerationService>();

// Configure logging
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

// Initialize API as Semantic Kernel plugin
using (var scope = app.Services.CreateScope())
{
    var kernel = scope.ServiceProvider.GetRequiredService<Kernel>();
    var apiPluginService = scope.ServiceProvider.GetRequiredService<IApiPluginService>();
    var openApiPluginService = scope.ServiceProvider.GetRequiredService<IOpenApiPluginService>();
    
    try
    {
        // Register the custom API plugin
        await apiPluginService.RegisterApiAsPluginAsync(kernel);
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Successfully registered Web API as Semantic Kernel plugin");

        // Wait a moment for the API to be fully ready, then load the live OpenAPI spec
        await Task.Delay(2000);
        
        // Load the live API as a plugin from the Swagger endpoint
        await openApiPluginService.LoadLiveApiAsPluginAsync(kernel);
        logger.LogInformation("Successfully loaded live OpenAPI specification as Semantic Kernel plugin");
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Failed to register API plugins");
    }
}

app.Run();
