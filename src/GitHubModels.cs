using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitHubKanbanAgent.Models
{
    // Base GitHub event structure
    public class GitHubEvent
    {
        [JsonProperty("action")]
        public string Action { get; set; }
    }

    // Issue event models
    public class GitHubIssueEvent : GitHubEvent
    {
        [JsonProperty("issue")]
        public GitHubIssue Issue { get; set; }

        [JsonProperty("repository")]
        public GitHubRepository Repository { get; set; }

        [JsonProperty("sender")]
        public GitHubUser Sender { get; set; }
    }

    public class GitHubIssue
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("labels")]
        public List<GitHubLabel> Labels { get; set; }

        [JsonProperty("assignees")]
        public List<GitHubUser> Assignees { get; set; }

        [JsonProperty("milestone")]
        public GitHubMilestone Milestone { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("closed_at")]
        public string ClosedAt { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }

    // Pull request event models
    public class GitHubPullRequestEvent : GitHubEvent
    {
        [JsonProperty("pull_request")]
        public GitHubPullRequest PullRequest { get; set; }

        [JsonProperty("repository")]
        public GitHubRepository Repository { get; set; }

        [JsonProperty("sender")]
        public GitHubUser Sender { get; set; }
    }

    public class GitHubPullRequest
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("merged")]
        public bool Merged { get; set; }

        [JsonProperty("mergeable")]
        public bool? Mergeable { get; set; }

        [JsonProperty("mergeable_state")]
        public string MergeableState { get; set; }

        [JsonProperty("head")]
        public GitHubBranch Head { get; set; }

        [JsonProperty("base")]
        public GitHubBranch Base { get; set; }

        [JsonProperty("labels")]
        public List<GitHubLabel> Labels { get; set; }

        [JsonProperty("assignees")]
        public List<GitHubUser> Assignees { get; set; }

        [JsonProperty("requested_reviewers")]
        public List<GitHubUser> RequestedReviewers { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("closed_at")]
        public string ClosedAt { get; set; }

        [JsonProperty("merged_at")]
        public string MergedAt { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }

    // Project card event models
    public class GitHubProjectCardEvent : GitHubEvent
    {
        [JsonProperty("project_card")]
        public GitHubProjectCard ProjectCard { get; set; }

        [JsonProperty("repository")]
        public GitHubRepository Repository { get; set; }

        [JsonProperty("sender")]
        public GitHubUser Sender { get; set; }

        [JsonProperty("changes")]
        public GitHubProjectCardChanges Changes { get; set; }
    }

    public class GitHubProjectCard
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("column_id")]
        public long ColumnId { get; set; }

        [JsonProperty("content_url")]
        public string ContentUrl { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }
    }

    public class GitHubProjectCardChanges
    {
        [JsonProperty("column_id")]
        public GitHubColumnIdChange ColumnId { get; set; }

        [JsonProperty("note")]
        public GitHubNoteChange Note { get; set; }
    }

    public class GitHubColumnIdChange
    {
        [JsonProperty("from")]
        public long? From { get; set; }
    }

    public class GitHubNoteChange
    {
        [JsonProperty("from")]
        public string From { get; set; }
    }

    // Project event models
    public class GitHubProjectEvent : GitHubEvent
    {
        [JsonProperty("project")]
        public GitHubProject Project { get; set; }

        [JsonProperty("repository")]
        public GitHubRepository Repository { get; set; }

        [JsonProperty("sender")]
        public GitHubUser Sender { get; set; }
    }

    public class GitHubProject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }

    // Project column event models
    public class GitHubProjectColumnEvent : GitHubEvent
    {
        [JsonProperty("project_column")]
        public GitHubProjectColumn ProjectColumn { get; set; }

        [JsonProperty("repository")]
        public GitHubRepository Repository { get; set; }

        [JsonProperty("sender")]
        public GitHubUser Sender { get; set; }
    }

    public class GitHubProjectColumn
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("project_url")]
        public string ProjectUrl { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }

    // Common models
    public class GitHubRepository
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }
    }

    public class GitHubUser
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }

    public class GitHubLabel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class GitHubMilestone
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("due_on")]
        public string DueOn { get; set; }
    }

    public class GitHubBranch
    {
        [JsonProperty("ref")]
        public string Ref { get; set; }

        [JsonProperty("sha")]
        public string Sha { get; set; }

        [JsonProperty("repo")]
        public GitHubRepository Repo { get; set; }
    }
}