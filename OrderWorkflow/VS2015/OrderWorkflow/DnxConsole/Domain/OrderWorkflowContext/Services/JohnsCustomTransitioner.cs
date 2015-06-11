using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.Services
{
    public class JohnsCustomTransitioner : OrderTransitioner
    {
        public JohnsCustomTransitioner(Func<ICanBeAutoAssigned, Vendor> safeAssign, WorkflowOrderFactory orderFactory)
            : base(safeAssign, orderFactory) {}

        protected override IWorkflowOrder TransitionToSubmitted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool moveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = base.TransitionToClosed;
            return base.OrderFactory.GetWorkflowOrder(orderId, OrderStatus.Submitted, orderDto);

        }

    }
}
