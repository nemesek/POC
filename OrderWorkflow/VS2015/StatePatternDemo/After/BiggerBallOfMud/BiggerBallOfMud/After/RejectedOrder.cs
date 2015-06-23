using System;

namespace BiggerBallOfMud.After
{
    public class RejectedOrder : Order
    {
        public RejectedOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status => OrderStatus.Rejected;

        public override Order ProcessNextStep()
        {
            if (base.CmsId%3 != 0) return new ReviewSubmissionOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
            Console.WriteLine("**************Applying custom rejected order business logic******************");
            return new ReviewSubmissionOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
        }
    }
}
