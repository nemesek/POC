using System;

namespace OrderWorkflow.Domain.Contracts
{
    public interface IWorkflowOrder
    {
        IWorkflowOrder MakeTransition();
        OrderStatus Status { get; }
        void AssignVendor(Vendor vendor);
        Guid OrderId { get; }
        void Save();

    }
}
