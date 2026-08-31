namespace Shared.Contracts;

public sealed record ToolCallRequest(
    string ToolName,
    Dictionary<string, string> Parameters);