using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class OrderDto
    {
        public Func<IOrderWithZipCode, Vendor> AssignFunc { get; set; }
        public Func<Guid, Func<OrderDto>, bool, IWorkflowOrder> ConditionalTransitionFunc { get; set; }
        public string ZipCode { get; set; }
        public Vendor Vendor { get; set; }
        public int ClientId { get; set; }
    }
}
