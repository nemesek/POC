using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class WithClientOrder : IOrder
    {
        private readonly Guid _id;
        private OrderDto _orderDto;
        private readonly int _clientId;

        public WithClientOrder(Guid id, OrderDto orderDto)
        {
            _id = id;
            _orderDto = orderDto;
            _clientId = orderDto.ClientId;
        }

        public IOrder MakeTransition()
        {
            throw new NotImplementedException();
        }

        public OrderStatus Status
        {
            get { return OrderStatus.WithClient; }
        }

        public Guid OrderId
        {
            get { return _id; }
        }

        public int ClientId
        {
            get { return _clientId; }
        }
    }
}
