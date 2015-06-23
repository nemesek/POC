using System;
using System.Threading;

namespace BiggerBallOfMud.After
{
    public class ReviewSubmissionOrder : Order
    {
        public ReviewSubmissionOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status => OrderStatus.ReviewSubmission;
        public override Order ProcessNextStep()
        {
            // randomly determine if its rejected
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();

            if (random.Next(1, 100) % 2 == 0)
            {
                // means we accepted
                return new ClosedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
            }

            Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage!!!!!!!!!!!!!!!!");
            return new RejectedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
        }
    }
}
