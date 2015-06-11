using DnxConsole.Domain.Common.Contracts;
using DnxConsole.Domain.OrderEditContext;

namespace DnxConsole.Domain.Common.Events
{
    public class OrderUpdatedEvent : IDomainEvent
    {
        public Order Order { get; set; }
    }
}
