using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AI.DeveloperAssistant.Application.Interfaces;

namespace AI.DeveloperAssistant.Infrastructure.Providers
{
    public class OpenAIProvider(HttpClient _httpClient) : IAIProvider
    {
        public async Task<string> GetResponseAsync(string prompt)
        {
            var requestBody = new
            {
                model = "gpt-4o-mini",
                messgaes = new[]
                {
                    new
                    {
                        role = "user",
                        content = prompt
                    }
                }
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "YOUR_API_KEY");
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            //return responseContent;
            using var jsonDoc = JsonDocument.Parse(responseContent);
            var result = jsonDoc
        .RootElement
        .GetProperty("choices")[0]
        .GetProperty("message")
        .GetProperty("content")
        .GetString();
            return result ?? "No response";
        }
    }
}
