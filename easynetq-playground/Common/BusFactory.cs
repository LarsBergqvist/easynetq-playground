using EasyNetQ;

namespace Common;

public class BusFactory
{
    public SelfHostedBus CreateBus()
    {
        return RabbitHutch.CreateBus("host=localhost");
    }
    
    public IAdvancedBus CreateAdvancedBus()
    {
        return RabbitHutch.CreateBus("host=localhost").Advanced;
    }

}