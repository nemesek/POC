using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class OrderWorkflowDto
    {
        public Func<ICanBeAutoAssigned, Vendor> AssignVendorFunc { get; set; }
        public Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> StateTransitionFunc { get; set; }
        public string ZipCode { get; set; }
        public Vendor Vendor { get; set; }
        public int ClientId { get; set; }
    }
}
