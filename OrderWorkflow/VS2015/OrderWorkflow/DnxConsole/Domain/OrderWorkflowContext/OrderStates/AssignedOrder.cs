using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.OrderStates
{
    public class AssignedOrder : Order
    {
        private readonly Vendor _vendor;
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _makeTransition;

        public AssignedOrder(Guid id, OrderWorkflowDto orderWorkflowDto, IOrderRepository repository):base(id,orderWorkflowDto, repository)
        {
            _vendor = base.Vendor;
            _makeTransition = orderWorkflowDto.StateTransitionFunc;
        }

        public override OrderStatus Status => OrderStatus.Assigned;

        protected override IWorkflowOrder MakeTransition()
        {
            _vendor.SendMeNotification(this);
            var vendorAccepted = _vendor.AcceptOrder(this);
            return _makeTransition(base.OrderId, base.MapToOrderWorkflowDto(),vendorAccepted);
        }
    }
}
