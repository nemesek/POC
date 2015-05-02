using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class ClosedOrder : Order
    {
        private readonly Func<Guid, Func<OrderDto>, bool, IOrder> _transitionFunc;

        public ClosedOrder(Guid id, OrderDto orderDto) : base(id, orderDto)
        {
            _transitionFunc = orderDto.ConditionalTransitionFunc;
        }

        public override OrderStatus Status { get { return OrderStatus.Closed; } }
        
        public override IOrder MakeTransition()
        {
            var order = _transitionFunc(base.OrderId, base.MapToOrderDto(), true);
            return order;
        }
    }
}
