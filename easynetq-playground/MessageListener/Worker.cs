using Common;
using Microsoft.Extensions.Options;

namespace MessageListener;

public class Worker : BackgroundService
{
//    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _services;
    private ListenerCollection _listenerCollection;

    public Worker(IServiceProvider services)
    {
//        _logger = logger;
        _services = services;
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
        var logger = _services.GetRequiredService<ILogger<Listener>>();
        var settings = _services.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
        _listenerCollection = new ListenerCollection(new List<IListener> 
            { new Listener(logger, settings), new Listener(logger, settings) });

        foreach (var listener in _listenerCollection.Listeners)
        {
            listener.Start();
        }


        await base.StartAsync(cancellationToken);
    }
    
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var listener in _listenerCollection.Listeners)
        {
            listener.Start();
        }
        await base.StopAsync(cancellationToken);
    }
}