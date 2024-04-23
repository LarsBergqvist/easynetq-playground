using Common;
using EasyNetQ;
using EasyNetQ.Topology;
using Messages;

namespace PublishAdvanced
{
    static class Program 
    {
        static void Main()
        {
            using var bus = new BusFactory().CreateBus();
            var advancedBus = bus.Advanced;
            var queue = advancedBus.QueueDeclare("my.queue");
            var exchange = advancedBus.ExchangeDeclare("my.exchange", ExchangeType.Topic);
            var binding = advancedBus.Bind(exchange, queue, "A.*");
            string? input;
            Console.WriteLine("Enter a message to Publish. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "Quit") 
            {
                var myMessage = new TextMessage {Text = "Hello from the publisher"};
                var message = new Message<TextMessage>(myMessage);
                advancedBus.PublishAsync("my-exchange", "A.in", true, message);
                Console.WriteLine("Message published!");
            }
        }
    }
}