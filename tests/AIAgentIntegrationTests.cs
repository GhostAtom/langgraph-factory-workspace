using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using GitHubKanbanAgent.AI;
using GitHubKanbanAgent.AI.Agents;
using GitHubKanbanAgent.Models;

namespace GitHubKanbanAgent.Tests
{
    public class AIAgentIntegrationTests
    {
        private readonly Mock<ILogger> _mockLogger;

        public AIAgentIntegrationTests()
        {
            _mockLogger = new Mock<ILogger>();
        }

        [Fact]
        public async Task AIAgentRouter_RoutesIssueEvent_ToCorrectAgents()
        {
            // Arrange
            var agents = new List<IAIAgent>
            {
                new IssueAnalysisAgent(_mockLogger.Object),
                new CodeReviewAgent(_mockLogger.Object),
                new ProjectManagementAgent(_mockLogger.Object)
            };
            
            var router = new AIAgentRouter(agents, _mockLogger.Object);
            
            var eventResult = new KanbanEventResult
            {
                Success = true,
                EventType = "issues",
                Action = "opened",
                Priority = EventPriority.High,
                EntityId = "12345",
                EntityNumber = 1,
                EntityTitle = "Test Issue",
                Repository = "test/repo"
            };

            // Act
            var responses = await router.RouteEventAsync(eventResult);

            // Assert
            Assert.NotNull(responses);
            Assert.Single(responses); // Only IssueAnalysisAgent should handle issues
            Assert.Equal("IssueAnalysisAgent", responses[0].AgentName);
            Assert.True(responses[0].Success);
        }

        [Fact]
        public async Task AIAgentRouter_RoutesPullRequestEvent_ToCorrectAgents()
        {
            // Arrange
            var agents = new List<IAIAgent>
            {
                new IssueAnalysisAgent(_mockLogger.Object),
                new CodeReviewAgent(_mockLogger.Object),
                new ProjectManagementAgent(_mockLogger.Object)
            };
            
            var router = new AIAgentRouter(agents, _mockLogger.Object);
            
            var eventResult = new KanbanEventResult
            {
                Success = true,
                EventType = "pull_request",
                Action = "opened",
                Priority = EventPriority.High,
                EntityId = "67890",
                EntityNumber = 2,
                EntityTitle = "Test PR",
                Repository = "test/repo"
            };

            // Act
            var responses = await router.RouteEventAsync(eventResult);

            // Assert
            Assert.NotNull(responses);
            Assert.Single(responses); // Only CodeReviewAgent should handle pull requests
            Assert.Equal("CodeReviewAgent", responses[0].AgentName);
            Assert.True(responses[0].Success);
        }

        [Fact]
        public async Task AIAgentRouter_RoutesProjectCardEvent_ToCorrectAgents()
        {
            // Arrange
            var agents = new List<IAIAgent>
            {
                new IssueAnalysisAgent(_mockLogger.Object),
                new CodeReviewAgent(_mockLogger.Object),
                new ProjectManagementAgent(_mockLogger.Object)
            };
            
            var router = new AIAgentRouter(agents, _mockLogger.Object);
            
            var eventResult = new KanbanEventResult
            {
                Success = true,
                EventType = "project_card",
                Action = "moved",
                Priority = EventPriority.High,
                EntityId = "11111",
                EntityTitle = "Test Card",
                Repository = "test/repo"
            };

            // Act
            var responses = await router.RouteEventAsync(eventResult);

            // Assert
            Assert.NotNull(responses);
            Assert.Single(responses); // Only ProjectManagementAgent should handle project cards
            Assert.Equal("ProjectManagementAgent", responses[0].AgentName);
            Assert.True(responses[0].Success);
        }

        [Fact]
        public async Task AIAgentRouter_FiltersByPriority()
        {
            // Arrange
            var agents = new List<IAIAgent>
            {
                new IssueAnalysisAgent(_mockLogger.Object),
                new CodeReviewAgent(_mockLogger.Object),
                new ProjectManagementAgent(_mockLogger.Object)
            };
            
            var router = new AIAgentRouter(agents, _mockLogger.Object);
            
            var eventResult = new KanbanEventResult
            {
                Success = true,
                EventType = "issues",
                Action = "opened",
                Priority = EventPriority.Low, // Low priority
                EntityId = "12345",
                EntityNumber = 1,
                EntityTitle = "Test Issue",
                Repository = "test/repo"
            };

            // Act
            var responses = await router.RouteEventAsync(eventResult);

            // Assert
            Assert.NotNull(responses);
            Assert.Single(responses); // IssueAnalysisAgent has MinPriority.Low, so it should handle this
            Assert.Equal("IssueAnalysisAgent", responses[0].AgentName);
        }

        [Fact]
        public async Task AIAgentRouter_FiltersByAction()
        {
            // Arrange
            var agents = new List<IAIAgent>
            {
                new IssueAnalysisAgent(_mockLogger.Object),
                new CodeReviewAgent(_mockLogger.Object),
                new ProjectManagementAgent(_mockLogger.Object)
            };
            
            var router = new AIAgentRouter(agents, _mockLogger.Object);
            
            var eventResult = new KanbanEventResult
            {
                Success = true,
                EventType = "issues",
                Action = "closed", // Not in IssueAnalysisAgent's SupportedActions
                Priority = EventPriority.Medium,
                EntityId = "12345",
                EntityNumber = 1,
                EntityTitle = "Test Issue",
                Repository = "test/repo"
            };

            // Act
            var responses = await router.RouteEventAsync(eventResult);

            // Assert
            Assert.NotNull(responses);
            Assert.Empty(responses); // No agents should handle "closed" action for issues
        }

