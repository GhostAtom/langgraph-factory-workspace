using System.Threading.Tasks;
using GitHubKanbanAgent.Models;

namespace GitHubKanbanAgent.AI
{
    /// <summary>
    /// Interface for AI agents that can be triggered by GitHub events
    /// </summary>
    public interface IAIAgent
    {
        /// <summary>
        /// The name of the AI agent
        /// </summary>
        string AgentName { get; }

        /// <summary>
        /// The priority level this agent should handle
        /// </summary>
        EventPriority MinPriority { get; }

        /// <summary>
        /// The event types this agent can handle
        /// </summary>
        string[] SupportedEventTypes { get; }

        /// <summary>
        /// The actions this agent can handle (optional filter)
        /// </summary>
        string[] SupportedActions { get; }

        /// <summary>
        /// Process a GitHub event and return the AI agent response
        /// </summary>
        /// <param name="eventResult">The processed GitHub event</param>
        /// <returns>AI agent response</returns>
        Task<AIAgentResponse> ProcessEventAsync(KanbanEventResult eventResult);
    }

    /// <summary>
    /// Response from an AI agent
    /// </summary>
    public class AIAgentResponse
    {
        public bool Success { get; set; }
        public string AgentName { get; set; }
        public string Message { get; set; }
        public string ActionTaken { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        public long ProcessingTimeMs { get; set; }
    }
}