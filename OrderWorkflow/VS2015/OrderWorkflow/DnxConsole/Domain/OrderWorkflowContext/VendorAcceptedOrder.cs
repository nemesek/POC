﻿using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext
{
    public class VendorAcceptedOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>,bool,IWorkflowOrder> _transitionFunc;

        public VendorAcceptedOrder(Guid id,OrderWorkflowDto orderWorkflowDto):base(id,orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }

        public override OrderStatus Status => OrderStatus.VendorAccepted;

        public override IWorkflowOrder MakeTransition()
        {
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), true);
        }
    }
}