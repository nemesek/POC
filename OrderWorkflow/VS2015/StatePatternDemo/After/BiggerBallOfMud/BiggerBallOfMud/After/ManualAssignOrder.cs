using System;
using System.Threading;

namespace BiggerBallOfMud.After
{
    public class ManualAssignOrder : Order
    {
        public ManualAssignOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status => OrderStatus.Assigned;
        public override Order ProcessNextStep()
        {
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            if (random.Next(1, 100)%2 != 0)
            {
                Console.WriteLine("^^^^^^^^Reassignment was not successful^^^^^^^^^^");
                return this;
            }

            var tempVendor = random.Next(1, 100) % 2 != 0
                   ? new Vendor(0, "38655", "Daniel Garrett")
                   : new Vendor(0, "38655", "Dwain Richardson");

            Console.WriteLine("About to assign order to {0}", tempVendor.Name);
            base.AssignVendor(tempVendor);
            return new AssignedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);

        }
    }
}
