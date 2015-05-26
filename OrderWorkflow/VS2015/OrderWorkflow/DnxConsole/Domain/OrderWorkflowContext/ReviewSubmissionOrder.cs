using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext
{
    public class ReviewSubmissionOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public ReviewSubmissionOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }
        
        public override OrderStatus Status => OrderStatus.ReviewSubmission;

        protected override IWorkflowOrder MakeTransition()
        {
            if (base.AcceptSubmittedReport()) return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), true);
            Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage!!!!!!!!!!!!!!!!");
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), false);
        }
    }
}
