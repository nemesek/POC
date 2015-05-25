using DnxConsole.Domain.OrderEditContext;

namespace DnxConsole.Domain.Events
{
    public class OrderUpdatedEvent : IDomainEvent
    {
        public Order Order { get; set; }
    }
}
