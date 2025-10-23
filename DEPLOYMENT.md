# Deployment Guide

This guide covers deploying the GitHub Kanban Events Agent to Azure.

## Prerequisites

- Azure CLI installed and configured
- Azure Functions Core Tools v4
- .NET 6.0 SDK
- GitHub repository with admin access

## Step 1: Create Azure Resources

### 1.1 Create Resource Group

```bash
az group create --name GitHubKanbanAgent-RG --location "East US"
```

### 1.2 Create Storage Account

```bash
az storage account create \
  --name githubkanbanagentstorage \
  --resource-group GitHubKanbanAgent-RG \
  --location "East US" \
  --sku Standard_LRS
```

### 1.3 Create Function App

```bash
az functionapp create \
  --resource-group GitHubKanbanAgent-RG \
  --consumption-plan-location "East US" \
  --runtime dotnet \
  --runtime-version 6.0 \
  --functions-version 4 \
  --name github-kanban-agent \
  --storage-account githubkanbanagentstorage
```

## Step 2: Configure Application Settings

### 2.1 Set GitHub Webhook Secret

```bash
az functionapp config appsettings set \
  --name github-kanban-agent \
  --resource-group GitHubKanbanAgent-RG \
  --settings GITHUB_WEBHOOK_SECRET="your_github_webhook_secret_here"
```

### 2.2 Set Storage Connection String

```bash
az functionapp config appsettings set \
  --name github-kanban-agent \
  --resource-group GitHubKanbanAgent-RG \
  --settings AzureWebJobsStorage="DefaultEndpointsProtocol=https;AccountName=githubkanbanagentstorage;AccountKey=<your_storage_key>;EndpointSuffix=core.windows.net"
```

## Step 3: Deploy the Function

### 3.1 Build and Publish

```bash
# Build the project
dotnet build --configuration Release

# Publish to Azure
func azure functionapp publish github-kanban-agent
```

### 3.2 Verify Deployment

```bash
# Check function status
az functionapp show --name github-kanban-agent --resource-group GitHubKanbanAgent-RG
```

## Step 4: Configure GitHub Webhook

### 4.1 Get Function URL

```bash
# Get the webhook URL
echo "https://github-kanban-agent.azurewebsites.net/api/github/webhook"
```

### 4.2 Configure GitHub Webhook

1. Go to your GitHub repository
2. Navigate to Settings → Webhooks
3. Click "Add webhook"
4. Set Payload URL: `https://github-kanban-agent.azurewebsites.net/api/github/webhook`
5. Set Content type: `application/json`
6. Set Secret: (same as GITHUB_WEBHOOK_SECRET)
7. Select events:
   - Issues
   - Pull requests
   - Projects
   - Project cards
   - Project columns
8. Click "Add webhook"

## Step 5: Test the Deployment

### 5.1 Test Webhook

1. Create a test issue in your repository
2. Check the function logs:

```bash
az functionapp logs tail --name github-kanban-agent --resource-group GitHubKanbanAgent-RG
```

### 5.2 Verify Event Processing

Look for log entries like:
```
[INFO] GitHub webhook received
[INFO] Successfully processed issues event: New issue opened: Test Issue
```

## Step 6: Monitoring Setup

### 6.1 Enable Application Insights

```bash
az monitor app-insights component create \
  --app github-kanban-agent-insights \
  --location "East US" \
  --resource-group GitHubKanbanAgent-RG

az functionapp config appsettings set \
  --name github-kanban-agent \
  --resource-group GitHubKanbanAgent-RG \
  --settings APPINSIGHTS_INSTRUMENTATIONKEY="<your_instrumentation_key>"
```

### 6.2 Set up Alerts

1. Go to Azure Portal → Application Insights
2. Navigate to Alerts
3. Create alerts for:
   - Function execution failures
   - High error rates
   - Performance issues

## Step 7: CI/CD Pipeline (Optional)

### 7.1 GitHub Actions

Create `.github/workflows/deploy.yml`:

```yaml
name: Deploy to Azure

on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: '6.0.x'
    
    - name: Build
      run: dotnet build --configuration Release
    
    - name: Deploy to Azure Functions
      uses: Azure/functions-action@v1
      with:
        app-name: 'github-kanban-agent'
        package: '.'
        publish-profile: ${{ secrets.AZURE_FUNCTIONAPP_PUBLISH_PROFILE }}
```

### 7.2 Azure DevOps Pipeline

Create `azure-pipelines.yml`:

```yaml
trigger:
- main

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'

steps:
- task: DotNetCoreCLI@2
  displayName: 'Restore packages'
  inputs:
    command: 'restore'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  displayName: 'Build project'
  inputs:
    command: 'build'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration)'

- task: DotNetCoreCLI@2
  displayName: 'Run tests'
  inputs:
    command: 'test'
    projects: '**/*.csproj'
    arguments: '--configuration $(buildConfiguration) --collect:"XPlat Code Coverage"'

- task: AzureFunctionApp@1
  displayName: 'Deploy to Azure Functions'
  inputs:
    azureSubscription: 'Azure Service Connection'
    appType: 'functionApp'
    appName: 'github-kanban-agent'
    package: '$(System.DefaultWorkingDirectory)/**/*.zip'
```

## Troubleshooting

### Common Issues

1. **Function not starting**
   - Check application settings
   - Verify storage account connection
   - Check function logs

2. **Webhook not receiving events**
   - Verify webhook URL is correct
   - Check GitHub webhook configuration
   - Test with GitHub's webhook test feature

3. **Signature verification failing**
   - Ensure GITHUB_WEBHOOK_SECRET matches GitHub webhook secret
   - Check webhook content type is application/json

4. **Performance issues**
   - Monitor function execution metrics
   - Consider upgrading to Premium plan for better performance
   - Check for memory leaks in event processing

### Useful Commands

```bash
# View function logs
az functionapp logs tail --name github-kanban-agent --resource-group GitHubKanbanAgent-RG

# Restart function app
az functionapp restart --name github-kanban-agent --resource-group GitHubKanbanAgent-RG

# Update application settings
az functionapp config appsettings set --name github-kanban-agent --resource-group GitHubKanbanAgent-RG --settings KEY=VALUE

# Delete function app (if needed)
az functionapp delete --name github-kanban-agent --resource-group GitHubKanbanAgent-RG
```

## Security Considerations

1. **Webhook Secret**: Use a strong, unique secret for webhook verification
2. **HTTPS Only**: Ensure all communications use HTTPS
3. **Access Control**: Limit function app access to necessary resources
4. **Monitoring**: Set up alerts for suspicious activity
5. **Regular Updates**: Keep dependencies updated for security patches

## Cost Optimization

1. **Consumption Plan**: Use consumption plan for cost-effective scaling
2. **Monitoring**: Set up cost alerts to monitor usage
3. **Log Retention**: Configure appropriate log retention periods
4. **Resource Cleanup**: Remove unused resources regularly