using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class AssignedOrder : Order
    {
        private readonly Vendor _vendor;
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public AssignedOrder(Guid id, OrderWorkflowDto orderWorkflowDto):base(id,orderWorkflowDto)
        {
            _vendor = base.Vendor;
            _transitionFunc = orderWorkflowDto.ConditionalTransitionFunc;
        }

        public override OrderStatus Status { get { return OrderStatus.Assigned; } }

        public override IWorkflowOrder MakeTransition()
        {
            _vendor.SendMeNotification(this);
            var vendorAccepted = _vendor.AcceptOrder(this);
            return _transitionFunc(base.OrderId, base.MapToOrderDto(),vendorAccepted);
        }
    }
}
