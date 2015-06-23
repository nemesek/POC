using BiggerBallOfMud.OrderStatuses;

namespace BiggerBallOfMud.Events
{
    public class OrderUpdatedEvent : IDomainEvent
    {
        public Order Order { get; set; }
    }
}
