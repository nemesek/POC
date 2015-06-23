using System;

namespace BiggerBallOfMud.After
{
    public class AssignedOrder : Order
    {
        public AssignedOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
            if (base.AssignedVendor == null) throw new ArgumentNullException(nameof(vendor));
        }

        public override OrderStatus Status => OrderStatus.Assigned;
        public override Order ProcessNextStep()
        {
            if (base.AssignedVendor.AcceptOrder(this))
            {
                Console.WriteLine("Vendor accepted.");
                return new VendorAcceptedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
            }

            Console.WriteLine("Vendor declined.");
            return new UnassignedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
        }
    }
}
