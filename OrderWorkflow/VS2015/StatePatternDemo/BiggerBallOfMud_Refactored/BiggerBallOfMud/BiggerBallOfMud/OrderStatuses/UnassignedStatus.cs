using System;
using System.Linq;

namespace BiggerBallOfMud.OrderStatuses
{
    public class UnassignedStatus : OrderStatus
    {
        public UnassignedStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            IOrderStatus newStatus;
            Vendor tempVendor;
            var vendorRepo = new VendorRepository();

            switch (Order.ClientId % 4)
            {
                case 0:
                    tempVendor = vendorRepo
                        .GetVendors()
                        .FirstOrDefault();

                    break;
                case 1:
                    tempVendor = vendorRepo
                        .GetVendors()
                        .FirstOrDefault(v => v.ZipCode == Order.ZipCode);

                    break;
                case 2:
                    tempVendor = vendorRepo
                        .GetVendors()
                        .Where(v => v.ZipCode == Order.ZipCode)
                        .OrderBy(v => v.OrderCount)
                        .FirstOrDefault();

                    break;
                default:
                    tempVendor = null;
                    break;
            }

            if (tempVendor != null)
            {
                Console.WriteLine("About to assign order to {0}", tempVendor.Name);
                Order.Vendor = tempVendor;
                newStatus = new AssignedStatus(Order);
            }
            else
            {
                Console.WriteLine("Going to have to manully assign.");
                newStatus = new ManualAssignStatus(Order);                
            }

            return newStatus;
        }
    }
}
