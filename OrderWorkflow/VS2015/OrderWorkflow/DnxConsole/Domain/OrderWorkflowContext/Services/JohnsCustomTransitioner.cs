using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.Services
{
    public class JohnsCustomTransitioner : OrderTransitioner
    {
        public JohnsCustomTransitioner(Func<ICanBeAutoAssigned, Vendor> safeAssign) : base(safeAssign){}

        protected override IWorkflowOrder TransitionToSubmitted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool moveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = base.TransitionToClosed;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.Submitted, orderDto);
        }

    }
}
