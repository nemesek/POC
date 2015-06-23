using BiggerBallOfMud.OrderStatuses;

namespace BiggerBallOfMud.Events
{
    public class OrderClosedEvent : IDomainEvent
    {
        public Order Order
        {
            get;
            set;
        }
    }
}
