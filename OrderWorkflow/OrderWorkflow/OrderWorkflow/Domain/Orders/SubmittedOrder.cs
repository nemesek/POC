using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class SubmittedOrder : Order
    {
        private readonly Func<Guid, Func<OrderDto>, bool, IWorkflowOrder> _transitionFunc;

        public SubmittedOrder(Guid id, OrderDto orderDto) : base(id, orderDto)
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
            get { return OrderStatus.Submitted; }
        }
    }
}
