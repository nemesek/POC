namespace BiggerBallOfMud.After
{
    public class VendorAcceptedOrder : Order
    {
        public VendorAcceptedOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status => OrderStatus.VendorAccepted;
        public override Order ProcessNextStep()
        {
            if (base.CmsId%5 == 0) return new ClosedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
            return this;
        }
    }
}
