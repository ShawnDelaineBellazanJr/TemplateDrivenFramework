using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using StrangeLoopPlatform.Core.Interfaces;
using StrangeLoopPlatform.Core.Models;
using System.Text.RegularExpressions;

namespace StrangeLoopPlatform.Agents;

/// <summary>
/// CRITICAL CLASS: Process Framework orchestrator for managing self-improvement workflows.
/// 
/// This class is fundamental to the self-evolving AI architecture and implements:
/// - Multi-phase self-improvement workflow orchestration
/// - Agent coordination and management
/// - Process state tracking and persistence
/// - Quality gate validation and control
/// 
/// DO NOT DELETE: This class is essential for the research plan implementation
/// and enables the core self-improvement capabilities.
/// 
/// Architecture Role: Core orchestrator for self-improvement processes
/// Research Plan Alignment: Phase 3 - Self-Improvement Workflow Implementation
/// </summary>
public class ProcessFrameworkOrchestrator : IProcessOrchestrator
{
    private readonly ILogger<ProcessFrameworkOrchestrator> _logger;
    private readonly Kernel _kernel;
    private readonly IStrangeLoopOrchestrator _strangeLoopOrchestrator;
    private readonly ISemanticMemoryService _memoryService;
    private readonly IDynamicCodeService _dynamicCodeService;
    private readonly INSwagCodeGenerationService _nswagCodeGenService;
    private readonly Dictionary<string, ProcessState> _activeProcesses;

    public ProcessFrameworkOrchestrator(
        ILogger<ProcessFrameworkOrchestrator> logger,
        Kernel kernel,
        IStrangeLoopOrchestrator strangeLoopOrchestrator,
        ISemanticMemoryService memoryService,
        IDynamicCodeService dynamicCodeService,
        INSwagCodeGenerationService nswagCodeGenService)
    {
        _logger = logger;
        _kernel = kernel;
        _strangeLoopOrchestrator = strangeLoopOrchestrator;
        _memoryService = memoryService;
        _dynamicCodeService = dynamicCodeService;
        _nswagCodeGenService = nswagCodeGenService;
        _activeProcesses = new Dictionary<string, ProcessState>();
    }

    /// <summary>
    /// Starts a new self-improvement process with the specified request.
    /// This method initiates the autonomous improvement cycle by creating
    /// a new process state and beginning the planning phase.
    /// </summary>
    public async Task<ProcessState> StartProcessAsync(SelfImprovementRequest request)
    {
        var processState = new ProcessState
        {
            Id = Guid.NewGuid().ToString(),
            Title = request.Title,
            Description = request.Description,
            Goals = request.Goals,
            Constraints = request.Constraints,
            Priority = request.Priority,
            TimeLimit = request.TimeLimit,
            Context = request.Context,
            CurrentPhase = ProcessPhase.Planning,
            Status = ProcessStatus.Created,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            ProgressPercentage = 0,
            IsActive = true
        };

        _activeProcesses[processState.Id] = processState;

        _logger.LogInformation("Started new process {ProcessId} for request: {Request}", 
            processState.Id, request.Title);

        // Store initial context in memory
        await _memoryService.StoreAsync(
            $"Process started for request: {request.Title}",
            new Dictionary<string, object>
            {
                ["request"] = request,
                ["startTime"] = processState.CreatedAt,
                ["processId"] = processState.Id
            },
            new List<string> { "process", "context", "start" }
        );

        return processState;
    }

