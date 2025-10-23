using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Newtonsoft.Json;
using GitHubKanbanAgent.Services;
using GitHubKanbanAgent.Models;

namespace GitHubKanbanAgent.Tests
{
    public class GitHubWebhookTests
    {
        private readonly Mock<ILogger> _mockLogger;

        public GitHubWebhookTests()
        {
            _mockLogger = new Mock<ILogger>();
        }

        [Fact]
        public async Task ProcessIssuesEvent_OpenedAction_ReturnsSuccess()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var issueEvent = new
            {
                action = "opened",
                issue = new
                {
                    id = 12345,
                    number = 1,
                    title = "Test Issue",
                    state = "open",
                    labels = new[] { new { name = "bug" } },
                    assignees = new object[0],
                    milestone = (object)null,
                    html_url = "https://github.com/test/repo/issues/1"
                },
                repository = new
                {
                    full_name = "test/repo"
                }
            };

            var payload = JsonConvert.SerializeObject(issueEvent);

            // Act
            var result = await processor.ProcessEventAsync("issues", payload);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("issues", result.EventType);
            Assert.Equal("opened", result.Action);
            Assert.Equal("12345", result.EntityId);
            Assert.Equal(1, result.EntityNumber);
            Assert.Equal("Test Issue", result.EntityTitle);
            Assert.Equal("test/repo", result.Repository);
            Assert.Equal(EventPriority.High, result.Priority);
            Assert.Contains("New issue opened: Test Issue", result.Message);
        }

        [Fact]
        public async Task ProcessIssuesEvent_ClosedAction_ReturnsSuccess()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var issueEvent = new
            {
                action = "closed",
                issue = new
                {
                    id = 12345,
                    number = 1,
                    title = "Test Issue",
                    state = "closed",
                    labels = new object[0],
                    assignees = new object[0],
                    milestone = (object)null,
                    html_url = "https://github.com/test/repo/issues/1"
                },
                repository = new
                {
                    full_name = "test/repo"
                }
            };

            var payload = JsonConvert.SerializeObject(issueEvent);

