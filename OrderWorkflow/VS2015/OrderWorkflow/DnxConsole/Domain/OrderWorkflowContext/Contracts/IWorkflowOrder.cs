using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.Contracts
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
