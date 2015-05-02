using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain
{
    public class OrderDto
    {
        public Func<IOrderWithZipCode, Vendor> AssignFunc { get; set; }
        public Func<Guid, Func<OrderDto>, bool, IOrder> ConditionalTransitionFunc { get; set; }
        public string ZipCode { get; set; }
        public Vendor Vendor { get; set; }
        public int ClientId { get; set; }
    }
}
