# GitHub Kanban Events Agent

This project is an Azure Functions-based agent that listens to GitHub Kanban events and processes them for workflow automation, notifications, and integrations.

## Features

- **GitHub Webhook Integration**: Listens to GitHub webhook events for issues, pull requests, and project management
- **AI Agent Integration**: Automatically triggers appropriate AI agents based on event types and content
- **Kanban Event Processing**: Processes project cards, columns, and project events
- **Event Prioritization**: Categorizes events by priority (High, Medium, Low)
- **Intelligent Routing**: Routes events to specialized AI agents for analysis and insights
- **Comprehensive Logging**: Detailed logging for monitoring and debugging
- **Security**: Webhook signature verification for secure event processing
- **Extensible Architecture**: Easy to extend with additional event processors and AI agents

## Supported Events

### Issues
- `opened` - New issue created
- `closed` - Issue closed
- `labeled` - Issue labeled
- `assigned` - Issue assigned to user
- `milestoned` - Issue added to milestone

### Pull Requests
- `opened` - New pull request created
- `closed` - Pull request closed (merged or not)
- `review_requested` - Review requested
- `synchronize` - Pull request updated

### Project Cards
- `created` - New project card created
- `moved` - Card moved between columns
- `deleted` - Card deleted
- `converted` - Card converted

### Projects
- `created` - New project created
- `updated` - Project updated
- `closed` - Project closed
- `reopened` - Project reopened

### Project Columns
- `created` - New column created
- `updated` - Column updated
- `moved` - Column moved
- `deleted` - Column deleted

## Prerequisites

- Azure Functions Core Tools v4
- .NET 6.0 SDK
- Azure Subscription
- GitHub repository with webhook access

## Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd GitHubKanbanAgent
   ```

2. **Install dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure local settings**
   - Copy `local.settings.json.example` to `local.settings.json`
   - Update the configuration values:
     ```json
     {
       "IsEncrypted": false,
       "Values": {
         "AzureWebJobsStorage": "UseDevelopmentStorage=true",
         "FUNCTIONS_WORKER_RUNTIME": "dotnet",
         "GITHUB_WEBHOOK_SECRET": "your_github_webhook_secret"
       }
     }
     ```

4. **Run locally**
   ```bash
   func start
   ```

## GitHub Webhook Configuration

1. **Create a webhook in your GitHub repository**
   - Go to Settings → Webhooks → Add webhook
   - Set Payload URL to: `https://your-function-app.azurewebsites.net/api/github/webhook`
   - Set Content type to: `application/json`
   - Select events: Issues, Pull requests, Projects, Project cards, Project columns
   - Set Secret to match your `GITHUB_WEBHOOK_SECRET`

2. **Test the webhook**
   - Use GitHub's "Send test payload" feature
   - Check the function logs for successful processing

## API Endpoints

### GitHub Webhook
- **POST** `/api/github/webhook` - Receives GitHub webhook events (basic processing)
- **POST** `/api/github/webhook/enhanced` - Receives GitHub webhook events with AI agent integration

## AI Agents

The system includes three specialized AI agents that are automatically triggered based on event types:

### IssueAnalysisAgent
- **Triggers on**: Issue events (opened, labeled, assigned)
- **Provides**: Priority analysis, category detection, assignee suggestions, effort estimation
- **Example**: Analyzes a new bug report and suggests it's high priority, needs security team review

### CodeReviewAgent  
- **Triggers on**: Pull request events (opened, updated, review requested)
- **Provides**: Risk assessment, reviewer suggestions, code quality analysis, security concerns
- **Example**: Analyzes a security-related PR and recommends security expert review

### ProjectManagementAgent
- **Triggers on**: Project card, project, and column events
- **Provides**: Workflow optimization, bottleneck detection, resource allocation analysis
- **Example**: Detects when cards are moving slowly and suggests workflow improvements

## Configuration

### Environment Variables

| Variable | Description | Required |
|----------|-------------|----------|
| `GITHUB_WEBHOOK_SECRET` | Secret for webhook signature verification | Yes |
| `AzureWebJobsStorage` | Azure Storage connection string | Yes |
| `FUNCTIONS_WORKER_RUNTIME` | Function runtime (dotnet) | Yes |
| `AI_PROVIDER` | AI provider (OpenAI, Azure OpenAI, etc.) | No |
| `AI_API_KEY` | API key for AI services | No |
| `AI_MODEL` | AI model to use (e.g., gpt-4) | No |
| `ENABLE_ISSUE_ANALYSIS_AGENT` | Enable issue analysis AI agent | No |
| `ENABLE_CODE_REVIEW_AGENT` | Enable code review AI agent | No |
| `ENABLE_PROJECT_MANAGEMENT_AGENT` | Enable project management AI agent | No |

### Event Processing

The agent processes events with different priorities:

- **High Priority**: New issues, merged PRs, project creation/deletion, card movements
- **Medium Priority**: Issue assignments, PR reviews, project updates, card creation
- **Low Priority**: Issue labeling, PR updates, column updates

## Development

### Running Tests

```bash
dotnet test
```

### Building

```bash
dotnet build
```

### Publishing

```bash
func azure functionapp publish <function-app-name>
```

## Architecture

```
GitHub Webhook → Azure Function → Event Processor → Action Handlers
                     ↓
                Logging & Monitoring
```

### Components

1. **GitHubWebhookFunction**: Main webhook endpoint with signature verification
2. **KanbanEventProcessor**: Processes different event types and extracts metadata
3. **GitHubModels**: Data models for GitHub API responses
4. **Event Handlers**: Extensible handlers for different event types

## Extending the Agent

### Adding New Event Types

1. Add the event type to `ProcessGitHubEvent` in `GitHubWebhookFunction.cs`
2. Create a processing method in `KanbanEventProcessor.cs`
3. Add corresponding models in `GitHubModels.cs`
4. Write tests in `GitHubWebhookTests.cs`

### Adding Custom Actions

1. Create a new service class implementing your custom logic
2. Inject it into the event processor
3. Call it from the appropriate event processing methods

## Security

- **Webhook Signature Verification**: All webhooks are verified using HMAC-SHA256
- **HTTPS Only**: All communications use HTTPS
- **Environment Variables**: Sensitive data stored in environment variables

## Monitoring

- **Application Insights**: Integrated for comprehensive monitoring
- **Structured Logging**: JSON-formatted logs for easy parsing
- **Event Tracking**: All processed events are logged with metadata

## Troubleshooting

### Common Issues

1. **Webhook signature verification fails**
   - Check that `GITHUB_WEBHOOK_SECRET` matches GitHub webhook secret
   - Verify webhook URL is correct

2. **Events not being processed**
   - Check function logs for errors
   - Verify webhook is configured for correct events
   - Ensure function is running and accessible

3. **Performance issues**
   - Monitor function execution time
   - Check for memory leaks in event processing
   - Consider scaling function app

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.