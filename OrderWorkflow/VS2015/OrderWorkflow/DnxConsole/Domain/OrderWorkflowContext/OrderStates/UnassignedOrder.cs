﻿using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.OrderStates
{
    public class UnassignedOrder : Order, ICanBeAutoAssigned
    {
        private readonly Func<ICanBeAutoAssigned,Vendor> _assignVendor;
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool,IWorkflowOrder> _makeTransition;
        private readonly string _zipCode;

        public UnassignedOrder(Guid id, OrderWorkflowDto orderWorkflowDto, IOrderRepository repository):base(id, orderWorkflowDto,repository)
        {
            _assignVendor = orderWorkflowDto.AssignVendorFunc;
            _makeTransition = orderWorkflowDto.StateTransitionFunc;
            _zipCode = orderWorkflowDto.ZipCode;
        }

        public override OrderStatus Status => OrderStatus.Unassigned;
        public string ZipCode => _zipCode;

        protected override IWorkflowOrder MakeTransition()
        {
            var vendorToAssign = _assignVendor(this);
            if (vendorToAssign == null)
            {
                return _makeTransition(base.OrderId, base.MapToOrderWorkflowDto(), false);
            }
            
            base.AssignVendor(vendorToAssign);
            return _makeTransition(base.OrderId, base.MapToOrderWorkflowDto(), true);
        }
    }
}
