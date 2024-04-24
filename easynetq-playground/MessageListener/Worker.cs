namespace MessageListener;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly Listener _listener;

    public Worker(ILogger<Worker> logger, Listener listener)
    {
        _logger = logger;
        _listener = listener;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _listener.Start();
        await base.StartAsync(cancellationToken);
    }
    
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _listener.Stop();
        await base.StopAsync(cancellationToken);
    }
}