using System;

namespace OrderWorkflow.Domain.Contracts
{
    public interface IOrder
    {
        IOrder MakeTransition();
        OrderStatus Status { get; }
        Guid OrderId { get; }
        int ClientId { get; }
        
    }
}
