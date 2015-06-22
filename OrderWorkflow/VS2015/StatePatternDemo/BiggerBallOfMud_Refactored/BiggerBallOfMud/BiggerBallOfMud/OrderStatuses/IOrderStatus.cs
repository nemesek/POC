namespace BiggerBallOfMud.OrderStatuses
{
    public interface IOrderStatus
    {
        Order Order { get; }
        IOrderStatus ProcessNextStep();

    }
}
