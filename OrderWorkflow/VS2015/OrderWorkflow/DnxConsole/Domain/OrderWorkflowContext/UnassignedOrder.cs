using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext
{
    public class UnassignedOrder : Order, ICanBeAutoAssigned
    {
        private readonly Func<ICanBeAutoAssigned,Vendor> _assignVendorFunc;
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool,IWorkflowOrder> _transitionFunc;
        private readonly string _zipCode;

        public UnassignedOrder(Guid id, OrderWorkflowDto orderWorkflowDto):base(id, orderWorkflowDto)
        {
            _assignVendorFunc = orderWorkflowDto.AssignVendorFunc;
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
            _zipCode = orderWorkflowDto.ZipCode;
        }

        public override OrderStatus Status => OrderStatus.Unassigned;
        public string ZipCode => _zipCode;

        public override IWorkflowOrder MakeTransition()
        {
            var vendorToAssign = _assignVendorFunc(this);
            if (vendorToAssign == null)
            {
                return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), false);
            }
            
            base.AssignVendor(vendorToAssign);
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), true);
        }
    }
}
