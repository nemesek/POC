using System;

namespace OrderWorkflow.Domain
{
    public class AcceptedOrder : IOrder
    {
        private readonly Vendor _vendor;
        private readonly Guid _id;
        private readonly Func<Guid, Vendor, IOrder> _transitionFunc;

        public AcceptedOrder(Guid id, Vendor vendor, Func<Guid, Vendor, IOrder> transitionFunc)
        {
            _id = id;
            _vendor = vendor;
            _transitionFunc = transitionFunc;
        }
        public IOrder MakeTransition()
        {
            return _transitionFunc(_id, _vendor);
        }

        public OrderStatus Status { get { return OrderStatus.Accepted; } }
        public Guid OrderId { get; private set; }
    }
}
