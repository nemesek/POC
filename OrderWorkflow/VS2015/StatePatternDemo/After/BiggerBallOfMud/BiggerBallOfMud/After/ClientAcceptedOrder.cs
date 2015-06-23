namespace BiggerBallOfMud.After
{
    public class ClientAcceptedOrder : Order
    {
        public ClientAcceptedOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status => OrderStatus.ClientAccepted; 
        public override Order ProcessNextStep()
        {
            return new SubmittedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);    // SubmittedStatus
        }
    }
}
