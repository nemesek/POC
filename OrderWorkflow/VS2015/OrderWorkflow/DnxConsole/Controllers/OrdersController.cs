﻿using System;
using System.Threading;
using DnxConsole.Domain;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.Events;
using DnxConsole.Utilities;

namespace DnxConsole.Controllers
{
    public class OrdersController
    {
        public IWorkflowOrder ProcessOrder(int cmsId, bool isDemo)
        {
            var isOpen = true;
            DomainEvents.Register<OrderClosedEvent>(_ => isOpen = false);
            var cms = new Cms(cmsId);
            var order = cms.GetWorkflowOrder();
            var delay = isDemo ? (() => Console.ReadLine()) : new Action (() => Thread.Sleep(1000));
            while (isOpen)
            {
                const string output = "++++++++++++++Incoming Request about to be processed.+++++++++++++";
                ConsoleHelper.WriteWithStyle(ConsoleColor.DarkCyan, ConsoleColor.White, output);
                order = order.ProcessNextStep();
                delay();
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
