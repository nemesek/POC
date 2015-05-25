using OrderWorkflow.Domain.WorkflowOrders;

namespace OrderWorkflow.Domain.Events
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
