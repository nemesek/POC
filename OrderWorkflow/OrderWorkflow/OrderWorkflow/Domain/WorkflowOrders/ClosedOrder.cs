using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.WorkflowOrders;

namespace OrderWorkflow.Domain.Orders
{
    public class ClosedOrder : Order
    {
        private readonly Func<Guid, Func<OrderDto>, bool, IWorkflowOrder> _transitionFunc;

        public ClosedOrder(Guid id, OrderDto orderDto) : base(id, orderDto)
        {
            _transitionFunc = orderDto.ConditionalTransitionFunc;
        }

        public override OrderStatus Status { get { return OrderStatus.Closed; } }
        
        public override IWorkflowOrder MakeTransition()
        {
            var order = _transitionFunc(base.OrderId, base.MapToOrderDto(), true);
            return order;
        }
    }
}
