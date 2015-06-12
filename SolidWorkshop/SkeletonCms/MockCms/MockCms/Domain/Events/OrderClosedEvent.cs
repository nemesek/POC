using DnxConsole.Domain.Common.Contracts;

namespace MockCms.Domain.Events
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
