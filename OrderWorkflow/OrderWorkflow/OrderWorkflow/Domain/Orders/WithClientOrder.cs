using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class WithClientOrder : Order
    {
        private readonly Func<Guid, OrderDto, bool, IOrder> _transitionFunc;

        public WithClientOrder(Guid id, OrderDto orderDto) : base(id, orderDto)
        {
            _transitionFunc = orderDto.ConditionalTransitionFunc;
        }
        
        public override OrderStatus Status
        {
            get { return OrderStatus.WithClient; }
        }

        public override IOrder MakeTransition()
        {
            return _transitionFunc(base.OrderId, base.OrderDto, true);
        }
    }
}
