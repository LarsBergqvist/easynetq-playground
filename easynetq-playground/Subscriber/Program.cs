using System;
using Common;
using EasyNetQ;
using Messages;

namespace Subscriber 
{
    static class Program 
    {
        static void Main()
        {
            const string subscriberId = "subscriber1";
            Console.WriteLine("Subscriber ID: {0}", subscriberId);
            using var bus = new BusFactory().CreateBus();
            bus.PubSub.Subscribe<TextMessage>(subscriberId, (message) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Got message: {0}", message.Text);
                Console.ResetColor();
            });
            Console.WriteLine("Listening for messages. Hit <return> to quit.");
            Console.ReadLine();
        }
    }
}