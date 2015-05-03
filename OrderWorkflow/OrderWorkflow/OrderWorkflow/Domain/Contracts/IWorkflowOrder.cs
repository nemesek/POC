using System;

namespace OrderWorkflow.Domain.Contracts
{
    public interface IWorkflowOrder
    {
        IWorkflowOrder MakeTransition();
        OrderStatus Status { get; }
        Guid OrderId { get; }
        void Save();

    }
}
