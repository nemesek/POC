using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class NewOrder : Order, IOrderWithZipCode
    {
        private readonly Func<IOrderWithZipCode,Vendor> _assignFunc;
        private readonly Func<Guid, OrderDto, bool,IOrder> _transitionFunc;
        private readonly string _zipCode;

        public NewOrder(Guid id, OrderDto orderDto):base(id, orderDto)
        {
            _assignFunc = orderDto.AssignFunc;
            _transitionFunc = orderDto.ConditionalTransitionFunc;
            _zipCode = orderDto.ZipCode;
        }

        public override OrderStatus Status { get { return OrderStatus.New; } }
        public string ZipCode { get { return _zipCode; } }

        public override IOrder MakeTransition()
        {
            var vendorToAssign = _assignFunc(this);
            var vendorAssigned = vendorToAssign != null;
            // todo: Don't know how I feel about child class writing to base class state like this
            base.OrderDto.Vendor = vendorToAssign;
            var assignedOrder = _transitionFunc(base.OrderId, base.OrderDto,vendorAssigned);
            return assignedOrder;
        }
    }
}
