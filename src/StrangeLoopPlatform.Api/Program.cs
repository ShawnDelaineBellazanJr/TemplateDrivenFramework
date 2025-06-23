using Microsoft.SemanticKernel;
using StrangeLoopPlatform.Agents;
using StrangeLoopPlatform.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure logging
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
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
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Add health check endpoint
app.MapGet("/health", () => new { status = "healthy", timestamp = DateTime.UtcNow });

app.Run();
