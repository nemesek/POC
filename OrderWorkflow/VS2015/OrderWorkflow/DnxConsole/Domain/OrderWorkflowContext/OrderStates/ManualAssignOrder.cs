﻿using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.OrderStates
{
    public class ManualAssignOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _makeTransition;

        public ManualAssignOrder(Guid id, OrderWorkflowDto orderWorkflowDto, IOrderRepository repository):base(id,orderWorkflowDto, repository)
        {
            _makeTransition = orderWorkflowDto.StateTransitionFunc;
        }
        
        public override OrderStatus Status => OrderStatus.ManualAssign;

        protected override IWorkflowOrder MakeTransition()
        {
            var assigned = base.Cms.ManualAssign(this);
            if (!assigned) Console.WriteLine("^^^^^^^^Reassignment was not successful^^^^^^^^^^");
            return _makeTransition(base.OrderId, base.MapToOrderWorkflowDto(), assigned);
        }
    }
}
