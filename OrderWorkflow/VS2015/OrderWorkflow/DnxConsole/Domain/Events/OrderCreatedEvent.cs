using DnxConsole.OrderCreationContext;

namespace DnxConsole.Domain.Events
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
