using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GitHubKanbanAgent.Models;

namespace GitHubKanbanAgent.AI.Agents
{
    /// <summary>
    /// AI agent that analyzes issues and provides insights
    /// </summary>
    public class IssueAnalysisAgent : IAIAgent
    {
        private readonly ILogger _logger;

        public IssueAnalysisAgent(ILogger logger)
        {
            _logger = logger;
        }

        public string AgentName => "IssueAnalysisAgent";
        public EventPriority MinPriority => EventPriority.Low;
        public string[] SupportedEventTypes => new[] { "issues" };
        public string[] SupportedActions => new[] { "opened", "labeled", "assigned" };

        public async Task<AIAgentResponse> ProcessEventAsync(KanbanEventResult eventResult)
        {
            try
            {
                _logger.LogInformation($"IssueAnalysisAgent processing {eventResult.Action} event for issue #{eventResult.EntityNumber}");

                var analysis = await AnalyzeIssue(eventResult);
                
                return new AIAgentResponse
                {
                    Success = true,
                    Message = $"Issue analysis completed: {analysis.Summary}",
                    ActionTaken = "analyzed_issue",
                    Data = analysis
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in IssueAnalysisAgent");
                return new AIAgentResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        private async Task<IssueAnalysis> AnalyzeIssue(KanbanEventResult eventResult)
        {
            // Simulate AI analysis (replace with actual AI service call)
            await Task.Delay(100); // Simulate processing time

            var labels = eventResult.Metadata.ContainsKey("labels") 
                ? (List<string>)eventResult.Metadata["labels"] 
                : new List<string>();

            var assignees = eventResult.Metadata.ContainsKey("assignees") 
                ? (List<string>)eventResult.Metadata["assignees"] 
                : new List<string>();

            var analysis = new IssueAnalysis
            {
                IssueNumber = eventResult.EntityNumber ?? 0,
                Title = eventResult.EntityTitle,
                Priority = DeterminePriority(labels),
                Category = CategorizeIssue(eventResult.EntityTitle, labels),
                SuggestedAssignees = SuggestAssignees(eventResult.EntityTitle, labels),
                EstimatedEffort = EstimateEffort(eventResult.EntityTitle, labels),
                RelatedIssues = FindRelatedIssues(eventResult.EntityTitle),
                Summary = GenerateSummary(eventResult.Action, labels, assignees)
            };

            return analysis;
        }

        private IssuePriority DeterminePriority(List<string> labels)
        {
            if (labels.Contains("bug") || labels.Contains("critical"))
                return IssuePriority.High;
            if (labels.Contains("enhancement") || labels.Contains("feature"))
                return IssuePriority.Medium;
            return IssuePriority.Low;
        }

        private string CategorizeIssue(string title, List<string> labels)
        {
            var titleLower = title.ToLower();
            
            if (titleLower.Contains("bug") || titleLower.Contains("error") || titleLower.Contains("fix"))
                return "Bug Fix";
            if (titleLower.Contains("feature") || titleLower.Contains("add") || titleLower.Contains("implement"))
                return "Feature Request";
            if (titleLower.Contains("refactor") || titleLower.Contains("improve"))
                return "Refactoring";
            if (titleLower.Contains("documentation") || titleLower.Contains("docs"))
                return "Documentation";
            
            return "General";
        }

        private List<string> SuggestAssignees(string title, List<string> labels)
        {
            // This would typically use AI to analyze the issue content and suggest appropriate team members
            var suggestions = new List<string>();
            
            if (labels.Contains("frontend") || title.ToLower().Contains("ui"))
                suggestions.Add("frontend-team");
            if (labels.Contains("backend") || title.ToLower().Contains("api"))
                suggestions.Add("backend-team");
            if (labels.Contains("database") || title.ToLower().Contains("db"))
                suggestions.Add("database-team");
            
            return suggestions;
        }

        private string EstimateEffort(string title, List<string> labels)
        {
            // Simple estimation based on keywords and labels
            var titleLower = title.ToLower();
            
            if (titleLower.Contains("simple") || titleLower.Contains("quick"))
                return "1-2 hours";
            if (titleLower.Contains("complex") || titleLower.Contains("major"))
                return "1-2 weeks";
            if (labels.Contains("bug"))
                return "2-4 hours";
            
            return "1-3 days";
        }

        private List<string> FindRelatedIssues(string title)
        {
            // This would typically use AI to find semantically related issues
            return new List<string>();
        }

        private string GenerateSummary(string action, List<string> labels, List<string> assignees)
        {
            var summary = $"Issue {action}";
            
            if (labels.Any())
                summary += $" with labels: {string.Join(", ", labels)}";
            
            if (assignees.Any())
                summary += $" assigned to: {string.Join(", ", assignees)}";
            
            return summary;
        }
    }

    public class IssueAnalysis
    {
        public int IssueNumber { get; set; }
        public string Title { get; set; }
        public IssuePriority Priority { get; set; }
        public string Category { get; set; }
        public List<string> SuggestedAssignees { get; set; }
        public string EstimatedEffort { get; set; }
        public List<string> RelatedIssues { get; set; }
        public string Summary { get; set; }
    }

    public enum IssuePriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}