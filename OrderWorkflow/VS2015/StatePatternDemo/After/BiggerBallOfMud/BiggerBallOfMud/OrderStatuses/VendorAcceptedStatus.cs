namespace BiggerBallOfMud.OrderStatuses
{
    public class VendorAcceptedStatus : OrderStatus
    {
        public VendorAcceptedStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            if (Order.ClientId%5 == 0)
                return new ClosedStatus(Order);
            else
                return new ReviewAcceptanceStatus(Order);
        }
    }
}
