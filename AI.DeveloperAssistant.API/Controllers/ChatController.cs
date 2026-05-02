using AI.DeveloperAssistant.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AI.DeveloperAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ChatController(AIService _aiService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Chat([FromBody] string input)
        {
            var response = await _aiService.GetAIResponseAsync(input);
            return Ok(response);
        }
    }
}
