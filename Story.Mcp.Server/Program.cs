using Shared.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/mcp/tools", () =>
{
    return Results.Ok(new[]
    {
        new ToolDefinition(
            "tell_story",
            "Returns a short story")
    });
});

app.MapPost("/mcp/execute",
    (ToolCallRequest request) =>
    {
        var story =
            """
        Once upon a time,
        a developer built a small MCP server.
        """;

        return Results.Ok(
            new ToolCallResponse(
                true,
                story));
    });

app.Run();