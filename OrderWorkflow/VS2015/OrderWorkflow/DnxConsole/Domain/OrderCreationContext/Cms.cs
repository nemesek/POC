using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.OrderCreationContext
{
    public class Cms : Common.Cms
    {
        public Cms(int id, ILogEvents logger, ISendExternalMessenges messenger) : base(id, logger, messenger)
        {
        }

        public Order CreateOrder()
        {
            var order = new Order(base.Id);
            var address = new Address("Dallas", "TX", "75019", "Elm", "456");
            order.Create(address);
            Console.WriteLine("Order Created from CMS");
            return order;
        }
    }
}
