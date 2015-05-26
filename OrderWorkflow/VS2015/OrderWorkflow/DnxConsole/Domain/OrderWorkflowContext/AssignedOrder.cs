using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext
{
    public class AssignedOrder : Order
    {
        private readonly Vendor _vendor;
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public AssignedOrder(Guid id, OrderWorkflowDto orderWorkflowDto):base(id,orderWorkflowDto)
        {
            _vendor = base.Vendor;
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }

        public override OrderStatus Status => OrderStatus.Assigned;

        protected override IWorkflowOrder MakeTransition()
        {
            _vendor.SendMeNotification(this);
            var vendorAccepted = _vendor.AcceptOrder(this);
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(),vendorAccepted);
        }
    }
}
