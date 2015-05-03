using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.WorkflowOrders;

namespace OrderWorkflow.Domain.Orders
{
    public class RejectedOrder : Order
    {
        private readonly Func<Guid, Func<OrderDto>, bool, IWorkflowOrder> _transitionFunc;

        public RejectedOrder(Guid id, OrderDto orderDto) : base(id, orderDto)
        {
            _transitionFunc = orderDto.ConditionalTransitionFunc;
        }

        public override IWorkflowOrder MakeTransition()
        {
            if (base.AcceptSubmittedReport()) return _transitionFunc(base.OrderId, base.MapToOrderDto(), true);
            Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage!!!!!!!!!!!!!!!!");
            return _transitionFunc(base.OrderId, base.MapToOrderDto(), false);
        }

        public override OrderStatus Status
        {
            get { return OrderStatus.Rejected; }
        }
    }
}
