using System.Collections.Generic;

namespace GitHubKanbanAgent.AI
{
    /// <summary>
    /// Configuration for AI agents
    /// </summary>
    public class AIAgentConfiguration
    {
        public bool EnableIssueAnalysisAgent { get; set; } = true;
        public bool EnableCodeReviewAgent { get; set; } = true;
        public bool EnableProjectManagementAgent { get; set; } = true;
        
        public Dictionary<string, object> AgentSettings { get; set; } = new Dictionary<string, object>();
        
        public int MaxConcurrentAgents { get; set; } = 5;
        public int AgentTimeoutMs { get; set; } = 30000; // 30 seconds
        
        public bool EnableDetailedLogging { get; set; } = true;
        public bool EnablePerformanceMetrics { get; set; } = true;
        
        // AI Service Configuration
        public string AIProvider { get; set; } = "OpenAI"; // OpenAI, Azure OpenAI, Anthropic, etc.
        public string AIApiKey { get; set; }
        public string AIEndpoint { get; set; }
        public string AIModel { get; set; } = "gpt-4";
        
        // Agent-specific settings
        public IssueAnalysisSettings IssueAnalysis { get; set; } = new IssueAnalysisSettings();
        public CodeReviewSettings CodeReview { get; set; } = new CodeReviewSettings();
        public ProjectManagementSettings ProjectManagement { get; set; } = new ProjectManagementSettings();
    }
    
    public class IssueAnalysisSettings
    {
        public bool EnablePriorityAnalysis { get; set; } = true;
        public bool EnableCategoryDetection { get; set; } = true;
        public bool EnableAssigneeSuggestion { get; set; } = true;
        public bool EnableEffortEstimation { get; set; } = true;
        public bool EnableRelatedIssueDetection { get; set; } = true;
        public double ConfidenceThreshold { get; set; } = 0.7;
    }
    
    public class CodeReviewSettings
    {
        public bool EnableRiskAssessment { get; set; } = true;
        public bool EnableReviewerSuggestion { get; set; } = true;
        public bool EnableCodeQualityAnalysis { get; set; } = true;
        public bool EnableSecurityAnalysis { get; set; } = true;
        public bool EnableTestingRecommendations { get; set; } = true;
        public double QualityThreshold { get; set; } = 0.8;
    }
    
    public class ProjectManagementSettings
    {
        public bool EnableWorkflowOptimization { get; set; } = true;
        public bool EnableBottleneckDetection { get; set; } = true;
        public bool EnableResourceAllocationAnalysis { get; set; } = true;
        public bool EnableTimelineImpactAssessment { get; set; } = true;
        public bool EnableRecommendations { get; set; } = true;
        public int AnalysisHistoryDays { get; set; } = 30;
    }
}