            // Act
            var result = await processor.ProcessEventAsync("issues", payload);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("closed", result.Action);
            Assert.Equal(EventPriority.Medium, result.Priority);
            Assert.Contains("Issue closed: Test Issue", result.Message);
        }

        [Fact]
        public async Task ProcessPullRequestEvent_OpenedAction_ReturnsSuccess()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var prEvent = new
            {
                action = "opened",
                pull_request = new
                {
                    id = 67890,
                    number = 2,
                    title = "Test PR",
                    state = "open",
                    merged = false,
                    mergeable = true,
                    mergeable_state = "clean",
                    head = new { @ref = "feature-branch" },
                    base = new { @ref = "main" },
                    labels = new object[0],
                    assignees = new object[0],
                    requested_reviewers = new object[0],
                    html_url = "https://github.com/test/repo/pull/2"
                },
                repository = new
                {
                    full_name = "test/repo"
                }
            };

            var payload = JsonConvert.SerializeObject(prEvent);

            // Act
            var result = await processor.ProcessEventAsync("pull_request", payload);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("pull_request", result.EventType);
            Assert.Equal("opened", result.Action);
            Assert.Equal("67890", result.EntityId);
            Assert.Equal(2, result.EntityNumber);
            Assert.Equal("Test PR", result.EntityTitle);
            Assert.Equal(EventPriority.High, result.Priority);
            Assert.Contains("New pull request opened: Test PR", result.Message);
        }

        [Fact]
        public async Task ProcessPullRequestEvent_MergedAction_ReturnsSuccess()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var prEvent = new
            {
                action = "closed",
                pull_request = new
                {
                    id = 67890,
                    number = 2,
                    title = "Test PR",
                    state = "closed",
                    merged = true,
                    mergeable = true,
                    mergeable_state = "clean",
                    head = new { @ref = "feature-branch" },
                    base = new { @ref = "main" },
                    labels = new object[0],
                    assignees = new object[0],
                    requested_reviewers = new object[0],
                    html_url = "https://github.com/test/repo/pull/2"
                },
                repository = new
                {
                    full_name = "test/repo"
                }
            };

            var payload = JsonConvert.SerializeObject(prEvent);

            // Act
            var result = await processor.ProcessEventAsync("pull_request", payload);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("closed", result.Action);
            Assert.Equal(EventPriority.High, result.Priority);
            Assert.Contains("Pull request merged: Test PR", result.Message);
        }

        [Fact]
        public async Task ProcessProjectCardEvent_CreatedAction_ReturnsSuccess()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var cardEvent = new
            {
                action = "created",
                project_card = new
                {
                    id = 11111,
                    note = "Test card note",
                    column_id = 22222,
                    content_url = "https://github.com/test/repo/issues/1",
                    archived = false
                },
                repository = new
                {
                    full_name = "test/repo"
                }
            };

            var payload = JsonConvert.SerializeObject(cardEvent);

            // Act
            var result = await processor.ProcessEventAsync("project_card", payload);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("project_card", result.EventType);
            Assert.Equal("created", result.Action);
            Assert.Equal("11111", result.EntityId);
            Assert.Equal(EventPriority.Medium, result.Priority);
            Assert.Contains("Project card created: Test card note", result.Message);
        }

        [Fact]
        public async Task ProcessProjectCardEvent_MovedAction_ReturnsSuccess()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var cardEvent = new
            {
                action = "moved",
                project_card = new
                {
                    id = 11111,
                    note = "Test card note",
                    column_id = 33333,
                    content_url = "https://github.com/test/repo/issues/1",
                    archived = false
                },
                changes = new
                {
                    column_id = new
                    {
                        from = 22222
                    }
                },
                repository = new
                {
                    full_name = "test/repo"
                }
            };

            var payload = JsonConvert.SerializeObject(cardEvent);

            // Act
            var result = await processor.ProcessEventAsync("project_card", payload);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("moved", result.Action);
            Assert.Equal(EventPriority.High, result.Priority);
            Assert.Contains("Project card moved from column 22222 to 33333", result.Message);
        }

        [Fact]
        public async Task ProcessProjectEvent_CreatedAction_ReturnsSuccess()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var projectEvent = new
            {
                action = "created",
                project = new
                {
                    id = 44444,
                    name = "Test Project",
                    state = "open",
                    body = "Project description",
                    html_url = "https://github.com/test/repo/projects/1"
                },
                repository = new
                {
                    full_name = "test/repo"
                }
            };

            var payload = JsonConvert.SerializeObject(projectEvent);

            // Act
            var result = await processor.ProcessEventAsync("project", payload);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("project", result.EventType);
            Assert.Equal("created", result.Action);
            Assert.Equal("44444", result.EntityId);
            Assert.Equal("Test Project", result.EntityTitle);
            Assert.Equal(EventPriority.High, result.Priority);
            Assert.Contains("Project created: Test Project", result.Message);
        }

        [Fact]
        public async Task ProcessProjectColumnEvent_CreatedAction_ReturnsSuccess()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var columnEvent = new
            {
                action = "created",
                project_column = new
                {
                    id = 55555,
                    name = "Test Column",
                    project_url = "https://github.com/test/repo/projects/1"
                },
                repository = new
                {
                    full_name = "test/repo"
                }
            };

            var payload = JsonConvert.SerializeObject(columnEvent);

            // Act
            var result = await processor.ProcessEventAsync("project_column", payload);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("project_column", result.EventType);
            Assert.Equal("created", result.Action);
            Assert.Equal("55555", result.EntityId);
            Assert.Equal("Test Column", result.EntityTitle);
            Assert.Equal(EventPriority.Medium, result.Priority);
            Assert.Contains("Project column created: Test Column", result.Message);
        }

        [Fact]
        public async Task ProcessEvent_UnhandledEventType_ReturnsFailure()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var payload = "{}";

            // Act
            var result = await processor.ProcessEventAsync("unknown_event", payload);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("unknown_event", result.EventType);
            Assert.Contains("not supported", result.Message);
        }

        [Fact]
        public async Task ProcessEvent_InvalidJson_ReturnsFailure()
        {
            // Arrange
            var processor = new KanbanEventProcessor(_mockLogger.Object);
            var payload = "invalid json";

            // Act
            var result = await processor.ProcessEventAsync("issues", payload);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Error processing event", result.Message);
        }
    }
}