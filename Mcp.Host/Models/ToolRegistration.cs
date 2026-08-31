namespace Mcp.Host.Models;

public sealed class ToolRegistration
{
    public string ToolName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public string ServerName { get; init; } = string.Empty;

    public string BaseUrl { get; init; } = string.Empty;
}