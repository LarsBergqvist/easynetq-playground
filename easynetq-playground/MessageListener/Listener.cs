using System.Text;
using Common;
using EasyNetQ;
using Microsoft.Extensions.Options;

namespace MessageListener;

public class Listener
{
    private readonly ILogger<Listener> _logger;
    private readonly RabbitMqSettings _settings;
    private SelfHostedBus? _bus;
    public Listener(ILogger<Listener> logger, IOptions<RabbitMqSettings> options)
    {
        _logger = logger;
        _settings = options.Value;
    }
    
    public void Start()
    {
        _bus = new BusFactory().CreateBus(_settings);
        var advancedBus = _bus.Advanced;
        var queue = advancedBus.QueueDeclare(_settings.QueueName);
        _logger.LogInformation("Consumer listening on queue '{QUEUE}", queue.Name);
        advancedBus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
        {
            var message = Encoding.UTF8.GetString(body.Span);
            _logger.LogInformation("Got message: '{MESSAGE}'", message);
            _logger.LogInformation("Info: '{INFO}'", info.ToString());
            _logger.LogInformation("Properties: '{PROPS}'", properties.ToString());
        }));

        _logger.LogInformation("Listener started");
    }
    
    public void Stop()
    {
        if (_bus != null)
        {
            _bus.Dispose();
        }
        _logger.LogInformation("Listener stopped");
    }
}