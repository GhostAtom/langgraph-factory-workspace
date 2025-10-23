using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GitHubKanbanAgent.Models;

namespace GitHubKanbanAgent.AI
{
    /// <summary>
    /// Routes GitHub events to appropriate AI agents
    /// </summary>
    public class AIAgentRouter
    {
        private readonly List<IAIAgent> _agents;
        private readonly ILogger _logger;

        public AIAgentRouter(IEnumerable<IAIAgent> agents, ILogger logger)
        {
            _agents = agents?.ToList() ?? new List<IAIAgent>();
            _logger = logger;
        }

        /// <summary>
        /// Route a GitHub event to the appropriate AI agents
        /// </summary>
        /// <param name="eventResult">The processed GitHub event</param>
        /// <returns>List of AI agent responses</returns>
        public async Task<List<AIAgentResponse>> RouteEventAsync(KanbanEventResult eventResult)
        {
            var responses = new List<AIAgentResponse>();
            var eligibleAgents = GetEligibleAgents(eventResult);

            _logger.LogInformation($"Routing {eventResult.EventType} event to {eligibleAgents.Count} eligible agents");

            foreach (var agent in eligibleAgents)
            {
                try
                {
                    var startTime = DateTime.UtcNow;
                    var response = await agent.ProcessEventAsync(eventResult);
                    response.ProcessingTimeMs = (long)(DateTime.UtcNow - startTime).TotalMilliseconds;
                    response.AgentName = agent.AgentName;
                    
                    responses.Add(response);
                    
                    _logger.LogInformation($"Agent {agent.AgentName} processed event: {response.Success} - {response.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error processing event with agent {agent.AgentName}");
                    
                    responses.Add(new AIAgentResponse
                    {
                        Success = false,
                        AgentName = agent.AgentName,
                        ErrorMessage = ex.Message,
                        ProcessingTimeMs = 0
                    });
                }
            }

            return responses;
        }

        /// <summary>
        /// Get agents that are eligible to handle the given event
        /// </summary>
        private List<IAIAgent> GetEligibleAgents(KanbanEventResult eventResult)
        {
            return _agents.Where(agent => 
                agent.SupportedEventTypes.Contains(eventResult.EventType) &&
                (int)agent.MinPriority <= (int)eventResult.Priority &&
                (agent.SupportedActions == null || 
                 agent.SupportedActions.Length == 0 || 
                 agent.SupportedActions.Contains(eventResult.Action))
            ).ToList();
        }

        /// <summary>
        /// Get all registered agents
        /// </summary>
        public List<IAIAgent> GetAllAgents()
        {
            return _agents.ToList();
        }

        /// <summary>
        /// Get agents by name
        /// </summary>
        public List<IAIAgent> GetAgentsByName(string agentName)
        {
            return _agents.Where(a => a.AgentName.Equals(agentName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}