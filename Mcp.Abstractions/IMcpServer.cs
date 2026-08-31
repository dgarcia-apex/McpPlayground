using Shared.Contracts;

namespace Mcp.Abstractions;

public interface IMcpServer
{
    Task<IEnumerable<ToolDefinition>> GetToolsAsync();

    Task<ToolCallResponse> ExecuteAsync(
        ToolCallRequest request);
}