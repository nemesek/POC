using System;
using OrderWorkflow.Controllers;
using OrderWorkflow.Domain.WorkflowOrders.Services;

namespace OrderWorkflow
{
    class Program
    {
        static void Main(string[] args)
        {
            // app root - DI Container would go here
            var orderProcessor = new OrderProcessor();
            var random = new Random();
            var cmsId = random.Next(1, 25);
            Console.WriteLine("About to process order for Cms with ID: {0}", cmsId);
            var controller = new OrdersController(orderProcessor);
            var order = controller.ProcessOrder(cmsId);
            order.Save();
        }
    }
}
