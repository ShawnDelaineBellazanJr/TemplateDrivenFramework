using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using StrangeLoopPlatform.Core.Interfaces;
using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Agents;

/// <summary>
/// Process Framework orchestrator for managing self-improvement workflows
/// </summary>
public class ProcessFrameworkOrchestrator : IProcessOrchestrator
{
    private readonly ILogger<ProcessFrameworkOrchestrator> _logger;
    private readonly Kernel _kernel;
    private readonly IStrangeLoopOrchestrator _strangeLoopOrchestrator;
    private readonly ISemanticMemoryService _memoryService;
    private readonly IDynamicCodeService _dynamicCodeService;
    private readonly Dictionary<string, ProcessState> _activeProcesses;

    public ProcessFrameworkOrchestrator(
        ILogger<ProcessFrameworkOrchestrator> logger,
        Kernel kernel,
        IStrangeLoopOrchestrator strangeLoopOrchestrator,
        ISemanticMemoryService memoryService,
        IDynamicCodeService dynamicCodeService)
    {
        _logger = logger;
        _kernel = kernel;
        _strangeLoopOrchestrator = strangeLoopOrchestrator;
        _memoryService = memoryService;
        _dynamicCodeService = dynamicCodeService;
        _activeProcesses = new Dictionary<string, ProcessState>();
    }

    public async Task<ProcessState> StartProcessAsync(StrangeLoopRequest request, CancellationToken cancellationToken = default)
    {
        var processState = new ProcessState
        {
            OriginalRequest = request,
            CurrentPhase = ProcessPhase.Initialized,
            StartTime = DateTime.UtcNow
        };

        _activeProcesses[processState.ProcessId] = processState;

        _logger.LogInformation("Started new process {ProcessId} for request: {Request}", 
            processState.ProcessId, request.Requirements);

        // Store initial context
        await _memoryService.StoreProcessContextAsync(processState.ProcessId, new ProcessContext
        {
            ProcessId = processState.ProcessId,
            Data = new Dictionary<string, object>
            {
                ["request"] = request,
                ["startTime"] = processState.StartTime
            },
            CreatedAt = DateTime.UtcNow
        }, cancellationToken);

        return processState;
    }

