using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public abstract class Order : IWorkflowOrder
    {
        private readonly Guid _id;
        private readonly int _clientId;
        private Vendor _vendor;

        protected Order(Guid id, OrderWorkflowDto orderWorkflowDto)
        {
            _id = id;
            _clientId = orderWorkflowDto.ClientId;
            _vendor = orderWorkflowDto.Vendor;
        }

        // MakeTransition and Status combine for my variation of state pattern
        // http://www.dofactory.com/net/state-design-pattern
        public abstract IWorkflowOrder MakeTransition();
        public abstract OrderStatus Status { get; }

        protected Vendor Vendor{ get { return _vendor; } }
        protected int ClientId { get { return _clientId; } }
        public Guid OrderId { get { return _id; } }

        public IWorkflowOrder ProcessNextStep()
        {
            // variation of the template method pattern
            // http://www.dofactory.com/net/template-method-design-pattern
            var next = this.MakeTransition();
            next.Save();
            return next;
        }
        
        public void AssignVendor(Vendor vendor)
        {
            if (this.Status != OrderStatus.Unassigned && this.Status != OrderStatus.ManualAssign)
            {
                throw new Exception("Order is not in correct state to be Assigned.");
            }

            // some additional business logic if required
            _vendor = vendor;
        }

        protected bool AcceptSubmittedReport()
        {
            if (this.Status != OrderStatus.ReviewSubmission)
            {
                throw new Exception("Order is not in correct state to have report Submitted.");
            }

            // randomly return false to simulate rejection
            return Randomizer.RandomYes();
        }

        protected Func<OrderWorkflowDto> MapToOrderWorkflowDto()
        {
            return () => new OrderWorkflowDto
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
