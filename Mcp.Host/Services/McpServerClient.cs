using Shared.Contracts;

namespace Mcp.Host.Services;

public sealed class McpServerClient
{
    private readonly HttpClient _httpClient;

    public McpServerClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ToolDefinition>> GetToolsAsync(
        string baseUrl)
    {
        return await _httpClient
            .GetFromJsonAsync<List<ToolDefinition>>
            ($"{baseUrl}/mcp/tools")
            ?? [];
    }

    public async Task<ToolCallResponse?> ExecuteAsync(
        string baseUrl,
        ToolCallRequest request)
    {
        var response =
            await _httpClient.PostAsJsonAsync(
                $"{baseUrl}/mcp/execute",
                request);

        return await response.Content
            .ReadFromJsonAsync<ToolCallResponse>();
    }
}