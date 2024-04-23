using System.Text;
using Common;
using EasyNetQ;

namespace Consumer 
{
    static class Program 
    {
        static void Main()
        {
            using var bus = new BusFactory().CreateBus();
            var advancedBus = bus.Advanced;
            var queue = advancedBus.QueueDeclare(MyExchangeDefinition.QueueName);
            Console.WriteLine($"Consumer listening on queue '{queue.Name}'");
            advancedBus.Consume(queue, (body, properties, info) => Task.Factory.StartNew(() =>
            {
                var message = Encoding.UTF8.GetString(body.Span);
                Console.WriteLine("Got message: '{0}'", message);
                Console.WriteLine("Info: '{0}'", info.ToString());
                Console.WriteLine("Properties: '{0}'", properties.ToString());
            }));
            Console.WriteLine("Listening for messages. Hit <return> to quit.");
            Console.ReadLine();
        }
    }
}