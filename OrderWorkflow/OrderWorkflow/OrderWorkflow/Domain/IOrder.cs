using System;

namespace OrderWorkflow.Domain
{
    public interface IOrder
    {
        IOrder MakeTransition();
        OrderStatus Status { get; }
        Guid OrderId { get; }
        
    }
}
