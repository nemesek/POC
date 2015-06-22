using System;
using System.Threading;

namespace BiggerBallOfMud.OrderStatuses
{
    public class ManualAssignStatus : OrderStatus
    {
        public ManualAssignStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            IOrderStatus newStatus;

            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            if (random.Next(1, 100)%2 != 0)
            {
                Console.WriteLine("^^^^^^^^Reassignment was not successful^^^^^^^^^^");
                newStatus = this;
            }
            else
            {
                var tempVendor = random.Next(1, 100)%2 != 0
                    ? new Vendor(0, "38655", "Daniel Garrett")
                    : new Vendor(0, "38655", "Dwain Richardson");

                Console.WriteLine("About to assign order to {0}", tempVendor.Name);
                Order.Vendor = tempVendor;
                newStatus = new AssignedStatus(Order);
            }

            return newStatus;
        }
    }
}
