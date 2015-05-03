using System;
using System.Threading;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public abstract class Order : IWorkflowOrder
    {
        private readonly Guid _id;
        private readonly int _clientId;
        private Vendor _vendor;

        protected Order(Guid id, OrderDto orderDto)
        {
            _id = id;
            _clientId = orderDto.ClientId;
            _vendor = orderDto.Vendor;
        }

        public abstract IWorkflowOrder MakeTransition();
        public abstract OrderStatus Status { get; }

        protected Vendor Vendor{ get { return _vendor; } }
        
        public Guid OrderId { get { return _id; } }
        
        protected void AssignVendor(Vendor vendor)
        {
            if (this.Status != OrderStatus.Unassigned) throw new Exception("Order is not in correct state to be Assigned.");
            // some additional business logic if required
            _vendor = vendor;
        }

        protected bool AcceptSubmittedReport()
        {
            if (this.Status != OrderStatus.Submitted && this.Status != OrderStatus.Rejected)
            {
                throw new Exception("Order is not in correct state to have report Submitted.");
            }

            // randomly determine if its rejected
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            return random.Next(1, 100)%2 == 0;
        }

        protected Func<OrderDto> MapToOrderDto()
        {
            return () => new OrderDto
            {
                ClientId = _clientId,
                Vendor = _vendor
            };
        }

        public void Save()
        {
            Console.WriteLine("Saving {0} State to DB", Status);
        }
    }
}
