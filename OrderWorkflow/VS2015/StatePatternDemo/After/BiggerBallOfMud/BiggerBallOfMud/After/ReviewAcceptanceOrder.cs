using System;
using System.Threading;

namespace BiggerBallOfMud.After
{
    public class ReviewAcceptanceOrder : Order
    {
        public ReviewAcceptanceOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status => OrderStatus.ReviewAcceptance;
        public override Order ProcessNextStep()
        {
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            if (random.Next(1, 100) % 2 == 0) return new ClientAcceptedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
            return new UnassignedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
        }
    }
}
