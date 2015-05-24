using OrderWorkflow.Domain.OrderEdit;

namespace OrderWorkflow.Domain.Events
{
    public class OrderUpdatedEvent : IDomainEvent
    {
        public Order Order { get; set; }
    }
}
