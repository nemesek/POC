namespace BiggerBallOfMud.After
{
    public class ClientAcceptedOrder : Order
    {
        public ClientAcceptedOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status { get; }
        public override Order ProcessNextStep()
        {
            return null;    // SubmittedStatus
        }
    }
}
