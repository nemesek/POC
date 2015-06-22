namespace BiggerBallOfMud.OrderStatuses
{
    public class ClientAcceptedStatus : OrderStatus
    {
        public ClientAcceptedStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            return new SubmittedStatus(Order);
        }
    }
}
