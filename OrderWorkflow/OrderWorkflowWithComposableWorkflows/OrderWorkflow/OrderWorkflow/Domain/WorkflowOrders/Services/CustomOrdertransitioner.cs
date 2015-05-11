﻿using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class CustomOrdertransitioner : OrderTransitioner
    {
        public CustomOrdertransitioner(Func<ICanBeAutoAssigned, Vendor> safeAssign) : base(safeAssign){}

        protected override IWorkflowOrder TransitionToVendorAccepted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = base.TransitionToClosed;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.VendorAccepted, orderDto);
        }
    }
}