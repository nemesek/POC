using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class AcceptedOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>,bool,IWorkflowOrder> _transitionFunc;

        public AcceptedOrder(Guid id,OrderWorkflowDto orderWorkflowDto):base(id,orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.ConditionalTransitionFunc;
        }

        public override OrderStatus Status { get { return OrderStatus.Accepted; } }

        public override IWorkflowOrder MakeTransition()
        {
            return _transitionFunc(base.OrderId, base.MapToOrderDto(), true);
        }
    }
}
