using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using GitHubKanbanAgent.Services;
using GitHubKanbanAgent.Models;
using GitHubKanbanAgent.AI;
using GitHubKanbanAgent.AI.Agents;

public static class EnhancedGitHubWebhookFunction
{
    private static readonly Lazy<AIAgentRouter> _aiRouter = new Lazy<AIAgentRouter>(() => 
    {
        var logger = new Logger<EnhancedGitHubWebhookFunction>(new LoggerFactory());
        var agents = new List<IAIAgent>
        {
            new IssueAnalysisAgent(logger),
            new CodeReviewAgent(logger),
            new ProjectManagementAgent(logger)
        };
        return new AIAgentRouter(agents, logger);
    });

    [FunctionName("EnhancedGitHubWebhook")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "github/webhook/enhanced")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("Enhanced GitHub webhook received");

        try
        {
            // Verify GitHub webhook signature
            if (!IsValidGitHubSignature(req, log))
            {
                log.LogWarning("Invalid GitHub signature");
                return new UnauthorizedResult();
            }

            // Get the event type
            string eventType = req.Headers["X-GitHub-Event"].FirstOrDefault();
            log.LogInformation($"GitHub event type: {eventType}");

            // Read the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            // Process the event
            var result = await ProcessEventWithAIAgents(eventType, requestBody, log);
            
            return new OkObjectResult(result);
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error processing enhanced GitHub webhook");
            return new StatusCodeResult(500);
        }
    }

    private static async Task<object> ProcessEventWithAIAgents(string eventType, string payload, ILogger log)
    {
        var startTime = DateTime.UtcNow;
        
        // Process the GitHub event
        var processor = new KanbanEventProcessor(log);
        var eventResult = await processor.ProcessEventAsync(eventType, payload);
        
        if (!eventResult.Success)
        {
            log.LogWarning($"Failed to process {eventType} event: {eventResult.Message}");
            return new { 
                success = false, 
                message = eventResult.Message,
                eventType = eventResult.EventType,
                processingTimeMs = (long)(DateTime.UtcNow - startTime).TotalMilliseconds
            };
        }

        log.LogInformation($"Successfully processed {eventType} event: {eventResult.Message}");

        // Route to AI agents
        var aiRouter = _aiRouter.Value;
        var aiResponses = await aiRouter.RouteEventAsync(eventResult);
        
        var totalProcessingTime = (long)(DateTime.UtcNow - startTime).TotalMilliseconds;
        
        log.LogInformation($"Triggered {aiResponses.Count} AI agents for {eventType} event in {totalProcessingTime}ms");

        // Analyze AI agent responses
        var successfulAgents = aiResponses.Count(r => r.Success);
        var failedAgents = aiResponses.Count(r => !r.Success);
        var totalAgentTime = aiResponses.Sum(r => r.ProcessingTimeMs);

        // Log detailed AI agent responses
        foreach (var aiResponse in aiResponses)
        {
            if (aiResponse.Success)
            {
                log.LogInformation($"✅ AI Agent {aiResponse.AgentName}: {aiResponse.Message} (took {aiResponse.ProcessingTimeMs}ms)");
                if (!string.IsNullOrEmpty(aiResponse.ActionTaken))
                {
                    log.LogInformation($"   Action: {aiResponse.ActionTaken}");
                }
            }
            else
            {
                log.LogWarning($"❌ AI Agent {aiResponse.AgentName} failed: {aiResponse.ErrorMessage}");
            }
        }

        // Generate summary
        var summary = GenerateEventSummary(eventResult, aiResponses, totalProcessingTime);

        return new { 
            success = true, 
            eventType = eventResult.EventType,
            action = eventResult.Action,
            priority = eventResult.Priority.ToString(),
            message = eventResult.Message,
            repository = eventResult.Repository,
            entityId = eventResult.EntityId,
            entityNumber = eventResult.EntityNumber,
            entityTitle = eventResult.EntityTitle,
            
            // AI Agent Results
            aiAgentsTriggered = aiResponses.Count,
            aiAgentsSuccessful = successfulAgents,
            aiAgentsFailed = failedAgents,
            totalAgentProcessingTimeMs = totalAgentTime,
            totalProcessingTimeMs = totalProcessingTime,
            
            // AI Agent Details
            aiResponses = aiResponses.Select(r => new {
                agentName = r.AgentName,
                success = r.Success,
                message = r.Message,
                actionTaken = r.ActionTaken,
                processingTimeMs = r.ProcessingTimeMs,
                errorMessage = r.ErrorMessage,
                data = r.Data
            }).ToList(),
            
            // Summary
            summary = summary,
            
            // Metadata
            metadata = eventResult.Metadata,
            timestamp = DateTime.UtcNow.ToString("O")
        };
    }

    private static string GenerateEventSummary(KanbanEventResult eventResult, List<AIAgentResponse> aiResponses, long totalProcessingTime)
    {
        var summary = $"Processed {eventResult.EventType} event '{eventResult.Action}'";
        
        if (!string.IsNullOrEmpty(eventResult.EntityTitle))
        {
            summary += $" for '{eventResult.EntityTitle}'";
        }
        
        summary += $" in {totalProcessingTime}ms";
        
        if (aiResponses.Any())
        {
            var successfulActions = aiResponses
                .Where(r => r.Success && !string.IsNullOrEmpty(r.ActionTaken))
                .Select(r => r.ActionTaken)
                .Distinct()
                .ToList();
                
            if (successfulActions.Any())
            {
                summary += $". AI agents performed: {string.Join(", ", successfulActions)}";
            }
        }
        
        return summary;
    }

    private static bool IsValidGitHubSignature(HttpRequest req, ILogger log)
    {
        try
        {
            string signature = req.Headers["X-Hub-Signature-256"].FirstOrDefault();
            if (string.IsNullOrEmpty(signature))
            {
                log.LogWarning("Missing GitHub signature");
                return false;
            }

            string webhookSecret = Environment.GetEnvironmentVariable("GITHUB_WEBHOOK_SECRET");
            if (string.IsNullOrEmpty(webhookSecret))
            {
                log.LogWarning("GitHub webhook secret not configured");
                return false;
            }

            req.Body.Position = 0;
            string requestBody = new StreamReader(req.Body).ReadToEndAsync().Result;
            req.Body.Position = 0;

            string computedSignature = "sha256=" + ComputeHmacSha256(webhookSecret, requestBody);
            
            return string.Equals(signature, computedSignature, StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error validating GitHub signature");
            return false;
        }
    }

    private static string ComputeHmacSha256(string secret, string payload)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
        {
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            return Convert.ToHexString(hashBytes).ToLower();
        }
    }
}