using System;

namespace OrderWorkflow.Domain
{
    public class AssignedOrder : IOrder
    {
        private readonly Vendor _vendor;
        private readonly Guid _id;
        private readonly Func<Guid, Vendor, IOrder> _transitionFunc;

        public AssignedOrder(Guid id, Vendor vendor, Func<Guid, Vendor, IOrder> transitionFunc)
        {
            _id = id;
            _vendor = vendor;
            _transitionFunc = transitionFunc;
        }

        public IOrder MakeTransition()
        {
            _vendor.SendMeNotification(this);
            var vendorAccept = _vendor.AcceptOrder(this);
            if (vendorAccept)
            {
                return _transitionFunc(_id, _vendor);
            }

            return null; // todo: let it go back to New
        }

        public OrderStatus Status { get { return OrderStatus.Assigned; } }
        public Guid OrderId { get { return _id; } }
    }
}
