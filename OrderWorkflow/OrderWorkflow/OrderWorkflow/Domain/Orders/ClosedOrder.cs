using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class ClosedOrder : IOrder
    {
        //private Vendor _vendor;
        private readonly Guid _id;
        private readonly int _clientId;

        public ClosedOrder(Guid id, OrderDto orderDto)
        {
            _id = id;
            _clientId = orderDto.ClientId;
        }
        public IOrder MakeTransition()
        {
            throw new NotImplementedException();
        }

        public OrderStatus Status { get { return OrderStatus.Closed; } }
        public Guid OrderId { get { return _id; } }
        public int ClientId { get { return _clientId; } }
    }
}
