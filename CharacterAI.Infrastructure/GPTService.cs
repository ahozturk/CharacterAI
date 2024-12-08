using System;
using System.Text;
using System.Text.Json;
using CharacterAI.Domain.Models;

namespace CharacterAI.Infrastructure
{
    public static class GPTService
    {
        private static HttpClient client;
        public static async Task<string> AskToChatGPT(ChatMessage[] messages)
        {
            client = new HttpClient();

            var apiKey = "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";
            var requestUrl = "https://api.openai.com/v1/chat/completions";

            var requestBody = new
            {
                model = "gpt-4o",
                temperature = 1.0,
                messages = messages
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var response = await client.PostAsync(requestUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(responseContent);

            string resultContent = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return resultContent;
        }
    }
}
