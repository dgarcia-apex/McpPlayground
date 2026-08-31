namespace Shared.Contracts;

public sealed record ToolCallResponse(
    bool Success,
    string Result);