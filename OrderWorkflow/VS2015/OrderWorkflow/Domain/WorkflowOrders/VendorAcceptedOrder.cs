using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class VendorAcceptedOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>,bool,IWorkflowOrder> _transitionFunc;

        public VendorAcceptedOrder(Guid id,OrderWorkflowDto orderWorkflowDto):base(id,orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }

        public override OrderStatus Status { get { return OrderStatus.VendorAccepted; } }

        public override IWorkflowOrder MakeTransition()
        {
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), true);
        }
    }
}
