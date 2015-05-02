using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class Unassigned : Order, IOrderWithZipCode
    {
        private readonly Func<IOrderWithZipCode,Vendor> _assignFunc;
        private readonly Func<Guid, Func<OrderDto>, bool,IOrder> _transitionFunc;
        private readonly string _zipCode;

        public Unassigned(Guid id, OrderDto orderDto):base(id, orderDto)
        {
            _assignFunc = orderDto.AssignFunc;
            _transitionFunc = orderDto.ConditionalTransitionFunc;
            _zipCode = orderDto.ZipCode;
        }

        public override OrderStatus Status { get { return OrderStatus.Unassigned; } }
        public string ZipCode { get { return _zipCode; } }

        public override IOrder MakeTransition()
        {
            var vendorToAssign = _assignFunc(this);
            if (vendorToAssign == null)
            {
                return _transitionFunc(base.OrderId, base.MapToOrderDto(), false);
            }
            
            base.AssignVendor(vendorToAssign);
            return _transitionFunc(base.OrderId, base.MapToOrderDto(), true);
        }
    }
}
