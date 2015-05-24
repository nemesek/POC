namespace DomainEvents
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
