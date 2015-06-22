using System;
using System.Threading;

namespace BiggerBallOfMud.After
{
    public class ReviewAcceptanceOrder : Order
    {
        public ReviewAcceptanceOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status { get; }
        public override Order ProcessNextStep()
        {
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            if (random.Next(1, 100) % 2 == 0) return this;
            return new UnassignedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
        }
    }
}
