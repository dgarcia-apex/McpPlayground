using Mcp.Host.Configuration;
using Mcp.Host.Models;
using Microsoft.Extensions.Options;

namespace Mcp.Host.Services;

public sealed class ServerDiscoveryService
{
    private readonly ToolRegistry _registry;
    private readonly McpServerClient _client;
    private readonly ILogger<ServerDiscoveryService> _logger;
    private readonly IConfiguration _configuration;

    public ServerDiscoveryService(
        ToolRegistry registry,
        McpServerClient client,
        ILogger<ServerDiscoveryService> logger,
        IConfiguration configuration)
    {
        _registry = registry;
        _client = client;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task DiscoverAsync()
    {
        var servers = _configuration
            .GetSection("McpServers")
            .Get<List<McpServerConfiguration>>();

        if (servers is null || servers.Count == 0)
        {
            _logger.LogWarning(
                "No MCP servers found in configuration.");

            return;
        }

        foreach (var server in servers)
        {
            try
            {
                _logger.LogInformation(
                    "Discovering tools from {ServerName} ({BaseUrl})",
                    server.Name,
                    server.BaseUrl);

                var tools =
                    await _client.GetToolsAsync(
                        server.BaseUrl);

                foreach (var tool in tools)
                {
                    _registry.Register(
                        new ToolRegistration
                        {
                            ToolName = tool.Name,
                            Description = tool.Description,
                            ServerName = server.Name,
                            BaseUrl = server.BaseUrl
                        });

                    _logger.LogInformation(
                        "Registered tool {ToolName} from {ServerName}",
                        tool.Name,
                        server.Name);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to discover tools from {ServerName}",
                    server.Name);
            }
        }

        _logger.LogInformation(
            "Discovery completed. Registered tools: {Count}",
            _registry.GetAll().Count);
    }
}