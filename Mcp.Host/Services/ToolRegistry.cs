using Mcp.Host.Models;

namespace Mcp.Host.Services;

public sealed class ToolRegistry
{
    private readonly Dictionary<string, ToolRegistration>
        _tools = [];

    public void Register(
        ToolRegistration registration)
    {
        _tools[registration.ToolName] =
            registration;
    }

    public ToolRegistration? Find(
        string toolName)
    {
        return _tools.GetValueOrDefault(toolName);
    }

    public IReadOnlyCollection<ToolRegistration>
        GetAll()
    {
        return _tools.Values;
    }
}