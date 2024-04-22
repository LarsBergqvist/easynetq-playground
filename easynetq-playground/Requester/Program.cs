using Common;
using EasyNetQ;
using Messages;

namespace Requester 
{
    static class Program 
    {
        static void Main()
        {
            using var bus = new BusFactory().CreateBus();
            string? input;
            Console.WriteLine($"Enter a message to send as a request on queue '{QueueDefinitions.RequestQueue}'. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "Quit") 
            {
                var request = new TestRequestMessage { RequestText = input };
                var task = bus.Rpc.RequestAsync<TestRequestMessage, TestResponseMessage>(request, 
                    x => x.WithQueueName(QueueDefinitions.RequestQueue));
                Console.WriteLine("Request sent!");
                task.ContinueWith(response => {
                    Console.WriteLine("Got response: '{0}'", response.Result.ResponseText);
                });
            }
        }
    }
}