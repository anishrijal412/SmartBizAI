using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SmartBizAI.Shared.DTOs.Ai;

namespace SmartBizAI.Api.Services;

public interface IAiService
{
    Task<AiChatResponseDto> ChatAsync(string userMessage, string userRole, string dbSummary, CancellationToken ct);
}

public sealed class AiService : IAiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<AiChatResponseDto> ChatAsync(string userMessage, string userRole, string dbSummary, CancellationToken ct)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return new AiChatResponseDto
            {
                Response = "OpenAI API key is missing. Set OpenAI:ApiKey in configuration."
            };
        }

        using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var payload = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = "You are SmartBizAI, a business management assistant. Provide concise, practical guidance." },
                new { role = "system", content = $"UserRole: {userRole}. DatabaseSummary: {dbSummary}" },
                new { role = "user", content = userMessage }
            },
            temperature = 0.2
        };

        request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        using var response = await _httpClient.SendAsync(request, ct);
        var json = await response.Content.ReadAsStringAsync(ct);
        if (!response.IsSuccessStatusCode)
        {
            return new AiChatResponseDto
            {
                Response = $"OpenAI request failed: {(int)response.StatusCode} {response.ReasonPhrase}."
            };
        }

        using var doc = JsonDocument.Parse(json);
        var content = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        return new AiChatResponseDto
        {
            Response = content ?? string.Empty
        };
    }
}
