using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.DeveloperAssistant.Application.Interfaces;

namespace AI.DeveloperAssistant.Application.Services
{
    public class AIService(IAIProvider _aiProvider)
    {
        public async Task<string> GetAIResponseAsync(string userInput)
        {
            // Later we can enhance prompt here
            return await _aiProvider.GetResponseAsync(userInput);
        }
    }
}
