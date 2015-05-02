using System;

namespace OrderWorkflow.Domain
{
    public class NewOrder : IOrder
    {
        private readonly Guid _id;
        private readonly Func<IOrder,Vendor> _assignFunc;
        private readonly Func<Guid, Vendor, IOrder> _transitionFunc;

        public NewOrder(Guid id, Func<IOrder,Vendor> assignFunc, Func<Guid, Vendor, IOrder> transitionFunc)
        {
            _id = id;
            _assignFunc = assignFunc;
            _transitionFunc = transitionFunc;
        }
        public IOrder MakeTransition()
        {
            var vendorToAssign = _assignFunc(this);
            var assignedOrder = _transitionFunc(_id, vendorToAssign);
            return assignedOrder;
        }

        public OrderStatus Status { get { return OrderStatus.New; } }
        public Guid OrderId { get { return _id; }}
    }
}
