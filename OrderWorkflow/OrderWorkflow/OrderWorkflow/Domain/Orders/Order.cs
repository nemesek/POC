using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public abstract class Order : IOrder
    {
        private readonly Guid _id;
        private readonly int _clientId;
        private readonly OrderDto _orderDto;

        protected Order(Guid id, OrderDto orderDto)
        {
            _id = id;
            _clientId = orderDto.ClientId;
            _orderDto = orderDto;
        }

        public abstract IOrder MakeTransition();
        public abstract OrderStatus Status { get; }
        public Guid OrderId { get { return _id; } }
        public int ClientId { get { return _clientId; } }
        protected OrderDto OrderDto { get { return _orderDto; } }

        public void Save()
        {
            Console.WriteLine("Saving {0} State to DB", Status);
        }
    }
}