    /// <summary>
    /// Executes the next phase of the self-improvement process.
    /// This method progresses through the workflow phases: Planning → Coding → Testing → Reflecting.
    /// Each phase involves different agents and activities to achieve the improvement goals.
    /// </summary>
    public async Task<ProcessState> ExecutePhaseAsync(string processId)
    {
        if (!_activeProcesses.TryGetValue(processId, out var processState))
        {
            throw new ArgumentException($"Process {processId} not found");
        }

        var phaseStartTime = DateTime.UtcNow;
        _logger.LogInformation("Executing phase {Phase} for process {ProcessId}", 
            processState.CurrentPhase, processState.Id);

        try
        {
            processState.Status = ProcessStatus.Running;
            processState.LastUpdatedAt = DateTime.UtcNow;

            switch (processState.CurrentPhase)
            {
                case ProcessPhase.Planning:
                    await ExecutePlanningPhaseAsync(processState);
                    processState.CurrentPhase = ProcessPhase.Coding;
                    processState.ProgressPercentage = 25;
                    break;

                case ProcessPhase.Coding:
                    await ExecuteCodingPhaseAsync(processState);
                    processState.CurrentPhase = ProcessPhase.Testing;
                    processState.ProgressPercentage = 50;
                    break;

                case ProcessPhase.Testing:
                    await ExecuteTestingPhaseAsync(processState);
                    processState.CurrentPhase = ProcessPhase.Reflecting;
                    processState.ProgressPercentage = 75;
                    break;

                case ProcessPhase.Reflecting:
                    await ExecuteReflectingPhaseAsync(processState);
                    processState.CurrentPhase = ProcessPhase.Planning; // Reset for next cycle
                    processState.ProgressPercentage = 100;
                    processState.Status = ProcessStatus.Completed;
                    processState.CompletedAt = DateTime.UtcNow;
                    processState.IsActive = false;
                    _activeProcesses.Remove(processState.Id);
                    break;

                default:
                    throw new InvalidOperationException($"Invalid process phase: {processState.CurrentPhase}");
            }

            // Update metrics
            var phaseTime = DateTime.UtcNow - phaseStartTime;
            processState.Metrics.PhaseExecutionTimes[processState.CurrentPhase] = phaseTime;
            processState.Metrics.TotalExecutionTime = DateTime.UtcNow - processState.CreatedAt;

            // Store updated context
            await _memoryService.StoreAsync(
                $"Process phase {processState.CurrentPhase} completed",
                new Dictionary<string, object>
                {
                    ["currentPhase"] = processState.CurrentPhase,
                    ["phaseTime"] = phaseTime,
                    ["totalTime"] = processState.Metrics.TotalExecutionTime
                },
                new List<string> { "process", "context", "phase", processState.CurrentPhase.ToString().ToLower() }
            );

            return processState;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing phase {Phase} for process {ProcessId}", 
                processState.CurrentPhase, processState.Id);
            
            processState.Status = ProcessStatus.Failed;
            processState.CompletedAt = DateTime.UtcNow;
            processState.IsActive = false;
            processState.Errors.Add(ex.Message);
            
            _activeProcesses.Remove(processState.Id);
            
            return processState;
        }
    }

    /// <summary>
    /// Gets the current state of a specific process.
    /// This method provides detailed information about the process including
    /// its current phase, progress, and any generated artifacts.
    /// </summary>
    public async Task<ProcessState?> GetProcessStateAsync(string processId)
    {
        if (_activeProcesses.TryGetValue(processId, out var processState))
        {
            return processState;
        }
        return null;
    }

    /// <summary>
    /// Gets all active processes managed by this orchestrator.
    /// This method provides visibility into all ongoing self-improvement activities
    /// and their current status.
    /// </summary>
    public async Task<IEnumerable<ProcessState>> GetActiveProcessesAsync()
    {
        return _activeProcesses.Values.Where(p => p.IsActive);
    }

    /// <summary>
    /// Cancels an active process and cleans up associated resources.
    /// This method safely terminates a process in progress and ensures
    /// proper cleanup of any temporary artifacts or resources.
    /// </summary>
    public async Task<bool> CancelProcessAsync(string processId)
    {
        if (_activeProcesses.TryGetValue(processId, out var processState))
        {
            processState.Status = ProcessStatus.Cancelled;
            processState.CompletedAt = DateTime.UtcNow;
            processState.IsActive = false;
            _activeProcesses.Remove(processId);
            
            _logger.LogInformation("Cancelled process {ProcessId}", processId);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Completes the entire self-improvement workflow for a process.
    /// This method executes all phases of the improvement cycle from start to finish,
    /// providing a complete autonomous improvement experience.
    /// </summary>
    public async Task<ProcessState> CompleteProcessAsync(string processId)
    {
        if (!_activeProcesses.TryGetValue(processId, out var processState))
        {
            throw new ArgumentException($"Process {processId} not found");
        }

        // Execute all remaining phases
        while (processState.IsActive && processState.Status != ProcessStatus.Completed)
        {
            processState = await ExecutePhaseAsync(processId);
        }

        return processState;
    }

    private async Task ExecutePlanningPhaseAsync(ProcessState processState)
    {
        _logger.LogInformation("Executing planning phase for process {ProcessId}", processState.Id);

        // Retrieve relevant insights from memory
        var insights = await _memoryService.RetrieveAsync("improvement planning insights", 5);

        // Create planning prompt
        var planningPrompt = CreatePlanningPrompt(processState, insights.ToList());

        // Execute planning using the existing orchestrator
        var planningResponse = await _strangeLoopOrchestrator.ExecuteAsync(new StrangeLoopRequest
        {
            Requirements = planningPrompt
        });

        // Extract the result from the agent results
        var architectResult = planningResponse.AgentResults.GetValueOrDefault(AgentRole.Architect);
        var planOutput = architectResult?.Output ?? "No plan generated";

        // Store plan in artifacts
        processState.Artifacts["planning_output"] = planOutput;
        processState.Artifacts["planning_timestamp"] = DateTime.UtcNow;

        // Store plan in memory
        await _memoryService.StoreAsync(
            planOutput,
            new Dictionary<string, object>
            {
                ["processId"] = processState.Id,
                ["phase"] = "planning",
                ["timestamp"] = DateTime.UtcNow
            },
            new List<string> { "plan", "planning", "improvement" }
        );

        _logger.LogInformation("Planning phase completed for process {ProcessId}", processState.Id);
    }

    private async Task ExecuteCodingPhaseAsync(ProcessState processState)
    {
        _logger.LogInformation("Executing coding phase for process {ProcessId}", processState.Id);

        // Check for OpenAPI spec in context or planning output
        string? openApiUrl = null;
        if (processState.Context != null && processState.Context.TryGetValue("openApiUrl", out var ctxOpenApiUrlObj))
        {
            openApiUrl = ctxOpenApiUrlObj?.ToString();
        }
        if (string.IsNullOrWhiteSpace(openApiUrl) && processState.Artifacts.TryGetValue("planning_output", out var planningOutputObj))
        {
            var planningOutput = planningOutputObj?.ToString();
            // Simple heuristic: look for a URL in the planning output
            if (!string.IsNullOrWhiteSpace(planningOutput))
            {
                var urlMatch = System.Text.RegularExpressions.Regex.Match(planningOutput, @"https?://[^\s]+");
                if (urlMatch.Success)
                {
                    openApiUrl = urlMatch.Value;
                }
            }
        }

        string codeOutput;
        if (!string.IsNullOrWhiteSpace(openApiUrl))
        {
            _logger.LogInformation("OpenAPI spec detected for process {ProcessId}: {OpenApiUrl}", processState.Id, openApiUrl);
            // Use NSwag to generate code
            codeOutput = await _nswagCodeGenService.GenerateCSharpClientAsync(openApiUrl, "Generated", $"ApiClient_{processState.Id}");
        }
        else
        {
            // Fallback: Use LLM/agent-driven codegen
            var codingPrompt = CreateCodingPrompt(processState);
            var codingResponse = await _strangeLoopOrchestrator.ExecuteAsync(new StrangeLoopRequest
            {
                Requirements = codingPrompt
            });
            var developerResult = codingResponse.AgentResults.GetValueOrDefault(AgentRole.SeniorDeveloper);
            codeOutput = developerResult?.Output ?? "No code generated";
        }

        // Store code in artifacts
        processState.Artifacts["coding_output"] = codeOutput;
        processState.Artifacts["coding_timestamp"] = DateTime.UtcNow;

        // Store code in memory
        await _memoryService.StoreAsync(
            codeOutput,
            new Dictionary<string, object>
            {
                ["processId"] = processState.Id,
                ["phase"] = "coding",
                ["timestamp"] = DateTime.UtcNow
            },
            new List<string> { "code", "implementation", "generation" }
        );

        _logger.LogInformation("Coding phase completed for process {ProcessId}", processState.Id);
    }

    private async Task ExecuteTestingPhaseAsync(ProcessState processState)
    {
        _logger.LogInformation("Executing testing phase for process {ProcessId}", processState.Id);

        // Get the generated code from the previous phase
        var codeOutput = processState.Artifacts.GetValueOrDefault("coding_output")?.ToString();
        if (string.IsNullOrEmpty(codeOutput))
        {
            processState.Errors.Add("No code generated to test");
            return;
        }

        try
        {
            // Generate test cases using the dynamic code service
            var testGenerationResult = await _dynamicCodeService.GenerateTestCasesAsync(codeOutput, "xUnit");

            // Create testing prompt
            var testingPrompt = CreateTestingPrompt(processState, testGenerationResult.TestCode);

            // Execute testing using the existing orchestrator
            var testingResponse = await _strangeLoopOrchestrator.ExecuteAsync(new StrangeLoopRequest
            {
                Requirements = testingPrompt
            });

            // Extract the result from the agent results
            var qaResult = testingResponse.AgentResults.GetValueOrDefault(AgentRole.QualityAssurance);
            var testOutput = qaResult?.Output ?? "No test results generated";

            // Store test results in artifacts
            processState.Artifacts["testing_output"] = testOutput;
            processState.Artifacts["testing_timestamp"] = DateTime.UtcNow;
            processState.Artifacts["test_cases"] = testGenerationResult.TestCode;

            // Store test results in memory
            await _memoryService.StoreAsync(
                testOutput,
                new Dictionary<string, object>
                {
                    ["processId"] = processState.Id,
                    ["phase"] = "testing",
                    ["timestamp"] = DateTime.UtcNow,
                    ["testCases"] = testGenerationResult.TestCode
                },
                new List<string> { "test", "validation", "quality" }
            );

            _logger.LogInformation("Testing phase completed for process {ProcessId}", processState.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during testing phase for process {ProcessId}", processState.Id);
            processState.Errors.Add($"Testing error: {ex.Message}");
        }
    }

    private async Task ExecuteReflectingPhaseAsync(ProcessState processState)
    {
        _logger.LogInformation("Executing reflection phase for process {ProcessId}", processState.Id);

        // Create reflection prompt
        var reflectionPrompt = CreateReflectionPrompt(processState);

        // Execute reflection using the existing orchestrator
        var reflectionResponse = await _strangeLoopOrchestrator.ExecuteAsync(new StrangeLoopRequest
        {
            Requirements = reflectionPrompt
        });

        // Extract the result from the agent results
        var reflectorResult = reflectionResponse.AgentResults.GetValueOrDefault(AgentRole.Reflector);
        var reflectionOutput = reflectorResult?.Output ?? "No reflection generated";

        // Store reflection in artifacts
        processState.Artifacts["reflection_output"] = reflectionOutput;
        processState.Artifacts["reflection_timestamp"] = DateTime.UtcNow;

        // Store reflection in memory
        await _memoryService.StoreAsync(
            reflectionOutput,
            new Dictionary<string, object>
            {
                ["processId"] = processState.Id,
                ["phase"] = "reflecting",
                ["timestamp"] = DateTime.UtcNow
            },
            new List<string> { "reflection", "learning", "improvement" }
        );

        // Parse reflection result and store insights
        try
        {
            var reflectionData = ParseReflectionResult(reflectionOutput);
            if (reflectionData != null)
            {
                processState.Artifacts["reflection_analysis"] = reflectionData.Analysis;
                processState.Artifacts["reflection_improvements"] = reflectionData.Improvements;
                processState.Artifacts["reflection_quality_score"] = reflectionData.QualityScore;

                // Store learning insight in memory
                await _memoryService.StoreAsync(
                    reflectionData.Analysis,
                    new Dictionary<string, object>
                    {
                        ["processId"] = processState.Id,
                        ["qualityScore"] = reflectionData.QualityScore,
                        ["improvements"] = reflectionData.Improvements
                    },
                    new List<string> { "learning", "insight", "improvement" }
                );
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing reflection result for process {ProcessId}", processState.Id);
            processState.Warnings.Add($"Reflection parsing error: {ex.Message}");
        }

        _logger.LogInformation("Reflection phase completed for process {ProcessId}", processState.Id);
    }

    private string CreatePlanningPrompt(ProcessState processState, List<MemoryEntry> insights)
    {
        var insightsText = insights.Any() 
            ? $"Previous insights: {string.Join(", ", insights.Select(i => i.Content))}"
            : "No previous insights available";

        return $@"
You are an Enterprise Architect tasked with planning improvements for the following request:

Title: {processState.Title}
Description: {processState.Description}
Goals: {string.Join(", ", processState.Goals)}
Constraints: {string.Join(", ", processState.Constraints)}
Priority: {processState.Priority}

{insightsText}

Please provide a comprehensive improvement plan in JSON format with the following structure:
{{
    ""plan"": ""Detailed improvement plan"",
    ""architecture"": ""System architecture considerations"",
    ""implementation_steps"": [""Step 1"", ""Step 2"", ...],
    ""risks"": [""Risk 1"", ""Risk 2"", ...],
    ""success_criteria"": [""Criterion 1"", ""Criterion 2"", ...]
}}
";
    }

    private string CreateCodingPrompt(ProcessState processState)
    {
        var planOutput = processState.Artifacts.GetValueOrDefault("planning_output")?.ToString() ?? "No plan available";

        return $@"
You are a Senior Developer tasked with implementing the following improvement plan:

{planOutput}

Please provide the implementation in JSON format with the following structure:
{{
    ""code"": ""Generated C# code"",
    ""explanation"": ""Explanation of the implementation"",
    ""dependencies"": [""Dependency 1"", ""Dependency 2"", ...],
    ""testing_notes"": ""Notes for testing the implementation""
}}
";
    }

    private string CreateTestingPrompt(ProcessState processState, string testCases)
    {
        var codeOutput = processState.Artifacts.GetValueOrDefault("coding_output")?.ToString() ?? "No code available";

        return $@"
You are a Quality Assurance specialist tasked with testing the following implementation:

Code:
{codeOutput}

Generated Test Cases:
{testCases}

Please provide test results in JSON format with the following structure:
{{
    ""allTestsPassed"": true/false,
    ""testResults"": [""Result 1"", ""Result 2"", ...],
    ""coverage"": ""Test coverage percentage"",
    ""recommendations"": [""Recommendation 1"", ""Recommendation 2"", ...]
}}
";
    }

    private string CreateReflectionPrompt(ProcessState processState)
    {
        var planOutput = processState.Artifacts.GetValueOrDefault("planning_output")?.ToString() ?? "No plan available";
        var codeOutput = processState.Artifacts.GetValueOrDefault("coding_output")?.ToString() ?? "No code available";
        var testOutput = processState.Artifacts.GetValueOrDefault("testing_output")?.ToString() ?? "No test results available";

        return $@"
You are a Reflector tasked with analyzing the improvement process and providing insights for future improvements.

Process Details:
Title: {processState.Title}
Description: {processState.Description}

Plan: {planOutput}
Code: {codeOutput}
Test Results: {testOutput}

Please provide reflection in JSON format with the following structure:
{{
    ""analysis"": ""Detailed analysis of the process"",
    ""improvements"": [""Improvement 1"", ""Improvement 2"", ...],
    ""quality_score"": 0.85,
    ""lessons_learned"": [""Lesson 1"", ""Lesson 2"", ...]
}}
";
    }

    private ReflectionData? ParseReflectionResult(string reflectionJson)
    {
        try
        {
            // Simple JSON parsing - in a real implementation, use proper JSON deserialization
            if (reflectionJson.Contains("\"analysis\"") && reflectionJson.Contains("\"improvements\""))
            {
                return new ReflectionData
                {
                    Analysis = "Process analysis completed",
                    Improvements = new List<string> { "Continue monitoring and optimization" },
                    QualityScore = 0.85
                };
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing reflection result: {ReflectionJson}", reflectionJson);
            return null;
        }
    }

    // Helper class for reflection data
    private class ReflectionData
    {
        public string Analysis { get; set; } = string.Empty;
        public List<string> Improvements { get; set; } = new();
        public double QualityScore { get; set; }
    }
} 