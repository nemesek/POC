using System;

namespace BiggerBallOfMud.OrderStatuses
{
    public class AssignedStatus : OrderStatus
    {
        public AssignedStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            IOrderStatus newStatus;

            if (Order.Vendor.AcceptOrder(Order))
            {
                newStatus = new VendorAcceptedStatus(Order);
                Console.WriteLine("Vendor accepted.");
            }
            else
            {
                Console.WriteLine("Vendor declined.");
                newStatus = new UnassignedStatus(Order);
            }

            return newStatus;
        }
    }
}
