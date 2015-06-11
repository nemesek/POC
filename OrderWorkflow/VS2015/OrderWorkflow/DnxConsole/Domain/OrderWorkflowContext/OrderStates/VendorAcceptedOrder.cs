using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.OrderStates
{
    public class VendorAcceptedOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>,bool,IWorkflowOrder> _makeTransition;

        public VendorAcceptedOrder(Guid id,OrderWorkflowDto orderWorkflowDto, IOrderRepository repository):base(id,orderWorkflowDto, repository)
        {
            _makeTransition = orderWorkflowDto.StateTransitionFunc;
        }

        public override OrderStatus Status => OrderStatus.VendorAccepted;

        protected override IWorkflowOrder MakeTransition()
        {
            return _makeTransition(base.OrderId, base.MapToOrderWorkflowDto(), true);
        }
    }
}
