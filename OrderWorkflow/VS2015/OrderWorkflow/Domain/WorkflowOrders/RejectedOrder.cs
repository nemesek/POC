using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class RejectedOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public RejectedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }
        
        public override OrderStatus Status => OrderStatus.Rejected;

        public override IWorkflowOrder MakeTransition()
        {
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), true);
        }
    }
}
