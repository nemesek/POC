using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class AcceptedOrder : IOrder
    {
        private readonly Guid _id;
        private readonly Func<Guid, OrderDto, IOrder> _transitionFunc;
        private readonly int _clientId;
        private readonly OrderDto _orderDto;

        public AcceptedOrder(Guid id,OrderDto orderDto)
        {
            _id = id;
            _transitionFunc = orderDto.TransitionFunc;
            _clientId = orderDto.ClientId;
            _orderDto = orderDto;

        }
        public IOrder MakeTransition()
        {
            return _transitionFunc(_id, _orderDto);
        }

        public OrderStatus Status { get { return OrderStatus.Accepted; } }
        public Guid OrderId { get { return _id; }}
        public int ClientId { get { return _clientId; } }
    }
}
