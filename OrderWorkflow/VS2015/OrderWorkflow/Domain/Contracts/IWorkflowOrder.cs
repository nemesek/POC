using System;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.Contracts
{
    public interface IWorkflowOrder
    {
        //IWorkflowOrder MakeTransition();
        IWorkflowOrder ProcessNextStep();
        OrderStatus Status { get; }
        void AssignVendor(Vendor vendor);
        Guid OrderId { get; }
        void Save();

    }
}
