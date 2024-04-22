using Common;
using EasyNetQ;
using Messages;

namespace Responder 
{
    static class Program 
    {
        static void Main()
        {
            var queue = QueueDefinitions.RequestQueue;
            using var bus = new BusFactory().CreateBus();
            Console.WriteLine($"Waiting for requests on queue '{queue}'. 'Quit' to quit.");
            bus.Rpc.Respond<TestRequestMessage, TestResponseMessage>(request =>
            {
                return new TestResponseMessage() { ResponseText = $"You said: '{request.RequestText}'" };
            }, x => x.WithQueueName(queue));
            while (Console.ReadLine() != "Quit") 
            {
            }
        }
    }
}