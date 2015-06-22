namespace BiggerBallOfMud.OrderStatuses
{
    public abstract class OrderStatus : IOrderStatus
    {
        public Order Order { get; }

        protected OrderStatus(Order order)
        {
            Order = order;
        }

        public abstract IOrderStatus ProcessNextStep();
    }
}
