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
            "bible_study",
            "Generates bible study topics")
    });
});

app.MapPost("/mcp/execute",
    (ToolCallRequest request) =>
    {
        var topic =
            request.Parameters.GetValueOrDefault(
                "topic",
                "faith");

        return Results.Ok(
            new ToolCallResponse(
                true,
                $"Bible study generated for: {topic}")
        );
    });

app.Run();
