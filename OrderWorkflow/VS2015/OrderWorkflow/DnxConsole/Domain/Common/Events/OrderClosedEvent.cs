using DnxConsole.Domain.Common.Contracts;
using DnxConsole.Domain.OrderWorkflowContext;

namespace DnxConsole.Domain.Common.Events
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
