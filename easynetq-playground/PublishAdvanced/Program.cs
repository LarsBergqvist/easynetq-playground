using Common;
using EasyNetQ;
using Messages;

namespace PublishAdvanced
{
    static class Program 
    {
        static void Main()
        {
            using var bus = new BusFactory().CreateBus();
            var advancedBus = bus.Advanced;
            var queue = advancedBus.QueueDeclare(MyExchangeDefinition.QueueName);
            var exchange = advancedBus.ExchangeDeclare(MyExchangeDefinition.ExchangeName, ExchangeType.Direct);
            advancedBus.Bind(exchange, queue, "");
            string? input;
            Console.WriteLine($"Enter a message to Publish with advanced api on queue '{queue.Name}' in exchange '{exchange.Name}'. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "Quit") 
            {
                var myMessage = new TextMessage {Text = input};
                var message = new Message<TextMessage>(myMessage);
                advancedBus.PublishAsync(exchange.Name, "", true, message);
                Console.WriteLine("Message published!");
            }
        }
    }
}