using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Common;
using OrderWorkflow.Domain.Events;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class ClosedOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public ClosedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }

        public override OrderStatus Status => OrderStatus.Closed;

        public override IWorkflowOrder MakeTransition()
        {
            var order = _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), true);
            return order;
        }
    }
}
