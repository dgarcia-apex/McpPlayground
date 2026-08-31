using Mcp.Host.Configuration;
using Mcp.Host.Services;
using Shared.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<McpOptions>(
    builder.Configuration);

builder.Services.AddSingleton<ToolRegistry>();

builder.Services.AddHttpClient<McpServerClient>();

builder.Services.AddSingleton<ServerDiscoveryService>();

builder.Services.AddHostedService<DiscoveryHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/debug/tools",
(
    ToolRegistry registry) =>
{
    return registry.GetAll();
});

app.MapGet("/capabilities",
(
    ToolRegistry registry) =>
{
    return registry
        .GetAll()
        .Select(t => t.ToolName);
});

app.MapPost("/execute",
async (
    ToolCallRequest request,
    ToolRegistry registry,
    McpServerClient client) =>
{
    var tool =
        registry.Find(request.ToolName);

    if (tool is null)
    {
        return Results.NotFound(
            $"Tool {request.ToolName} not found");
    }

    var response =
        await client.ExecuteAsync(
            tool.BaseUrl,
            request);

    return Results.Ok(response);
});

app.Run();

