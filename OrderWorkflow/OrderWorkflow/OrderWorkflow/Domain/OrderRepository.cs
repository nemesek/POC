using System;

namespace OrderWorkflow.Domain
{
    public class OrderRepository
    {
        public IOrder CreateNewOrder(Func<IOrder,Vendor> assignFunc)
        {
            return new NewOrder(Guid.NewGuid(), assignFunc, GetAssignedOrder);
        }

        public IOrder GetAssignedOrder(Guid orderId, Vendor vendor)
        {
            return new AssignedOrder(orderId, vendor, GetAcceptedOrder);
        }

        public IOrder GetAcceptedOrder(Guid orderId, Vendor vendor)
        {
            return new AcceptedOrder(orderId, vendor, GetClosedOrder);
        }

        public IOrder GetClosedOrder(Guid orderId, Vendor vendor)
        {
            return new ClosedOrder(orderId, vendor);
        }

    }
}
