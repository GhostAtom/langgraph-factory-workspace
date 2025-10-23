using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GitHubKanbanAgent.Models;

namespace GitHubKanbanAgent.AI.Agents
{
    /// <summary>
    /// AI agent that handles code review automation and PR analysis
    /// </summary>
    public class CodeReviewAgent : IAIAgent
    {
        private readonly ILogger _logger;

        public CodeReviewAgent(ILogger logger)
        {
            _logger = logger;
        }

        public string AgentName => "CodeReviewAgent";
        public EventPriority MinPriority => EventPriority.Medium;
        public string[] SupportedEventTypes => new[] { "pull_request" };
        public string[] SupportedActions => new[] { "opened", "synchronize", "review_requested" };

        public async Task<AIAgentResponse> ProcessEventAsync(KanbanEventResult eventResult)
        {
            try
            {
                _logger.LogInformation($"CodeReviewAgent processing {eventResult.Action} event for PR #{eventResult.EntityNumber}");

                var review = await AnalyzePullRequest(eventResult);
                
                return new AIAgentResponse
                {
                    Success = true,
                    Message = $"Code review analysis completed: {review.Summary}",
                    ActionTaken = "analyzed_pull_request",
                    Data = review
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CodeReviewAgent");
                return new AIAgentResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private async Task<CodeReviewAnalysis> AnalyzePullRequest(KanbanEventResult eventResult)
        {
            // Simulate AI analysis (replace with actual AI service call)
            await Task.Delay(150); // Simulate processing time

            var labels = eventResult.Metadata.ContainsKey("labels") 
                ? (List<string>)eventResult.Metadata["labels"] 
                : new List<string>();

            var assignees = eventResult.Metadata.ContainsKey("assignees") 
                ? (List<string>)eventResult.Metadata["assignees"] 
                : new List<string>();

            var requestedReviewers = eventResult.Metadata.ContainsKey("requested_reviewers") 
                ? (List<string>)eventResult.Metadata["requested_reviewers"] 
                : new List<string>();

            var analysis = new CodeReviewAnalysis
            {
                PullRequestNumber = eventResult.EntityNumber ?? 0,
                Title = eventResult.EntityTitle,
                ReviewStatus = DetermineReviewStatus(eventResult.Action, requestedReviewers),
                RiskLevel = AssessRiskLevel(eventResult.EntityTitle, labels),
                SuggestedReviewers = SuggestReviewers(eventResult.EntityTitle, labels),
                CodeQualityScore = CalculateCodeQualityScore(eventResult.EntityTitle, labels),
                TestingRecommendations = GenerateTestingRecommendations(eventResult.EntityTitle, labels),
                SecurityConcerns = IdentifySecurityConcerns(eventResult.EntityTitle, labels),
                Summary = GenerateReviewSummary(eventResult.Action, labels, requestedReviewers)
            };

            return analysis;
        }

        private string DetermineReviewStatus(string action, List<string> requestedReviewers)
        {
            switch (action)
            {
                case "opened":
                    return "Needs Review";
                case "review_requested":
                    return "Review Requested";
                case "synchronize":
                    return "Updated - Review Needed";
                default:
                    return "Unknown";
            }
        }

        private string AssessRiskLevel(string title, List<string> labels)
        {
            var titleLower = title.ToLower();
            
            if (titleLower.Contains("security") || titleLower.Contains("auth") || 
                titleLower.Contains("permission") || labels.Contains("security"))
                return "High";
            
            if (titleLower.Contains("refactor") || titleLower.Contains("optimization") ||
                labels.Contains("refactor"))
                return "Medium";
            
            if (titleLower.Contains("docs") || titleLower.Contains("typo") ||
                labels.Contains("documentation"))
                return "Low";
            
            return "Medium";
        }

        private List<string> SuggestReviewers(string title, List<string> labels)
        {
            var suggestions = new List<string>();
            
            if (labels.Contains("frontend") || title.ToLower().Contains("ui"))
                suggestions.Add("frontend-senior");
            if (labels.Contains("backend") || title.ToLower().Contains("api"))
                suggestions.Add("backend-senior");
            if (labels.Contains("database") || title.ToLower().Contains("db"))
                suggestions.Add("database-expert");
            if (labels.Contains("security") || title.ToLower().Contains("auth"))
                suggestions.Add("security-expert");
            
            return suggestions;
        }

        private int CalculateCodeQualityScore(string title, List<string> labels)
        {
            // Simple scoring based on labels and title keywords
            var score = 70; // Base score
            
            if (labels.Contains("good-first-issue"))
                score += 10;
            if (labels.Contains("hacktoberfest"))
                score += 5;
            if (title.ToLower().Contains("fix") || title.ToLower().Contains("bug"))
                score += 15;
            if (title.ToLower().Contains("test"))
                score += 20;
            
            return Math.Min(100, score);
        }

        private List<string> GenerateTestingRecommendations(string title, List<string> labels)
        {
            var recommendations = new List<string>();
            
            if (title.ToLower().Contains("api") || labels.Contains("backend"))
            {
                recommendations.Add("Add unit tests for new API endpoints");
                recommendations.Add("Test error handling scenarios");
            }
            
            if (title.ToLower().Contains("ui") || labels.Contains("frontend"))
            {
                recommendations.Add("Add visual regression tests");
                recommendations.Add("Test responsive design");
            }
            
            if (labels.Contains("security"))
            {
                recommendations.Add("Perform security testing");
                recommendations.Add("Validate input sanitization");
            }
            
            return recommendations;
        }

        private List<string> IdentifySecurityConcerns(string title, List<string> labels)
        {
            var concerns = new List<string>();
            var titleLower = title.ToLower();
            
            if (titleLower.Contains("password") || titleLower.Contains("token"))
                concerns.Add("Sensitive data handling");
            
            if (titleLower.Contains("sql") || titleLower.Contains("query"))
                concerns.Add("SQL injection potential");
            
            if (titleLower.Contains("upload") || titleLower.Contains("file"))
                concerns.Add("File upload security");
            
            return concerns;
        }

        private string GenerateReviewSummary(string action, List<string> labels, List<string> requestedReviewers)
        {
            var summary = $"PR {action}";
            
            if (labels.Any())
                summary += $" with labels: {string.Join(", ", labels)}";
            
            if (requestedReviewers.Any())
                summary += $" - Review requested from: {string.Join(", ", requestedReviewers)}";
            
            return summary;
        }
    }

    public class CodeReviewAnalysis
    {
        public int PullRequestNumber { get; set; }
        public string Title { get; set; }
        public string ReviewStatus { get; set; }
        public string RiskLevel { get; set; }
        public List<string> SuggestedReviewers { get; set; }
        public int CodeQualityScore { get; set; }
        public List<string> TestingRecommendations { get; set; }
        public List<string> SecurityConcerns { get; set; }
        public string Summary { get; set; }
    }
}