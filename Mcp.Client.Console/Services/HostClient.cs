using System.Net.Http.Json;
using Shared.Contracts;

namespace Mcp.Client.Console.Services;

public sealed class HostClient
{
    private readonly HttpClient _httpClient;

    public HostClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> GetCapabilitiesAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<List<string>>(
                "/capabilities")
            ?? [];
    }

    public async Task<ToolCallResponse?> ExecuteAsync(
        ToolCallRequest request)
    {
        var response =
            await _httpClient.PostAsJsonAsync(
                "/execute",
                request);

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<ToolCallResponse>();
    }
}