        [Fact]
        public async Task IssueAnalysisAgent_ProcessesIssueEvent_ReturnsAnalysis()
        {
            // Arrange
            var agent = new IssueAnalysisAgent(_mockLogger.Object);
            var eventResult = new KanbanEventResult
            {
                Success = true,
                EventType = "issues",
                Action = "opened",
                Priority = EventPriority.High,
                EntityId = "12345",
                EntityNumber = 1,
                EntityTitle = "Bug in authentication system",
                Repository = "test/repo",
                Metadata = new Dictionary<string, object>
                {
                    ["labels"] = new List<string> { "bug", "critical" },
                    ["assignees"] = new List<string>(),
                    ["state"] = "open"
                }
            };

            // Act
            var response = await agent.ProcessEventAsync(eventResult);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("IssueAnalysisAgent", response.AgentName);
            Assert.Equal("analyzed_issue", response.ActionTaken);
            Assert.NotNull(response.Data);
            Assert.IsType<IssueAnalysis>(response.Data);
            
            var analysis = (IssueAnalysis)response.Data;
            Assert.Equal(1, analysis.IssueNumber);
            Assert.Equal("Bug in authentication system", analysis.Title);
            Assert.Equal(IssuePriority.High, analysis.Priority);
            Assert.Equal("Bug Fix", analysis.Category);
        }

        [Fact]
        public async Task CodeReviewAgent_ProcessesPullRequestEvent_ReturnsAnalysis()
        {
            // Arrange
            var agent = new CodeReviewAgent(_mockLogger.Object);
            var eventResult = new KanbanEventResult
            {
                Success = true,
                EventType = "pull_request",
                Action = "opened",
                Priority = EventPriority.High,
                EntityId = "67890",
                EntityNumber = 2,
                EntityTitle = "Add user authentication API",
                Repository = "test/repo",
                Metadata = new Dictionary<string, object>
                {
                    ["labels"] = new List<string> { "backend", "api" },
                    ["assignees"] = new List<string>(),
                    ["requested_reviewers"] = new List<string>(),
                    ["state"] = "open"
                }
            };

            // Act
            var response = await agent.ProcessEventAsync(eventResult);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("CodeReviewAgent", response.AgentName);
            Assert.Equal("analyzed_pull_request", response.ActionTaken);
            Assert.NotNull(response.Data);
            Assert.IsType<CodeReviewAnalysis>(response.Data);
            
            var analysis = (CodeReviewAnalysis)response.Data;
            Assert.Equal(2, analysis.PullRequestNumber);
            Assert.Equal("Add user authentication API", analysis.Title);
            Assert.Equal("Needs Review", analysis.ReviewStatus);
            Assert.Contains("backend-senior", analysis.SuggestedReviewers);
        }

        [Fact]
        public async Task ProjectManagementAgent_ProcessesProjectCardEvent_ReturnsAnalysis()
        {
            // Arrange
            var agent = new ProjectManagementAgent(_mockLogger.Object);
            var eventResult = new KanbanEventResult
            {
                Success = true,
                EventType = "project_card",
                Action = "moved",
                Priority = EventPriority.High,
                EntityId = "11111",
                EntityTitle = "Implement new feature",
                Repository = "test/repo"
            };

            // Act
            var response = await agent.ProcessEventAsync(eventResult);

            // Assert
            Assert.True(response.Success);
            Assert.Equal("ProjectManagementAgent", response.AgentName);
            Assert.Equal("analyzed_project_event", response.ActionTaken);
            Assert.NotNull(response.Data);
            Assert.IsType<ProjectManagementAnalysis>(response.Data);
            
            var analysis = (ProjectManagementAnalysis)response.Data;
            Assert.Equal("project_card", analysis.EventType);
            Assert.Equal("moved", analysis.Action);
            Assert.Equal("11111", analysis.EntityId);
            Assert.NotNull(analysis.WorkflowOptimization);
            Assert.NotNull(analysis.Recommendations);
        }

        [Fact]
        public async Task AIAgentRouter_HandlesAgentErrors_Gracefully()
        {
            // Arrange
            var mockAgent = new Mock<IAIAgent>();
            mockAgent.Setup(x => x.AgentName).Returns("MockAgent");
            mockAgent.Setup(x => x.MinPriority).Returns(EventPriority.Low);
            mockAgent.Setup(x => x.SupportedEventTypes).Returns(new[] { "issues" });
            mockAgent.Setup(x => x.SupportedActions).Returns(new[] { "opened" });
            mockAgent.Setup(x => x.ProcessEventAsync(It.IsAny<KanbanEventResult>()))
                .ThrowsAsync(new Exception("Test error"));

            var agents = new List<IAIAgent> { mockAgent.Object };
            var router = new AIAgentRouter(agents, _mockLogger.Object);
            
            var eventResult = new KanbanEventResult
            {
                Success = true,
                EventType = "issues",
                Action = "opened",
                Priority = EventPriority.High,
                EntityId = "12345",
                EntityNumber = 1,
                EntityTitle = "Test Issue",
                Repository = "test/repo"
            };

            // Act
            var responses = await router.RouteEventAsync(eventResult);

            // Assert
            Assert.NotNull(responses);
            Assert.Single(responses);
            Assert.False(responses[0].Success);
            Assert.Equal("MockAgent", responses[0].AgentName);
            Assert.Equal("Test error", responses[0].ErrorMessage);
        }
    }
}