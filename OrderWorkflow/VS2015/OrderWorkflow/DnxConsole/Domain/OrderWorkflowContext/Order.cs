using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.Events;
using DnxConsole.Utilities;

namespace DnxConsole.Domain.OrderWorkflowContext
{
    public abstract class Order : IWorkflowOrder
    {
        private readonly Guid _id;
        private readonly Cms _cms;
        private Vendor _vendor;

        protected Order(Guid id, OrderWorkflowDto orderWorkflowDto)
        {
            _id = id;
            _cms = orderWorkflowDto.Cms;
            _vendor = orderWorkflowDto.Vendor;
        }

        // MakeTransition and Status combine for my variation of state pattern
        // http://www.dofactory.com/net/state-design-pattern
        public abstract IWorkflowOrder MakeTransition();
        public abstract OrderStatus Status { get; }

        protected Vendor Vendor => _vendor;
        protected Cms Cms => _cms;
        public Guid OrderId => _id;

        public IWorkflowOrder ProcessNextStep()
        {
            // variation of the template method pattern
            // http://www.dofactory.com/net/template-method-design-pattern
            var next = this.MakeTransition();
            next.Save();
            if (next.Status == OrderStatus.Closed) DomainEvents.Raise<OrderClosedEvent>(new OrderClosedEvent {Order = this});
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
                Cms = _cms,
                Vendor = _vendor
            };
        }

        public void Save()
        {
            Console.ResetColor();
            Console.WriteLine("Saving {0} State to DB", Status);
        }
    }
}
