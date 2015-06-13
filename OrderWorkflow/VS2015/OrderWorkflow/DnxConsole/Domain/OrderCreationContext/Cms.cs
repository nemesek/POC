using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Common.Contracts;


namespace DnxConsole.Domain.OrderCreationContext
{
    public class Cms : Common.Cms
    {
        private readonly IOrderCreationRepository _repository;

        public Cms(int id, ILogEvents logger, ISendExternalMessenges messenger, IOrderCreationRepository repository) : base(id, logger, messenger)
        {
            _repository = repository;
        }

        public Order CreateOrder()
        {
            var serviceId = base.GetServiceId();
            var loan = _repository.GetLoan(100);
            var order = new Order(base.Id, _repository, serviceId);
            var address = new Address("Dallas", "TX", "75019", "Elm", "456");
            order.Create(address);
            loan.AddOrder(order);
            Console.WriteLine("Order Created from CMS");
            return order;
        }
    }
}
    


