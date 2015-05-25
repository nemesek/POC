using DnxConsole.Domain.OrderWorkflowContext;

namespace DnxConsole.Domain.Events
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
