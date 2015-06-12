using DnxConsole.Domain.Common.Contracts;

namespace MockCms.Domain.Events
{
    public class OrderUpdatedEvent : IDomainEvent
    {
        public Order Order { get; set; }
    }
}
