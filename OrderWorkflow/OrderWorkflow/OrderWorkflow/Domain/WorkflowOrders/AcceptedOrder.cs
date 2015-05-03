using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.WorkflowOrders;

namespace OrderWorkflow.Domain.Orders
{
    public class AcceptedOrder : Order
    {
        private readonly Func<Guid, Func<OrderDto>,bool,IWorkflowOrder> _transitionFunc;

        public AcceptedOrder(Guid id,OrderDto orderDto):base(id,orderDto)
        {
            _transitionFunc = orderDto.ConditionalTransitionFunc;
        }

        public override OrderStatus Status { get { return OrderStatus.Accepted; } }

        public override IWorkflowOrder MakeTransition()
        {
            return _transitionFunc(base.OrderId, base.MapToOrderDto(), true);
        }
    }
}
