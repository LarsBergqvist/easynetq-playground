using EasyNetQ;

namespace Common;

public class BusFactory
{
    public SelfHostedBus CreateBus()
    {
        return RabbitHutch.CreateBus("host=localhost");
    }
    
    // CreateBus from with a generated connection using RabbitMqSettings    
    public SelfHostedBus CreateBus(RabbitMqSettings settings)
    {
        return RabbitHutch.CreateBus($"host={settings.Host};username={settings.UserName};password={settings.Password}");
    }

}