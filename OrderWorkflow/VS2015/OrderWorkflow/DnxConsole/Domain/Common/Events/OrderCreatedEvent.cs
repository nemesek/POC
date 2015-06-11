using DnxConsole.Domain.Common.Contracts;
using DnxConsole.Domain.OrderCreationContext;

namespace DnxConsole.Domain.Common.Events
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
