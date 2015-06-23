using BiggerBallOfMud.OrderStatuses;

namespace BiggerBallOfMud.Events
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public Order Order
        {
            get;
            set;
        }
    }
}
