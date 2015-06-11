using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Common.Events;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext
{
    public abstract class Order : IWorkflowOrder
    {
        private readonly Guid _id;
        private readonly Cms _cms;
        private Vendor _vendor;
        private readonly IOrderRepository _repository;

        protected Order(Guid id, OrderWorkflowDto orderWorkflowDto, IOrderRepository repository)
        {
            _id = id;
            _cms = orderWorkflowDto.Cms;
            _vendor = orderWorkflowDto.Vendor;
            _repository = repository;
        }

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
            if (next.Status == OrderStatus.Closed) DomainEvents.Publish(new OrderClosedEvent {Order = this});
            return next;
        }
        
        public void Save()
        {
            Console.ResetColor();
            this.Validate();
            Console.WriteLine("Saving {0} State to repository", Status);
            _repository.PersistThisUpdatedDataSomeWhere(this.MapToOrderWorkflowDto());
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
        
        // MakeTransition and Status combine for my variation of state pattern
        // http://www.dofactory.com/net/state-design-pattern
        protected abstract IWorkflowOrder MakeTransition();

        protected Func<OrderWorkflowDto> MapToOrderWorkflowDto()
        {
            return () => new OrderWorkflowDto
            {
                OrderId = _id,
                Cms = _cms,
                Vendor = _vendor
            };
        }

        private void Validate()
        {
        }
    }
}
