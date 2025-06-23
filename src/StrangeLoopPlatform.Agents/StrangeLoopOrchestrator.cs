using Microsoft.Extensions.Logging;
using StrangeLoopPlatform.Core.Interfaces;
using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Agents;

/// <summary>
/// Main orchestrator for the strange loop platform that coordinates all enterprise agents
/// </summary>
public class StrangeLoopOrchestrator : IStrangeLoopOrchestrator
{
    private readonly ILogger<StrangeLoopOrchestrator> _logger;
    private readonly IEnumerable<IEnterpriseAgent> _agents;
    private readonly Dictionary<string, StrangeLoopResponse> _activeProcesses = new();
    private readonly object _lockObject = new();

    public StrangeLoopOrchestrator(
        IEnumerable<IEnterpriseAgent> agents,
        ILogger<StrangeLoopOrchestrator> logger)
    {
        _agents = agents;
        _logger = logger;
    }

    public async Task<StrangeLoopResponse> ExecuteAsync(StrangeLoopRequest request, CancellationToken cancellationToken = default)
    {
        var response = new StrangeLoopResponse
        {
            Request = request,
            Status = StrangeLoopStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        // Register the process
        lock (_lockObject)
        {
            _activeProcesses[request.Id] = response;
        }

        try
        {
            _logger.LogInformation("Starting strange loop process for request {RequestId}", request.Id);

            // Execute the strange loop process
            await ExecuteStrangeLoopProcessAsync(response, cancellationToken);

            response.Status = StrangeLoopStatus.Completed;
            response.CompletedAt = DateTime.UtcNow;

            _logger.LogInformation("Completed strange loop process for request {RequestId} in {Duration}ms", 
                request.Id, response.Duration?.TotalMilliseconds);
        }
        catch (OperationCanceledException)
        {
            response.Status = StrangeLoopStatus.Cancelled;
            _logger.LogInformation("Strange loop process cancelled for request {RequestId}", request.Id);
        }
        catch (Exception ex)
        {
            response.Status = StrangeLoopStatus.Failed;
            response.Errors.Add(ex.Message);
            _logger.LogError(ex, "Strange loop process failed for request {RequestId}", request.Id);
        }
        finally
        {
            // Remove from active processes
            lock (_lockObject)
            {
                _activeProcesses.Remove(request.Id);
            }
        }

        return response;
    }

    public Task<StrangeLoopResponse?> GetStatusAsync(string requestId)
    {
        lock (_lockObject)
        {
            return Task.FromResult(_activeProcesses.TryGetValue(requestId, out var response) ? response : null);
        }
    }

    public Task<bool> CancelAsync(string requestId)
    {
        lock (_lockObject)
        {
            if (_activeProcesses.TryGetValue(requestId, out var response))
            {
                response.Status = StrangeLoopStatus.Cancelled;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }

    public Task<IEnumerable<StrangeLoopResponse>> GetActiveProcessesAsync()
    {
        lock (_lockObject)
        {
            return Task.FromResult(_activeProcesses.Values.AsEnumerable());
        }
    }

    private async Task ExecuteStrangeLoopProcessAsync(StrangeLoopResponse response, CancellationToken cancellationToken)
    {
        var request = response.Request;
        var currentIteration = 0;

        while (currentIteration < request.MaxIterations && !cancellationToken.IsCancellationRequested)
        {
            currentIteration++;
            response.CurrentIteration = currentIteration;

            _logger.LogInformation("Starting iteration {Iteration} for request {RequestId}", 
                currentIteration, request.Id);

            // Create execution context for this iteration
            var context = new AgentExecutionContext
            {
                Request = request,
                Iteration = currentIteration,
                PreviousResults = new Dictionary<AgentRole, AgentResult>(response.AgentResults),
                PreviousArtifacts = response.GeneratedArtifacts.ToList(),
                PreviousQualityMetrics = response.QualityMetrics,
                Errors = response.Errors.ToList(),
                Warnings = response.Warnings.ToList()
            };

            // Execute agents in sequence based on the strange loop pattern
            var shouldContinue = await ExecuteIterationAsync(context, response, cancellationToken);

            if (!shouldContinue)
            {
                _logger.LogInformation("Strange loop process converged after {Iteration} iterations for request {RequestId}", 
                    currentIteration, request.Id);
                break;
            }

            // Check if we should continue based on quality metrics
            if (response.QualityMetrics.AllQualityGatesPassed)
            {
                _logger.LogInformation("All quality gates passed after {Iteration} iterations for request {RequestId}", 
                    currentIteration, request.Id);
                break;
            }
        }

        if (currentIteration >= request.MaxIterations)
        {
            response.Warnings.Add($"Maximum iterations ({request.MaxIterations}) reached without convergence");
        }
    }

    private async Task<bool> ExecuteIterationAsync(
        AgentExecutionContext context, 
        StrangeLoopResponse response, 
        CancellationToken cancellationToken)
    {
        var agentsToExecute = GetAgentExecutionOrder(context);
        var iterationResults = new Dictionary<AgentRole, AgentResult>();

        foreach (var agentRole in agentsToExecute)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            var agent = _agents.FirstOrDefault(a => a.Role == agentRole);
            if (agent == null)
            {
                _logger.LogWarning("Agent {AgentRole} not found", agentRole);
                continue;
            }

            if (!agent.IsAvailable)
            {
                _logger.LogWarning("Agent {AgentRole} is not available", agentRole);
                continue;
            }

            // Update context with previous results
            context.PreviousResults = new Dictionary<AgentRole, AgentResult>(iterationResults);

            // Execute the agent
            var result = await agent.ExecuteAsync(context, cancellationToken);
            iterationResults[agentRole] = result;

            // Update response
            response.AgentResults[agentRole] = result;
            response.Status = GetStatusForAgent(agentRole);

            if (!result.Success)
            {
                response.Errors.Add($"Agent {agentRole} failed: {result.Error}");
                _logger.LogError("Agent {AgentRole} failed for request {RequestId}: {Error}", 
                    agentRole, context.Request.Id, result.Error);
            }

            // Process agent-specific outputs
            ProcessAgentOutput(agentRole, result, response);
        }

        // Determine if we should continue to the next iteration
        return ShouldContinueToNextIteration(iterationResults, response);
    }

    private IEnumerable<AgentRole> GetAgentExecutionOrder(AgentExecutionContext context)
    {
        // Define the execution order for the strange loop pattern
        // This follows the Plan -> Implement -> Test -> Reflect cycle
        
        if (context.Iteration == 1)
        {
            // First iteration: start with business analysis and architecture
            return new[]
            {
                AgentRole.BusinessAnalyst,
                AgentRole.Architect,
                AgentRole.SeniorDeveloper,
                AgentRole.QualityAssurance,
                AgentRole.Reflector
            };
        }
        else
        {
            // Subsequent iterations: focus on implementation and improvement
            return new[]
            {
                AgentRole.SeniorDeveloper,
                AgentRole.QualityAssurance,
                AgentRole.Reflector
            };
        }
    }

    private StrangeLoopStatus GetStatusForAgent(AgentRole agentRole)
    {
        return agentRole switch
        {
            AgentRole.BusinessAnalyst => StrangeLoopStatus.Planning,
            AgentRole.Architect => StrangeLoopStatus.Planning,
            AgentRole.SeniorDeveloper => StrangeLoopStatus.Implementing,
            AgentRole.QualityAssurance => StrangeLoopStatus.Testing,
            AgentRole.Reflector => StrangeLoopStatus.Reflecting,
            _ => StrangeLoopStatus.Pending
        };
    }

    private void ProcessAgentOutput(
        AgentRole agentRole, 
        AgentResult result, 
        StrangeLoopResponse response)
    {
        switch (agentRole)
        {
            case AgentRole.Architect:
                // Process architecture design
                ProcessArchitectureOutput(result, response);
                break;
            
            case AgentRole.SeniorDeveloper:
                // Process implementation output
                ProcessImplementationOutput(result, response);
                break;
            
            case AgentRole.QualityAssurance:
                // Process quality validation
                ProcessQualityOutput(result, response);
                break;
            
            case AgentRole.Reflector:
                // Process reflection and improvement suggestions
                ProcessReflectionOutput(result, response);
                break;
        }
    }

    private void ProcessArchitectureOutput(AgentResult result, StrangeLoopResponse response)
    {
        // Extract architecture artifacts from the agent output
        // This could include component diagrams, service definitions, etc.
        var artifact = new GeneratedArtifact
        {
            Type = ArtifactType.Architecture,
            Name = "System Architecture Design",
            Content = result.Output,
            MimeType = "application/json",
            IsValid = result.Success
        };

        response.GeneratedArtifacts.Add(artifact);
    }

    private void ProcessImplementationOutput(AgentResult result, StrangeLoopResponse response)
    {
        // Extract code artifacts from the agent output
        var artifact = new GeneratedArtifact
        {
            Type = ArtifactType.Code,
            Name = "Generated Implementation",
            Content = result.Output,
            MimeType = "text/plain",
            IsValid = result.Success
        };

        response.GeneratedArtifacts.Add(artifact);
    }

    private void ProcessQualityOutput(AgentResult result, StrangeLoopResponse response)
    {
        // Extract quality metrics from the agent output
        // This is a simplified version - in practice, you'd parse the JSON output
        if (result.Success)
        {
            response.QualityMetrics.OverallScore = 85.0; // Example score
            response.QualityMetrics.AllQualityGatesPassed = true;
        }
    }

    private void ProcessReflectionOutput(AgentResult result, StrangeLoopResponse response)
    {
        // Process reflection output to determine if we should continue
        // This could include improvement suggestions, convergence analysis, etc.
        if (result.Success)
        {
            // Add any warnings or suggestions from the reflector
            if (result.Output.Contains("warning", StringComparison.OrdinalIgnoreCase))
            {
                response.Warnings.Add($"Reflection warning: {result.Output}");
            }
        }
    }

    private bool ShouldContinueToNextIteration(Dictionary<AgentRole, AgentResult> iterationResults, StrangeLoopResponse response)
    {
        // Check if all agents succeeded
        if (iterationResults.Any(r => !r.Value.Success))
        {
            return false;
        }

        // Check if quality gates are passed
        if (response.QualityMetrics.AllQualityGatesPassed)
        {
            return false;
        }

        // Check if reflector suggests stopping
        if (iterationResults.TryGetValue(AgentRole.Reflector, out var reflectorResult))
        {
            if (reflectorResult.Output.Contains("converged", StringComparison.OrdinalIgnoreCase) ||
                reflectorResult.Output.Contains("complete", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
        }

        return true;
    }
} 