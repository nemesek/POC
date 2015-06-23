namespace BiggerBallOfMud.After
{
    public class ClosedOrder : Order
    {
        public ClosedOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status => OrderStatus.Closed; 
        public override Order ProcessNextStep()
        {
            return new TerminalOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
        }
    }
}
