using Shared.Contracts;

var builder = WebApplication.CreateBuilder(args);
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
            "tell_joke",
            "Returns funny jokes")
    });
});

app.MapPost("/mcp/execute",
async (ToolCallRequest request) =>
{
    var joke =
        "Why do developers hate nature? Too many bugs.";

    return Results.Ok(
        new ToolCallResponse(true, joke));
});

app.Run();

