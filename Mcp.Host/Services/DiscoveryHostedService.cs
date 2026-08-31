namespace Mcp.Host.Services;

public sealed class DiscoveryHostedService
    : IHostedService
{
    private readonly ServerDiscoveryService _discovery;

    public DiscoveryHostedService(
        ServerDiscoveryService discovery)
    {
        _discovery = discovery;
    }

    public async Task StartAsync(
        CancellationToken cancellationToken)
    {
        await _discovery.DiscoverAsync();
    }

    public Task StopAsync(
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}