using Common;
using EasyNetQ;
using Messages;

namespace Sender 
{
    static class Program 
    {
        static void Main()
        {
            using var bus = new BusFactory().CreateBus();
            string? input;
            Console.WriteLine($"Enter a message to send on queue '{QueueDefinitions.Queue1}'. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "Quit") 
            {
                bus.SendReceive.Send("my.queue", new TextMessage { Text = input });
                Console.WriteLine("Message sent!");
            }
        }
    }
}