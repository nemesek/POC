using DnxConsole.Domain.Common.Contracts;

namespace MockCms.Domain.Events
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
