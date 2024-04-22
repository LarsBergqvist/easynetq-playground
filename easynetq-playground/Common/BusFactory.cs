using EasyNetQ;

namespace Common;

public class BusFactory
{
    public SelfHostedBus CreateBus()
    {
        return RabbitHutch.CreateBus("host=localhost");
    }
}