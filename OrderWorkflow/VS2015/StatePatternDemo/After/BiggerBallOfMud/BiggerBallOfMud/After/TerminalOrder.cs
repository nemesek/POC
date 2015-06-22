namespace BiggerBallOfMud.After
{
    public class TerminalOrder : Order
    {
        public TerminalOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status => OrderStatus.Terminal; 
        public override Order ProcessNextStep()
        {
            return this;
        }
    }
}
