using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class SubmittedOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public SubmittedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }
        
        public override OrderStatus Status => OrderStatus.Submitted;

        public override IWorkflowOrder MakeTransition()
        {
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), true);
        }
    }
}
