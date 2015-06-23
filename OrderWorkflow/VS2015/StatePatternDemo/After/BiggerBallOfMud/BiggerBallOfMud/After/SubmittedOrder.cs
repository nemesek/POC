using System;

namespace BiggerBallOfMud.After
{
    public class SubmittedOrder : Order
    {
        public SubmittedOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status { get; }
        public override Order ProcessNextStep()
        {
            switch (this.CmsId)
            {
                case 21:
                case 14:
                case 24:
                    return new ClosedOrder(base.CmsId, base.ZipCode, base.AssignedVendor); 
                case 17:
                case 16:
                case 22:
                    Console.WriteLine("Doing John Additional Order Submitted Buisness Logic");
                    return new ReviewSubmissionOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
                default:
                    return new ReviewSubmissionOrder(base.CmsId, base.ZipCode, base.AssignedVendor);

            }
        }
    }
}
