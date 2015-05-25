using System;
using System.Threading;
using OrderWorkflow.Domain;
using OrderWorkflow.Domain.Common;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Events;

namespace OrderWorkflow.Controllers
{
    public class OrdersController
    {
        public IWorkflowOrder ProcessOrder(int cmsId)
        {
            var isOpen = true;
            DomainEvents.Register<OrderClosedEvent>(_ => isOpen = false);
            var cms = new Cms(cmsId);
            var order = cms.GetWorkflowOrder();
            while (isOpen)
            {
                Thread.Sleep(500);
                const string output = "++++++++++++++Incoming Request about to be processed.+++++++++++++";
                ConsoleHelper.WriteWithStyle(ConsoleColor.DarkCyan, ConsoleColor.White, output);
                order = order.ProcessNextStep();
                Thread.Sleep(500);
            }

            return order;
        }
        
        public void EditOrderAddress(int cmsId)
        {
            var cms = new Cms(cmsId);
            var newAddress = new Address("Dallas", "TX", "75019", "Elm", "456");
            cms.EditOrderAddress(newAddress);
        }
        
        public void CreateOrder(int cmsId)
        {
            var cms = new Cms(cmsId);
            var order = cms.CreateOrder();
            Console.WriteLine("Mapping order {0} to DTO", order.Id);
        }

    }
}
