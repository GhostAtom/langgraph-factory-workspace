using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using GitHubKanbanAgent.Services;
using GitHubKanbanAgent.Models;

public static class GitHubWebhookFunction
{
    [FunctionName("GitHubWebhook")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "github/webhook")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("GitHub webhook received");

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
            
            // Process the event based on type
            var result = await ProcessGitHubEvent(eventType, requestBody, log);
            
            return new OkObjectResult(new { message = "Event processed successfully", eventType = eventType });
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error processing GitHub webhook");
            return new StatusCodeResult(500);
        }
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

            // Get the webhook secret from environment variables
            string webhookSecret = Environment.GetEnvironmentVariable("GITHUB_WEBHOOK_SECRET");
            if (string.IsNullOrEmpty(webhookSecret))
            {
                log.LogWarning("GitHub webhook secret not configured");
                return false;
            }

            // Read the request body
            req.Body.Position = 0;
            string requestBody = new StreamReader(req.Body).ReadToEndAsync().Result;
            req.Body.Position = 0;

            // Compute the hash
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

    private static async Task<object> ProcessGitHubEvent(string eventType, string payload, ILogger log)
    {
        // Use the KanbanEventProcessor for more advanced event handling
        var processor = new KanbanEventProcessor(log);
        var result = await processor.ProcessEventAsync(eventType, payload);
        
        if (result.Success)
        {
            log.LogInformation($"Successfully processed {eventType} event: {result.Message}");
            
            // Here you can add additional processing like:
            // - Storing events in a database
            // - Sending notifications
            // - Updating external systems
            // - Triggering workflows
            
            return new { 
                success = true, 
                message = result.Message,
                eventType = result.EventType,
                action = result.Action,
                priority = result.Priority.ToString(),
                metadata = result.Metadata
            };
        }
        else
        {
            log.LogWarning($"Failed to process {eventType} event: {result.Message}");
            return new { 
                success = false, 
                message = result.Message,
                eventType = result.EventType
            };
        }
    }

}