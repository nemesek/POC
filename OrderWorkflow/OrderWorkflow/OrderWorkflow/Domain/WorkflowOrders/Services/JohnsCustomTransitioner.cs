using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class JohnsCustomTransitioner : OrderTransitioner
    {
        public JohnsCustomTransitioner(Func<ICanBeAutoAssigned, Vendor> safeAssign) : base(safeAssign){}

        protected override IWorkflowOrder TransitionToSubmitted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool moveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = base.TransitionToClosed;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Submitted, orderDto);
        }

    }
}
