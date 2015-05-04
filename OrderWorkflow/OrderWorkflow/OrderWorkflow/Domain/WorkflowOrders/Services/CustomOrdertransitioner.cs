using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class CustomOrdertransitioner : OrderTransitioner
    {
        protected override IWorkflowOrder TransitionToAccepted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = base.TransitionToClosed;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Accepted, orderDto);
        }
    }
}
