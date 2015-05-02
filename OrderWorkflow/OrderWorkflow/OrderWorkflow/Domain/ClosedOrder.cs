using System;

namespace OrderWorkflow.Domain
{
    public class ClosedOrder : IOrder
    {
        private Vendor _vendor;
        private Guid _id;

        public ClosedOrder(Guid id, Vendor vendor)
        {
            _id = id;
            _vendor = vendor;
        }
        public IOrder MakeTransition()
        {
            throw new NotImplementedException();
        }

        public OrderStatus Status { get { return OrderStatus.Closed; } }
        public Guid OrderId { get; private set; }
    }
}
