using System;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext
{
    public class OrderWorkflowDto
    {
        public Func<ICanBeAutoAssigned, Vendor> AssignVendorFunc { get; set; }
        public Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> StateTransitionFunc { get; set; }
        public string ZipCode { get; set; }
        public Vendor Vendor { get; set; }
        public Cms Cms { get; set; }
        
    }
}
