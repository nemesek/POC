using OrderWorkflow.Domain.OrderCreation;

namespace OrderWorkflow.Domain.Events
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
