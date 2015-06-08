using System;
using System.Threading;
using DnxConsole.Domain;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.Events;
using DnxConsole.Infrastructure.Utilities;

namespace DnxConsole.Controllers
{
    public class OrdersController
    {
        private readonly ILogEvents _logger;
        private readonly ISendExternalMessenges _messenger;

        public OrdersController(ILogEvents logger, ISendExternalMessenges messenger)
        {
            _logger = logger;
            _messenger = messenger;
        }
        public IWorkflowOrder ProcessOrder(int cmsId, bool isDemo)
        {
            var isOpen = true;
            DomainEvents.Subscribe<OrderClosedEvent>(_ => isOpen = false);
            var cms = new Cms(cmsId, _logger,_messenger);
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
            var cms = new Cms(cmsId,_logger,_messenger);
            var newAddress = new Address("Dallas", "TX", "75019", "Elm", "456");
            cms.EditOrderAddress(newAddress);
        }

        public void CreateOrder(int cmsId)
        {
            var cms = new Cms(cmsId, _logger,_messenger);
            var order = cms.CreateOrder();
            Console.WriteLine("Mapping order {0} to DTO", order.Id);
        }

    }
}
