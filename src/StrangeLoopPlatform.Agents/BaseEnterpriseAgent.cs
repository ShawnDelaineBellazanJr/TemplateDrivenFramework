using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using StrangeLoopPlatform.Core.Interfaces;
using StrangeLoopPlatform.Core.Models;

namespace StrangeLoopPlatform.Agents;

/// <summary>
/// Base implementation for enterprise agents using Semantic Kernel
/// </summary>
public abstract class BaseEnterpriseAgent : IEnterpriseAgent
{
    protected readonly ILogger<BaseEnterpriseAgent> _logger;
    protected readonly Kernel _kernel;
    protected readonly ChatCompletionAgent _agent;
    protected readonly AgentCapabilities _capabilities;

    protected BaseEnterpriseAgent(
        AgentRole role,
        Kernel kernel,
        ILogger<BaseEnterpriseAgent> logger,
        AgentCapabilities capabilities)
    {
        Role = role;
        _kernel = kernel;
        _logger = logger;
        _capabilities = capabilities;

        // Create the agent using the correct pattern from documentation
        _agent = new ChatCompletionAgent()
        {
            Name = role.ToString(),
            Instructions = GetAgentInstructions(),
            Kernel = kernel
        };
    }

    public AgentRole Role { get; }

    public virtual bool IsAvailable => true;

    public virtual AgentCapabilities GetCapabilities() => _capabilities;

    public async Task<AgentResult> ExecuteAsync(AgentExecutionContext context, CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;
        try
        {
            _logger.LogInformation("Starting execution of {AgentRole} agent for request {RequestId}", Role, context.Request.Id);

            var input = PrepareInput(context);
            var message = new ChatMessageContent(AuthorRole.User, input);

            // Generate the agent response(s) using the correct pattern
            ChatMessageContent? lastContent = null;
            await foreach (ChatMessageContent response in _agent.InvokeAsync(message, cancellationToken: cancellationToken))
            {
                lastContent = response;
            }

            var agentResult = ProcessResult(lastContent ?? new ChatMessageContent(AuthorRole.Assistant, ""), context);
            agentResult.Duration = DateTime.UtcNow.Subtract(startTime);
            agentResult.Success = true;

            _logger.LogInformation("Completed execution of {AgentRole} agent for request {RequestId} in {Duration}ms", Role, context.Request.Id, agentResult.Duration.TotalMilliseconds);
            return agentResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing {AgentRole} agent for request {RequestId}", Role, context.Request.Id);
            return new AgentResult
            {
                Role = Role,
                Output = string.Empty,
                Success = false,
                Duration = DateTime.UtcNow.Subtract(startTime),
                Error = ex.Message
            };
        }
    }

    /// <summary>
    /// Gets the instructions for this agent based on the Prompty template
    /// </summary>
    protected abstract string GetAgentInstructions();

    /// <summary>
    /// Gets the model ID to use for this agent
    /// </summary>
    protected virtual string GetModelId() => "gpt-4-turbo";

    /// <summary>
    /// Gets the temperature setting for this agent
    /// </summary>
    protected virtual double GetTemperature() => 0.2;

    /// <summary>
    /// Gets the maximum tokens for this agent
    /// </summary>
    protected virtual int GetMaxTokens() => 4000;

    /// <summary>
    /// Prepares the input for the agent based on the context
    /// </summary>
    protected virtual string PrepareInput(AgentExecutionContext context)
    {
        var input = new System.Text.StringBuilder();
        
        // Add the main requirements
        input.AppendLine($"Requirements: {context.Request.Requirements}");
        
        // Add non-functional requirements if available
        if (!string.IsNullOrEmpty(context.Request.NonFunctionalRequirements))
        {
            input.AppendLine($"Non-functional requirements: {context.Request.NonFunctionalRequirements}");
        }
        
        // Add business context if available
        if (!string.IsNullOrEmpty(context.Request.BusinessContext))
        {
            input.AppendLine($"Business context: {context.Request.BusinessContext}");
        }
        
        // Add constraints if available
        if (!string.IsNullOrEmpty(context.Request.Constraints))
        {
            input.AppendLine($"Constraints: {context.Request.Constraints}");
        }
        
        // Add iteration information
        input.AppendLine($"Current iteration: {context.Iteration}");
        
        // Add previous results if available
        if (context.PreviousResults.Any())
        {
            input.AppendLine("Previous agent results:");
            foreach (var result in context.PreviousResults)
            {
                input.AppendLine($"- {result.Key}: {result.Value.Output}");
            }
        }
        
        // Add errors if any
        if (context.Errors.Any())
        {
            input.AppendLine("Errors to address:");
            foreach (var error in context.Errors)
            {
                input.AppendLine($"- {error}");
            }
        }
        
        // Add warnings if any
        if (context.Warnings.Any())
        {
            input.AppendLine("Warnings to consider:");
            foreach (var warning in context.Warnings)
            {
                input.AppendLine($"- {warning}");
            }
        }

        return input.ToString();
    }

    /// <summary>
    /// Processes the result from the agent execution
    /// </summary>
    protected virtual AgentResult ProcessResult(ChatMessageContent result, AgentExecutionContext context)
    {
        return new AgentResult
        {
            Role = Role,
            Output = result.Content ?? string.Empty,
            Success = true,
            Metadata = new Dictionary<string, object>
            {
                ["model"] = GetModelId(),
                ["temperature"] = GetTemperature(),
                ["maxTokens"] = GetMaxTokens()
            }
        };
    }
} 