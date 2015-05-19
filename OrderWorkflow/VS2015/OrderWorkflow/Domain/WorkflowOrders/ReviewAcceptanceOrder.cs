using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class ReviewAcceptanceOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public ReviewAcceptanceOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }

        public override IWorkflowOrder MakeTransition()
        {
            var cms = new Cms(base.ClientId);
            var reviewPassed = cms.ReviewAcceptance(this);
            if (!reviewPassed)
            {
                Console.WriteLine("Accept With Conditions will not be met.");
            }
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), reviewPassed);
        }

        public override OrderStatus Status
        {
            get { return OrderStatus.ReviewAcceptance; }
        }
    }
}
