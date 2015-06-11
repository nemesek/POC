using System;
using System.Threading;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.Events;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Controllers
{
    public class OrdersController
    {
        private readonly CmsFactory _factory = new CmsFactory();

        public IWorkflowOrder ProcessOrder(int cmsId, bool isDemo)
        {
            var isOpen = true;
            DomainEvents.SubscribeTo<OrderClosedEvent>(_ => isOpen = false);
            var cms = _factory.GetCms(cmsId, OrderContext.Workflow);
            var order = cms.GetWorkflowOrder();
            var delay = isDemo ? (() => Console.ReadLine()) : new Action (() => Thread.Sleep(1000));
            while (isOpen)
            {
                var output = $"+++Incoming Request about to process order with status {order.Status}.+++++";
                ConsoleHelper.WriteWithStyle(ConsoleColor.DarkCyan, ConsoleColor.White, output);
                order = order.ProcessNextStep();
                delay();
            }

            return order;
        }

        public void EditOrderAddress(int cmsId)
        {
            var cms = _factory.GetCms(cmsId, OrderContext.Edit);
            var newAddress = new Address("Dallas", "TX", "75019", "Elm", "456");
            cms.EditOrderAddress(newAddress);
        }

        public void CreateOrder(int cmsId)
        {
            var cms = _factory.GetCms(cmsId, OrderContext.Creation);
            var order = cms.CreateOrder();
            Console.WriteLine("Mapping order {0} to DTO", order.Id);
        }

    }
}
