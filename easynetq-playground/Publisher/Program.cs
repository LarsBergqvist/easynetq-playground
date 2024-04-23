using Common;
using EasyNetQ;
using Messages;

namespace Publisher 
{
    static class Program 
    {
        static void Main()
        {
            using var bus = new BusFactory().CreateBus();
            string? input;
            Console.WriteLine("Enter a message to Publish. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "Quit") 
            {
                bus.PubSub.Publish(new TextMessage { Text = input });
                Console.WriteLine("Message published!");
            }
        }
    }
}