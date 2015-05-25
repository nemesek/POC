using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class ReviewSubmissionOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public ReviewSubmissionOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }
        
        public override OrderStatus Status => OrderStatus.ReviewSubmission;

        public override IWorkflowOrder MakeTransition()
        {
            if (base.AcceptSubmittedReport()) return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), true);
            Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage!!!!!!!!!!!!!!!!");
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), false);
        }

        

    }
}
