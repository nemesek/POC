using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class SubmittedOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public SubmittedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }

        public override IWorkflowOrder MakeTransition()
        {
            if (base.AcceptSubmittedReport()) return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), true);
            Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage!!!!!!!!!!!!!!!!");
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), false);
        }

        public override OrderStatus Status
        {
            get { return OrderStatus.Submitted; }
        }
    }
}
