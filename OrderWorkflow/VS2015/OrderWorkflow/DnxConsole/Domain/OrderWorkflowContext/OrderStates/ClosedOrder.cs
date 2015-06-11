using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.OrderStates
{
    public class ClosedOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _makeTransition;

        public ClosedOrder(Guid id, OrderWorkflowDto orderWorkflowDto, IOrderRepository orderRepository) : base(id, orderWorkflowDto, orderRepository)
        {
            _makeTransition = orderWorkflowDto.StateTransitionFunc;
        }

        public override OrderStatus Status => OrderStatus.Closed;

        protected override IWorkflowOrder MakeTransition()
        {
            var order = _makeTransition(base.OrderId, base.MapToOrderWorkflowDto(), true);
            return order;
        }
    }
}
