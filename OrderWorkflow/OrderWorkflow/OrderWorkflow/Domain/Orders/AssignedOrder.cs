using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class AssignedOrder : Order
    {
        private readonly Vendor _vendor;
        private readonly Func<Guid, Func<OrderDto>, bool, IWorkflowOrder> _transitionFunc;

        public AssignedOrder(Guid id, OrderDto orderDto):base(id,orderDto)
        {
            _vendor = base.Vendor;
            _transitionFunc = orderDto.ConditionalTransitionFunc;
        }

        public override OrderStatus Status { get { return OrderStatus.Assigned; } }

        public override IWorkflowOrder MakeTransition()
        {
            _vendor.SendMeNotification(this);
            var vendorAccepted = _vendor.AcceptOrder(this);
            return _transitionFunc(base.OrderId, base.MapToOrderDto(),vendorAccepted);
        }
    }
}
