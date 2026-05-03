using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AI.DeveloperAssistant.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AI.DeveloperAssistant.Infrastructure.Providers
{
    public class OpenAIProvider : IAIProvider
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIProvider(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAI:ApiKey"];
        }
        public async Task<string> GetResponseAsync(string prompt)
        {
            try
            {
                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                new { role = "user", content = prompt }
            }
                };

                var requestJson = JsonSerializer.Serialize(requestBody);

                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return $"Error: {response.StatusCode}";
                }

                var responseContent = await response.Content.ReadAsStringAsync();

                using var jsonDoc = JsonDocument.Parse(responseContent);

                var result = jsonDoc
                    .RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return result ?? "No response from AI";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}