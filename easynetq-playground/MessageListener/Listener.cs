using System.Text;
using Common;
using EasyNetQ;
using Microsoft.Extensions.Options;

namespace MessageListener;

public class Listener(ILogger<Listener> logger, RabbitMqSettings settings) : IListener
{
    private SelfHostedBus? _bus;

    public void Start()
    {
        _bus = new BusFactory().CreateBus(settings);
        var advancedBus = _bus.Advanced;
        var queue = advancedBus.QueueDeclare(settings.QueueName);
        logger.LogInformation("Consumer listening on queue '{QUEUE}", queue.Name);
        advancedBus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
        {
            var message = Encoding.UTF8.GetString(body.Span);
            logger.LogInformation("Got message: '{MESSAGE}'", message);
            logger.LogInformation("Info: '{INFO}'", info.ToString());
            logger.LogInformation("Properties: '{PROPS}'", properties.ToString());
        }));

        logger.LogInformation("Listener started");
    }
    
    public void Stop()
    {
        if (_bus != null)
        {
            _bus.Dispose();
        }
        logger.LogInformation("Listener stopped");
    }
}