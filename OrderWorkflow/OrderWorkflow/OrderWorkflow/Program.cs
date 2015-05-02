using System;
using OrderWorkflow.Controllers;
using OrderWorkflow.Domain;

namespace OrderWorkflow
{
    class Program
    {
        static void Main(string[] args)
        {
            // app root - DI Continaer would go here
            var orderProcessor = new OrderProcessor();
            var random = new Random();
            var clientId = random.Next(1, 7);
            Console.WriteLine("About to process order for Client {0}", clientId);
            var controller = new OrdersController(orderProcessor);
            var order = controller.ProcessOrder(clientId);
        }
    }
}
