using Common;
using EasyNetQ;
using Messages;

namespace Receiver 
{
    static class Program 
    {
        static void Main()
        {
            using var bus = new BusFactory().CreateBus();
            bus.SendReceive.Receive<TextMessage>(QueueDefinitions.Queue1, message => Console.WriteLine("MyMessage: {0}", message.Text));
            Console.WriteLine($"Listening for messages on queue {QueueDefinitions.Queue1}. Hit <return> to quit.");
            Console.ReadLine();
        }
    }
}