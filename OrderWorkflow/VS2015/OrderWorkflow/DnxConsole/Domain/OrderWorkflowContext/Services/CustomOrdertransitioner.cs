using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.Services
{
    public class CustomOrdertransitioner : OrderTransitioner
    {
        public CustomOrdertransitioner(Func<ICanBeAutoAssigned, Vendor> safeAssign) : base(safeAssign){}

        protected override IWorkflowOrder TransitionToVendorAccepted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = base.TransitionToClosed;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.VendorAccepted, orderDto);
        }
    }
}
