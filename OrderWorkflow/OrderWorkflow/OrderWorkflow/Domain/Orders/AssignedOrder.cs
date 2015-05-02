using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class AssignedOrder : Order
    {
        private readonly Vendor _vendor;
        private readonly Func<Guid, OrderDto, bool, IOrder> _transitionFunc;

        public AssignedOrder(Guid id, OrderDto orderDto):base(id,orderDto)
        {
            _vendor = base.OrderDto.Vendor;
            _transitionFunc = orderDto.ConditionalTransitionFunc;
        }

        public override OrderStatus Status { get { return OrderStatus.Assigned; } }

        public override IOrder MakeTransition()
        {
            _vendor.SendMeNotification(this);
            var vendorAccepted = _vendor.AcceptOrder(this);
            return _transitionFunc(base.OrderId, base.OrderDto,vendorAccepted);
        }
    }
}