    public async Task<ProcessState> ExecuteNextPhaseAsync(ProcessState processState, CancellationToken cancellationToken = default)
    {
        var phaseStartTime = DateTime.UtcNow;
        _logger.LogInformation("Executing phase {Phase} for process {ProcessId}", 
            processState.CurrentPhase, processState.ProcessId);

        try
        {
            switch (processState.CurrentPhase)
            {
                case ProcessPhase.Initialized:
                    processState.CurrentPhase = ProcessPhase.Planning;
                    break;

                case ProcessPhase.Planning:
                    await ExecutePlanningPhaseAsync(processState, cancellationToken);
                    processState.CurrentPhase = ProcessPhase.Coding;
                    break;

                case ProcessPhase.Coding:
                    await ExecuteCodingPhaseAsync(processState, cancellationToken);
                    processState.CurrentPhase = ProcessPhase.Testing;
                    break;

                case ProcessPhase.Testing:
                    await ExecuteTestingPhaseAsync(processState, cancellationToken);
                    processState.CurrentPhase = ProcessPhase.Reflecting;
                    break;

                case ProcessPhase.Reflecting:
                    await ExecuteReflectingPhaseAsync(processState, cancellationToken);
                    
                    if (processState.IsConverged)
                    {
                        processState.CurrentPhase = ProcessPhase.Converged;
                        processState.CompletionTime = DateTime.UtcNow;
                        _activeProcesses.Remove(processState.ProcessId);
                    }
                    else if (processState.ShouldContinue)
                    {
                        processState.IterationNumber++;
                        processState.CurrentPhase = ProcessPhase.Planning;
                    }
                    else
                    {
                        processState.CurrentPhase = ProcessPhase.Failed;
                        processState.CompletionTime = DateTime.UtcNow;
                        _activeProcesses.Remove(processState.ProcessId);
                    }
                    break;

                default:
                    throw new InvalidOperationException($"Invalid process phase: {processState.CurrentPhase}");
            }

            // Update metrics
            var phaseTime = DateTime.UtcNow - phaseStartTime;
            processState.Metrics.PhaseTimings[processState.CurrentPhase.ToString()] = phaseTime;
            processState.Metrics.TotalExecutionTime = DateTime.UtcNow - processState.StartTime;

            // Store updated context
            await _memoryService.StoreProcessContextAsync(processState.ProcessId, new ProcessContext
            {
                ProcessId = processState.ProcessId,
                Data = new Dictionary<string, object>
                {
                    ["currentPhase"] = processState.CurrentPhase,
                    ["iterationNumber"] = processState.IterationNumber,
                    ["currentPlan"] = processState.CurrentPlan,
                    ["generatedCode"] = processState.GeneratedCode,
                    ["testResults"] = processState.TestResults,
                    ["reflectionResult"] = processState.ReflectionResult
                },
                LastAccessed = DateTime.UtcNow
            }, cancellationToken);

            return processState;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing phase {Phase} for process {ProcessId}", 
                processState.CurrentPhase, processState.ProcessId);
            
            processState.CurrentPhase = ProcessPhase.Failed;
            processState.ErrorMessage = ex.Message;
            processState.CompletionTime = DateTime.UtcNow;
            _activeProcesses.Remove(processState.ProcessId);
            
            return processState;
        }
    }

    public async Task<ProcessState> ExecuteCompleteProcessAsync(StrangeLoopRequest request, CancellationToken cancellationToken = default)
    {
        var processState = await StartProcessAsync(request, cancellationToken);

        while (processState.CurrentPhase != ProcessPhase.Converged && 
               processState.CurrentPhase != ProcessPhase.Failed)
        {
            processState = await ExecuteNextPhaseAsync(processState, cancellationToken);
            
            // Add a small delay to prevent tight loops
            await Task.Delay(100, cancellationToken);
        }

        // Store final learning insights
        if (processState.CurrentPhase == ProcessPhase.Converged)
        {
            await StoreLearningInsightsAsync(processState, cancellationToken);
        }

        return processState;
    }

    public async Task<ProcessState?> GetProcessStatusAsync(string processId)
    {
        if (_activeProcesses.TryGetValue(processId, out var processState))
        {
            return processState;
        }

        // Try to retrieve from memory
        var context = await _memoryService.GetProcessContextAsync(processId);
        if (context != null)
        {
            // Reconstruct process state from context
            var reconstructedState = new ProcessState
            {
                ProcessId = processId,
                StartTime = context.CreatedAt
            };

            if (context.Data.TryGetValue("currentPhase", out var phase))
            {
                reconstructedState.CurrentPhase = (ProcessPhase)phase;
            }

            if (context.Data.TryGetValue("iterationNumber", out var iteration))
            {
                reconstructedState.IterationNumber = (int)iteration;
            }

            return reconstructedState;
        }

        return null;
    }

    public async Task<bool> CancelProcessAsync(string processId)
    {
        await Task.CompletedTask;
        if (_activeProcesses.TryGetValue(processId, out var processState))
        {
            processState.CurrentPhase = ProcessPhase.Failed;
            processState.ErrorMessage = "Process cancelled by user";
            processState.CompletionTime = DateTime.UtcNow;
            _activeProcesses.Remove(processId);

            _logger.LogInformation("Process {ProcessId} cancelled", processId);
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<ProcessState>> GetActiveProcessesAsync()
    {
        await Task.CompletedTask;
        return _activeProcesses.Values;
    }

    private async Task ExecutePlanningPhaseAsync(ProcessState processState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing planning phase for process {ProcessId}", processState.ProcessId);

        // Retrieve relevant learning insights
        var insights = await _memoryService.GetLearningInsightsAsync(
            processState.OriginalRequest.Requirements, 5, cancellationToken);

        // Create planning prompt with context
        var planningPrompt = CreatePlanningPrompt(processState, insights);

        // Execute planning using the existing orchestrator
        var planningResponse = await _strangeLoopOrchestrator.ExecuteAsync(new StrangeLoopRequest
        {
            Requirements = planningPrompt
        }, cancellationToken);

        // Extract the result from the agent results
        var architectResult = planningResponse.AgentResults.GetValueOrDefault(AgentRole.Architect);
        processState.CurrentPlan = architectResult?.Output ?? "No plan generated";
        
        // Store plan in artifact history
        processState.ArtifactHistory.Add(new ArtifactVersion
        {
            Version = processState.IterationNumber,
            Timestamp = DateTime.UtcNow,
            ArtifactType = "plan",
            Content = processState.CurrentPlan,
            IsActive = true
        });

        _logger.LogInformation("Planning phase completed for process {ProcessId}", processState.ProcessId);
    }

    private async Task ExecuteCodingPhaseAsync(ProcessState processState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing coding phase for process {ProcessId}", processState.ProcessId);

        // Create coding prompt with plan context
        var codingPrompt = CreateCodingPrompt(processState);

        // Execute coding using the existing orchestrator
        var codingResponse = await _strangeLoopOrchestrator.ExecuteAsync(new StrangeLoopRequest
        {
            Requirements = codingPrompt
        }, cancellationToken);

        // Extract the result from the agent results
        var developerResult = codingResponse.AgentResults.GetValueOrDefault(AgentRole.SeniorDeveloper);
        processState.GeneratedCode = developerResult?.Output ?? "No code generated";

        // Store code in artifact history
        processState.ArtifactHistory.Add(new ArtifactVersion
        {
            Version = processState.IterationNumber,
            Timestamp = DateTime.UtcNow,
            ArtifactType = "code",
            Content = processState.GeneratedCode,
            IsActive = true
        });

        _logger.LogInformation("Coding phase completed for process {ProcessId}", processState.ProcessId);
    }

    private async Task ExecuteTestingPhaseAsync(ProcessState processState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing testing phase for process {ProcessId}", processState.ProcessId);

        if (string.IsNullOrEmpty(processState.GeneratedCode))
        {
            processState.TestResults = new TestResults
            {
                AllTestsPassed = false,
                ErrorMessage = "No code generated to test"
            };
            return;
        }

        try
        {
            // Generate test cases
            var testCases = await _dynamicCodeService.GenerateTestCasesAsync(
                processState.GeneratedCode, "MainFunction");

            // Create testing prompt
            var testingPrompt = CreateTestingPrompt(processState, testCases);

            // Execute testing using the existing orchestrator
            var testingResponse = await _strangeLoopOrchestrator.ExecuteAsync(new StrangeLoopRequest
            {
                Requirements = testingPrompt
            }, cancellationToken);

            // Extract the result from the agent results
            var qaResult = testingResponse.AgentResults.GetValueOrDefault(AgentRole.QualityAssurance);
            var testOutput = qaResult?.Output ?? "No test results generated";

            // Parse test results (simplified for now)
            processState.TestResults = ParseTestResults(testOutput);

            _logger.LogInformation("Testing phase completed for process {ProcessId}. All tests passed: {AllPassed}", 
                processState.ProcessId, processState.TestResults.AllTestsPassed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during testing phase for process {ProcessId}", processState.ProcessId);
            processState.TestResults = new TestResults
            {
                AllTestsPassed = false,
                ErrorMessage = ex.Message
            };
        }
    }

    private async Task ExecuteReflectingPhaseAsync(ProcessState processState, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing reflection phase for process {ProcessId}", processState.ProcessId);

        // Create reflection prompt
        var reflectionPrompt = CreateReflectionPrompt(processState);

        // Execute reflection using the existing orchestrator
        var reflectionResponse = await _strangeLoopOrchestrator.ExecuteAsync(new StrangeLoopRequest
        {
            Requirements = reflectionPrompt
        }, cancellationToken);

        // Extract the result from the agent results
        var reflectorResult = reflectionResponse.AgentResults.GetValueOrDefault(AgentRole.Reflector);
        var reflectionOutput = reflectorResult?.Output ?? "No reflection generated";

        // Parse reflection results
        processState.ReflectionResult = ParseReflectionResult(reflectionOutput);

        // Determine if process should continue
        processState.IsConverged = processState.ReflectionResult.ShouldContinue == false && 
                                  processState.TestResults?.AllTestsPassed == true;

        _logger.LogInformation("Reflection phase completed for process {ProcessId}. Should continue: {ShouldContinue}", 
            processState.ProcessId, processState.ReflectionResult.ShouldContinue);
    }

    private async Task StoreLearningInsightsAsync(ProcessState processState, CancellationToken cancellationToken)
    {
        var insight = new LearningInsight
        {
            Title = $"Successful completion of {processState.OriginalRequest.Requirements}",
            Description = $"Process completed successfully in {processState.IterationNumber} iterations",
            Category = "process_completion",
            Tags = new[] { "success", "optimization", "learning" },
            CreatedAt = DateTime.UtcNow,
            SourceProcessId = processState.ProcessId,
            Confidence = 0.9,
            Evidence = new Dictionary<string, object>
            {
                ["iterations"] = processState.IterationNumber,
                ["totalTime"] = processState.Metrics.TotalExecutionTime,
                ["finalCode"] = processState.GeneratedCode
            }
        };

        await _memoryService.StoreLearningInsightAsync(insight, cancellationToken);
    }

    private string CreatePlanningPrompt(ProcessState processState, List<LearningInsight> insights)
    {
        var prompt = $@"
You are an Enterprise Architect planning a solution for the following request:

**Request:** {processState.OriginalRequest.Requirements}

**Context:**
- This is iteration {processState.IterationNumber} of the process
- Previous iterations: {processState.IterationNumber - 1}

**Learning Insights from Previous Similar Tasks:**
{string.Join("\n", insights.Select(i => $"- {i.Title}: {i.Description}"))}

**Task:** Create a detailed plan for implementing this solution. The plan should:
1. Break down the problem into clear, implementable steps
2. Consider any insights from previous similar tasks
3. Be specific enough for a developer to implement
4. Include testing considerations

**Output Format:** Provide a structured plan in JSON format with the following structure:
{{
  ""steps"": [
    {{
      ""stepNumber"": 1,
      ""description"": ""Step description"",
      ""implementation"": ""How to implement this step"",
      ""testing"": ""How to test this step""
    }}
  ],
  ""estimatedComplexity"": ""low|medium|high"",
  ""potentialChallenges"": [""challenge1"", ""challenge2""],
  ""successCriteria"": [""criterion1"", ""criterion2""]
}}";

        return prompt;
    }

    private string CreateCodingPrompt(ProcessState processState)
    {
        return $@"
You are an Enterprise Senior Developer implementing code based on the following plan:

**Original Request:** {processState.OriginalRequest.Requirements}

**Current Plan:** {processState.CurrentPlan}

**Task:** Implement the solution according to the plan. The code should:
1. Be clean, well-structured, and follow C# best practices
2. Include proper error handling and edge cases
3. Be testable and maintainable
4. Include XML documentation comments

**Output Format:** Provide the complete C# code implementation in a code block.

**Requirements:**
- Use only safe, allowed namespaces and types
- Include a main function that can be called for testing
- Handle edge cases and provide meaningful error messages
- Follow enterprise coding standards";
    }

    private string CreateTestingPrompt(ProcessState processState, string testCases)
    {
        return $@"
You are an Enterprise Quality Assurance specialist testing the following code:

**Original Request:** {processState.OriginalRequest.Requirements}

**Generated Code:** {processState.GeneratedCode}

**Generated Test Cases:** {testCases}

**Task:** Evaluate the code and test cases to determine if the solution meets the requirements. Consider:
1. Functional correctness
2. Edge case handling
3. Performance considerations
4. Code quality and maintainability

**Output Format:** Provide a structured test result in JSON format:
{{
  ""allTestsPassed"": true/false,
  ""testCases"": [
    {{
      ""testName"": ""Test name"",
      ""passed"": true/false,
      ""expectedOutput"": ""Expected result"",
      ""actualOutput"": ""Actual result"",
      ""errorMessage"": ""Error if any""
    }}
  ],
  ""performanceScore"": 0.0-1.0,
  ""executionTime"": ""00:00:00.000"",
  ""overallAssessment"": ""Overall assessment of the solution""
}}";
    }

    private string CreateReflectionPrompt(ProcessState processState)
    {
        return $@"
You are an Enterprise Reflector analyzing the results of a self-improvement process:

**Original Request:** {processState.OriginalRequest.Requirements}

**Current Plan:** {processState.CurrentPlan}

**Generated Code:** {processState.GeneratedCode}

**Test Results:** {processState.TestResults?.AllTestsPassed} - {processState.TestResults?.ErrorMessage}

**Process Context:**
- Iteration: {processState.IterationNumber}
- Total execution time: {processState.Metrics.TotalExecutionTime}

**Task:** Analyze the current results and determine the next action. Consider:
1. Did the tests pass? If not, what went wrong?
2. Is the solution complete and correct?
3. Are there opportunities for improvement?
4. Should we continue to the next iteration or converge?

**Output Format:** Provide a structured reflection in JSON format:
{{
  ""shouldContinue"": true/false,
  ""analysis"": ""Detailed analysis of the current state"",
  ""improvements"": [""improvement1"", ""improvement2""],
  ""issues"": [""issue1"", ""issue2""],
  ""qualityScore"": 0.0-1.0,
  ""nextAction"": ""Specific action to take next"",
  ""convergenceReason"": ""Reason for continuing or stopping""
}}";
    }

    private TestResults ParseTestResults(string testResultJson)
    {
        try
        {
            // Simplified parsing - in a real implementation, you'd use proper JSON deserialization
            var testResults = new TestResults
            {
                AllTestsPassed = testResultJson.Contains("\"allTestsPassed\": true"),
                TestCases = new List<TestResult>(),
                PerformanceScore = 0.8, // Default score
                ExecutionTime = TimeSpan.FromMilliseconds(100) // Default time
            };

            // Add a simple test case based on the result
            testResults.TestCases.Add(new TestResult
            {
                TestName = "Basic Functionality Test",
                Passed = testResults.AllTestsPassed,
                ExpectedOutput = "Expected successful execution",
                ActualOutput = testResults.AllTestsPassed ? "Success" : "Failed",
                ErrorMessage = testResults.AllTestsPassed ? null : "Test failed"
            });

            return testResults;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing test results");
            return new TestResults
            {
                AllTestsPassed = false,
                ErrorMessage = "Error parsing test results: " + ex.Message
            };
        }
    }

    private ReflectionResult ParseReflectionResult(string reflectionJson)
    {
        try
        {
            // Simplified parsing - in a real implementation, you'd use proper JSON deserialization
            return new ReflectionResult
            {
                ShouldContinue = reflectionJson.Contains("\"shouldContinue\": true"),
                Analysis = "Analysis of current results",
                Improvements = new List<string> { "Potential improvement 1" },
                Issues = new List<string>(),
                QualityScore = 0.8, // Default score
                NextAction = "Continue to next iteration or converge"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing reflection results");
            return new ReflectionResult
            {
                ShouldContinue = false,
                Analysis = "Error parsing reflection results",
                QualityScore = 0.0,
                NextAction = "Stop due to parsing error"
            };
        }
    }
} 