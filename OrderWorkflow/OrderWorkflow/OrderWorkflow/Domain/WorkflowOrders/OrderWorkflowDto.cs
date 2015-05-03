using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class OrderWorkflowDto
    {
        public Func<ICanBeAutoAssigned, Vendor> AssignFunc { get; set; }
        public Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> ConditionalTransitionFunc { get; set; }
        public string ZipCode { get; set; }
        public Vendor Vendor { get; set; }
        public int ClientId { get; set; }
    }
}
