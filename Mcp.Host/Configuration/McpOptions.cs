namespace Mcp.Host.Configuration;

public sealed class McpOptions
{
    public List<McpServerConfiguration> McpServers { get; set; }
        = [];
}