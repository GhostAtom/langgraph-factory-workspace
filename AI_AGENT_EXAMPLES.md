# AI Agent Integration Examples

This document demonstrates how the GitHub Kanban Events Agent triggers appropriate AI agents based on different GitHub events.

## How It Works

1. **GitHub Event Received** → Webhook receives event from GitHub
2. **Event Processing** → Event is processed and categorized
3. **AI Agent Routing** → Appropriate AI agents are selected based on event type, priority, and action
4. **AI Processing** → Each selected agent processes the event and returns insights
5. **Response Aggregation** → All AI agent responses are combined and returned

## Example Scenarios

### Scenario 1: New Issue Created

**GitHub Event:**
```json
{
  "action": "opened",
  "issue": {
    "id": 12345,
    "number": 1,
    "title": "Bug: Authentication fails on mobile devices",
    "labels": [{"name": "bug"}, {"name": "critical"}, {"name": "mobile"}]
  }
}
```

**AI Agents Triggered:**
- ✅ **IssueAnalysisAgent** - Analyzes the issue and provides insights

**AI Agent Response:**
```json
{
  "agentName": "IssueAnalysisAgent",
  "success": true,
  "message": "Issue analysis completed: New issue opened: Bug: Authentication fails on mobile devices with labels: bug, critical, mobile",
  "actionTaken": "analyzed_issue",
  "data": {
    "issueNumber": 1,
    "title": "Bug: Authentication fails on mobile devices",
    "priority": "High",
    "category": "Bug Fix",
    "suggestedAssignees": ["mobile-team", "security-team"],
    "estimatedEffort": "2-4 hours",
    "relatedIssues": [],
    "summary": "Critical bug in authentication system affecting mobile users"
  }
}
```

### Scenario 2: Pull Request Opened

**GitHub Event:**
```json
{
  "action": "opened",
  "pull_request": {
    "id": 67890,
    "number": 2,
    "title": "Add user authentication API endpoints",
    "labels": [{"name": "backend"}, {"name": "api"}, {"name": "security"}]
  }
}
```

**AI Agents Triggered:**
- ✅ **CodeReviewAgent** - Analyzes the PR for code quality and security

**AI Agent Response:**
```json
{
  "agentName": "CodeReviewAgent",
  "success": true,
  "message": "Code review analysis completed: New pull request opened: Add user authentication API endpoints",
  "actionTaken": "analyzed_pull_request",
  "data": {
    "pullRequestNumber": 2,
    "title": "Add user authentication API endpoints",
    "reviewStatus": "Needs Review",
    "riskLevel": "High",
    "suggestedReviewers": ["backend-senior", "security-expert"],
    "codeQualityScore": 85,
    "testingRecommendations": [
      "Add unit tests for new API endpoints",
      "Test error handling scenarios",
      "Perform security testing",
      "Validate input sanitization"
    ],
    "securityConcerns": ["Sensitive data handling"],
    "summary": "High-risk PR requiring security expert review"
  }
}
```

### Scenario 3: Project Card Moved

**GitHub Event:**
```json
{
  "action": "moved",
  "project_card": {
    "id": 11111,
    "note": "Implement new dashboard feature",
    "column_id": 33333
  },
  "changes": {
    "column_id": {
      "from": 22222
    }
  }
}
```

**AI Agents Triggered:**
- ✅ **ProjectManagementAgent** - Analyzes workflow and project management aspects

**AI Agent Response:**
```json
{
  "agentName": "ProjectManagementAgent",
  "success": true,
  "message": "Project management analysis completed: Card movement detected - check for workflow efficiency",
  "actionTaken": "analyzed_project_event",
  "data": {
    "eventType": "project_card",
    "action": "moved",
    "entityId": "11111",
    "entityTitle": "Implement new dashboard feature",
    "workflowOptimization": [
      "Card movement detected - check for workflow efficiency",
      "Consider automating similar card movements"
    ],
    "bottleneckDetection": [
      "Monitor card movement patterns for bottlenecks",
      "Check if any columns are consistently overloaded"
    ],
    "resourceAllocation": [],
    "timelineImpact": "Card movement may impact project timeline",
    "recommendations": [
      "Review card priority and dependencies",
      "Ensure proper labeling and categorization"
    ],
    "summary": "project_card moved: Implement new dashboard feature - Project management analysis completed"
  }
}
```

### Scenario 4: Multiple Agents for Complex Event

**GitHub Event:**
```json
{
  "action": "review_requested",
  "pull_request": {
    "id": 67890,
    "number": 3,
    "title": "Fix critical security vulnerability",
    "labels": [{"name": "security"}, {"name": "critical"}, {"name": "hotfix"}]
  }
}
```

