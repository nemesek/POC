namespace BiggerBallOfMud.OrderStatuses
{
    public class ClosedStatus : OrderStatus
    {
        public ClosedStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            return null;
        }
    }
}
