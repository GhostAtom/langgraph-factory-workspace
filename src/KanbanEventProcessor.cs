using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GitHubKanbanAgent.Services
{
    public class KanbanEventProcessor
    {
        private readonly ILogger _logger;

        public KanbanEventProcessor(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<KanbanEventResult> ProcessEventAsync(string eventType, string payload)
        {
            try
            {
                _logger.LogInformation($"Processing Kanban event: {eventType}");

                switch (eventType)
                {
                    case "issues":
                        return await ProcessIssuesEventAsync(payload);
                    case "pull_request":
                        return await ProcessPullRequestEventAsync(payload);
                    case "project_card":
                        return await ProcessProjectCardEventAsync(payload);
                    case "project":
                        return await ProcessProjectEventAsync(payload);
                    case "project_column":
                        return await ProcessProjectColumnEventAsync(payload);
                    default:
                        _logger.LogWarning($"Unhandled event type: {eventType}");
                        return new KanbanEventResult
                        {
                            Success = false,
                            Message = $"Event type '{eventType}' is not supported",
                            EventType = eventType
                        };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing {eventType} event");
                return new KanbanEventResult
                {
                    Success = false,
                    Message = $"Error processing event: {ex.Message}",
                    EventType = eventType
                };
            }
        }

        private async Task<KanbanEventResult> ProcessIssuesEventAsync(string payload)
        {
            var issueEvent = JsonConvert.DeserializeObject<GitHubIssueEvent>(payload);
            var result = new KanbanEventResult
            {
                Success = true,
                EventType = "issues",
                Action = issueEvent.Action,
                EntityId = issueEvent.Issue.Id.ToString(),
                EntityNumber = issueEvent.Issue.Number,
                EntityTitle = issueEvent.Issue.Title,
                Repository = issueEvent.Repository.FullName
            };

            // Process different issue actions
            switch (issueEvent.Action)
            {
                case "opened":
                    result.Message = $"New issue opened: {issueEvent.Issue.Title}";
                    result.Priority = EventPriority.High;
                    break;
                case "closed":
                    result.Message = $"Issue closed: {issueEvent.Issue.Title}";
                    result.Priority = EventPriority.Medium;
                    break;
                case "labeled":
                    var labels = string.Join(", ", issueEvent.Issue.Labels.Select(l => l.Name));
                    result.Message = $"Issue labeled: {issueEvent.Issue.Title} with {labels}";
                    result.Priority = EventPriority.Low;
                    break;
                case "assigned":
                    var assignees = string.Join(", ", issueEvent.Issue.Assignees.Select(a => a.Login));
                    result.Message = $"Issue assigned: {issueEvent.Issue.Title} to {assignees}";
                    result.Priority = EventPriority.Medium;
                    break;
                case "milestoned":
                    result.Message = $"Issue milestoned: {issueEvent.Issue.Title} to {issueEvent.Issue.Milestone?.Title}";
                    result.Priority = EventPriority.Medium;
                    break;
                default:
                    result.Message = $"Issue {issueEvent.Action}: {issueEvent.Issue.Title}";
                    result.Priority = EventPriority.Low;
                    break;
            }

            // Add metadata
            result.Metadata = new Dictionary<string, object>
            {
                ["state"] = issueEvent.Issue.State,
                ["labels"] = issueEvent.Issue.Labels.Select(l => l.Name).ToList(),
                ["assignees"] = issueEvent.Issue.Assignees.Select(a => a.Login).ToList(),
                ["milestone"] = issueEvent.Issue.Milestone?.Title,
                ["html_url"] = issueEvent.Issue.HtmlUrl
            };

            return result;
        }

        private async Task<KanbanEventResult> ProcessPullRequestEventAsync(string payload)
        {
            var prEvent = JsonConvert.DeserializeObject<GitHubPullRequestEvent>(payload);
            var result = new KanbanEventResult
            {
                Success = true,
                EventType = "pull_request",
                Action = prEvent.Action,
                EntityId = prEvent.PullRequest.Id.ToString(),
                EntityNumber = prEvent.PullRequest.Number,
                EntityTitle = prEvent.PullRequest.Title,
                Repository = prEvent.Repository.FullName
            };

            // Process different PR actions
            switch (prEvent.Action)
            {
                case "opened":
                    result.Message = $"New pull request opened: {prEvent.PullRequest.Title}";
                    result.Priority = EventPriority.High;
                    break;
                case "closed":
                    if (prEvent.PullRequest.Merged)
                    {
                        result.Message = $"Pull request merged: {prEvent.PullRequest.Title}";
                        result.Priority = EventPriority.High;
                    }
                    else
                    {
                        result.Message = $"Pull request closed: {prEvent.PullRequest.Title}";
                        result.Priority = EventPriority.Medium;
                    }
                    break;
                case "review_requested":
                    var reviewers = string.Join(", ", prEvent.PullRequest.RequestedReviewers.Select(r => r.Login));
                    result.Message = $"Review requested for PR: {prEvent.PullRequest.Title} from {reviewers}";
                    result.Priority = EventPriority.Medium;
                    break;
                case "synchronize":
                    result.Message = $"Pull request updated: {prEvent.PullRequest.Title}";
                    result.Priority = EventPriority.Low;
                    break;
                default:
                    result.Message = $"Pull request {prEvent.Action}: {prEvent.PullRequest.Title}";
                    result.Priority = EventPriority.Low;
                    break;
            }

            // Add metadata
            result.Metadata = new Dictionary<string, object>
            {
                ["state"] = prEvent.PullRequest.State,
                ["merged"] = prEvent.PullRequest.Merged,
                ["mergeable"] = prEvent.PullRequest.Mergeable,
                ["mergeable_state"] = prEvent.PullRequest.MergeableState,
                ["head_ref"] = prEvent.PullRequest.Head.Ref,
                ["base_ref"] = prEvent.PullRequest.Base.Ref,
                ["labels"] = prEvent.PullRequest.Labels.Select(l => l.Name).ToList(),
                ["assignees"] = prEvent.PullRequest.Assignees.Select(a => a.Login).ToList(),
                ["requested_reviewers"] = prEvent.PullRequest.RequestedReviewers.Select(r => r.Login).ToList(),
                ["html_url"] = prEvent.PullRequest.HtmlUrl
            };

            return result;
        }

        private async Task<KanbanEventResult> ProcessProjectCardEventAsync(string payload)
        {
            var cardEvent = JsonConvert.DeserializeObject<GitHubProjectCardEvent>(payload);
            var result = new KanbanEventResult
            {
                Success = true,
                EventType = "project_card",
                Action = cardEvent.Action,
                EntityId = cardEvent.ProjectCard.Id.ToString(),
                Repository = cardEvent.Repository.FullName
            };

            // Process different project card actions
            switch (cardEvent.Action)
            {
                case "created":
                    result.Message = $"Project card created: {cardEvent.ProjectCard.Note}";
                    result.Priority = EventPriority.Medium;
                    break;
                case "moved":
                    var fromColumn = cardEvent.Changes?.ColumnId?.From?.ToString() ?? "unknown";
                    var toColumn = cardEvent.ProjectCard.ColumnId.ToString();
                    result.Message = $"Project card moved from column {fromColumn} to {toColumn}";
                    result.Priority = EventPriority.High;
                    break;
                case "deleted":
                    result.Message = "Project card deleted";
                    result.Priority = EventPriority.Medium;
                    break;
                case "converted":
                    result.Message = $"Project card converted: {cardEvent.ProjectCard.Note}";
                    result.Priority = EventPriority.Medium;
                    break;
                default:
                    result.Message = $"Project card {cardEvent.Action}";
                    result.Priority = EventPriority.Low;
                    break;
            }

            // Add metadata
            result.Metadata = new Dictionary<string, object>
            {
                ["note"] = cardEvent.ProjectCard.Note,
                ["column_id"] = cardEvent.ProjectCard.ColumnId,
                ["content_url"] = cardEvent.ProjectCard.ContentUrl,
                ["archived"] = cardEvent.ProjectCard.Archived,
                ["from_column_id"] = cardEvent.Changes?.ColumnId?.From
            };

            return result;
        }

        private async Task<KanbanEventResult> ProcessProjectEventAsync(string payload)
        {
            var projectEvent = JsonConvert.DeserializeObject<GitHubProjectEvent>(payload);
            var result = new KanbanEventResult
            {
                Success = true,
                EventType = "project",
                Action = projectEvent.Action,
                EntityId = projectEvent.Project.Id.ToString(),
                EntityTitle = projectEvent.Project.Name,
                Repository = projectEvent.Repository.FullName
            };

            // Process different project actions
            switch (projectEvent.Action)
            {
                case "created":
                    result.Message = $"Project created: {projectEvent.Project.Name}";
                    result.Priority = EventPriority.High;
                    break;
                case "updated":
                    result.Message = $"Project updated: {projectEvent.Project.Name}";
                    result.Priority = EventPriority.Medium;
                    break;
                case "closed":
                    result.Message = $"Project closed: {projectEvent.Project.Name}";
                    result.Priority = EventPriority.High;
                    break;
                case "reopened":
                    result.Message = $"Project reopened: {projectEvent.Project.Name}";
                    result.Priority = EventPriority.Medium;
                    break;
                default:
                    result.Message = $"Project {projectEvent.Action}: {projectEvent.Project.Name}";
                    result.Priority = EventPriority.Low;
                    break;
            }

            // Add metadata
            result.Metadata = new Dictionary<string, object>
            {
                ["name"] = projectEvent.Project.Name,
                ["state"] = projectEvent.Project.State,
                ["body"] = projectEvent.Project.Body,
                ["html_url"] = projectEvent.Project.HtmlUrl
            };

            return result;
        }

        private async Task<KanbanEventResult> ProcessProjectColumnEventAsync(string payload)
        {
            var columnEvent = JsonConvert.DeserializeObject<GitHubProjectColumnEvent>(payload);
            var result = new KanbanEventResult
            {
                Success = true,
                EventType = "project_column",
                Action = columnEvent.Action,
                EntityId = columnEvent.ProjectColumn.Id.ToString(),
                EntityTitle = columnEvent.ProjectColumn.Name,
                Repository = columnEvent.Repository.FullName
            };

            // Process different project column actions
            switch (columnEvent.Action)
            {
                case "created":
                    result.Message = $"Project column created: {columnEvent.ProjectColumn.Name}";
                    result.Priority = EventPriority.Medium;
                    break;
                case "updated":
                    result.Message = $"Project column updated: {columnEvent.ProjectColumn.Name}";
                    result.Priority = EventPriority.Low;
                    break;
                case "moved":
                    result.Message = $"Project column moved: {columnEvent.ProjectColumn.Name}";
                    result.Priority = EventPriority.Medium;
                    break;
                case "deleted":
                    result.Message = $"Project column deleted: {columnEvent.ProjectColumn.Name}";
                    result.Priority = EventPriority.High;
                    break;
                default:
                    result.Message = $"Project column {columnEvent.Action}: {columnEvent.ProjectColumn.Name}";
                    result.Priority = EventPriority.Low;
                    break;
            }

            // Add metadata
            result.Metadata = new Dictionary<string, object>
            {
                ["name"] = columnEvent.ProjectColumn.Name,
                ["project_url"] = columnEvent.ProjectColumn.ProjectUrl
            };

            return result;
        }
    }

    public class KanbanEventResult
    {
        public bool Success { get; set; }
        public string EventType { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public string EntityId { get; set; }
        public int? EntityNumber { get; set; }
        public string EntityTitle { get; set; }
        public string Repository { get; set; }
        public EventPriority Priority { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }

    public enum EventPriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}