**AI Agents Triggered:**
- ✅ **CodeReviewAgent** - Analyzes the security PR
- ✅ **ProjectManagementAgent** - Analyzes project impact (if configured for PR events)

**Combined AI Agent Response:**
```json
{
  "aiAgentsTriggered": 2,
  "aiAgentsSuccessful": 2,
  "aiAgentsFailed": 0,
  "aiResponses": [
    {
      "agentName": "CodeReviewAgent",
      "success": true,
      "message": "Code review analysis completed: Review requested for PR: Fix critical security vulnerability",
      "actionTaken": "analyzed_pull_request",
      "data": {
        "riskLevel": "High",
        "securityConcerns": ["Sensitive data handling", "SQL injection potential"],
        "suggestedReviewers": ["security-expert", "backend-senior"]
      }
    },
    {
      "agentName": "ProjectManagementAgent",
      "success": true,
      "message": "Project management analysis completed: High-priority security fix requires immediate attention",
      "actionTaken": "analyzed_project_event",
      "data": {
        "workflowOptimization": ["Expedite security fixes through fast-track process"],
        "recommendations": ["Prioritize this PR above all other work", "Consider emergency deployment"]
      }
    }
  ]
}
```

## Agent Selection Logic

### IssueAnalysisAgent
- **Event Types**: `issues`
- **Actions**: `opened`, `labeled`, `assigned`
- **Priority**: Low and above
- **Purpose**: Analyzes issues for priority, category, assignee suggestions, effort estimation

### CodeReviewAgent
- **Event Types**: `pull_request`
- **Actions**: `opened`, `synchronize`, `review_requested`
- **Priority**: Medium and above
- **Purpose**: Analyzes PRs for code quality, security, reviewer suggestions

### ProjectManagementAgent
- **Event Types**: `project_card`, `project`, `project_column`
- **Actions**: `created`, `moved`, `deleted`, `updated`
- **Priority**: Low and above
- **Purpose**: Analyzes project workflow, bottlenecks, resource allocation

## Configuration Options

### Enable/Disable Agents
```json
{
  "ENABLE_ISSUE_ANALYSIS_AGENT": "true",
  "ENABLE_CODE_REVIEW_AGENT": "true",
  "ENABLE_PROJECT_MANAGEMENT_AGENT": "false"
}
```

### AI Provider Settings
```json
{
  "AI_PROVIDER": "OpenAI",
  "AI_API_KEY": "your-api-key",
  "AI_MODEL": "gpt-4",
  "AI_ENDPOINT": "https://api.openai.com/v1"
}
```

## Custom AI Agent Development

### 1. Implement IAIAgent Interface
```csharp
public class CustomAgent : IAIAgent
{
    public string AgentName => "CustomAgent";
    public EventPriority MinPriority => EventPriority.Medium;
    public string[] SupportedEventTypes => new[] { "issues", "pull_request" };
    public string[] SupportedActions => new[] { "opened", "closed" };

    public async Task<AIAgentResponse> ProcessEventAsync(KanbanEventResult eventResult)
    {
        // Your custom AI logic here
        return new AIAgentResponse
        {
            Success = true,
            Message = "Custom analysis completed",
            ActionTaken = "custom_analysis",
            Data = customAnalysisResult
        };
    }
}
```

### 2. Register Agent
```csharp
var agents = new List<IAIAgent>
{
    new IssueAnalysisAgent(logger),
    new CodeReviewAgent(logger),
    new ProjectManagementAgent(logger),
    new CustomAgent(logger) // Add your custom agent
};
```

## Monitoring and Debugging

### Logs
The system provides detailed logging for each AI agent:
```
[INFO] Triggered 2 AI agents for pull_request event in 150ms
[INFO] ✅ AI Agent CodeReviewAgent: Code review analysis completed (took 75ms)
[INFO] ✅ AI Agent ProjectManagementAgent: Project management analysis completed (took 50ms)
```

### Metrics
- `aiAgentsTriggered`: Number of agents that processed the event
- `aiAgentsSuccessful`: Number of successful agent responses
- `aiAgentsFailed`: Number of failed agent responses
- `totalAgentProcessingTimeMs`: Total time spent in AI agents
- `totalProcessingTimeMs`: Total time for entire event processing

## Error Handling

If an AI agent fails:
```json
{
  "agentName": "IssueAnalysisAgent",
  "success": false,
  "errorMessage": "AI service temporarily unavailable",
  "processingTimeMs": 0
}
```

The system continues processing with other agents and logs the error for debugging.

## Performance Considerations

- **Concurrent Processing**: AI agents run concurrently when possible
- **Timeout Handling**: Agents have configurable timeouts
- **Caching**: Consider implementing caching for repeated analyses
- **Rate Limiting**: Implement rate limiting for AI service calls
- **Fallback**: Graceful degradation when AI services are unavailable