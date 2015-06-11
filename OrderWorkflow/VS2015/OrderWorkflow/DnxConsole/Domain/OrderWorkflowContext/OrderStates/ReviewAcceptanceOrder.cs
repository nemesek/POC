using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.OrderStates
{
    public class ReviewAcceptanceOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _makeTransition;

        public ReviewAcceptanceOrder(Guid id, OrderWorkflowDto orderWorkflowDto, IOrderRepository repository):base(id,orderWorkflowDto, repository)
        {
            _makeTransition = orderWorkflowDto.StateTransitionFunc;
        }
        
        public override OrderStatus Status => OrderStatus.ReviewAcceptance;

        protected override IWorkflowOrder MakeTransition()
        {
            var reviewPassed = base.Cms.ReviewAcceptance(this);
            if (!reviewPassed) Console.WriteLine("Accept With Conditions will not be met.");
            return _makeTransition(base.OrderId, base.MapToOrderWorkflowDto(), reviewPassed);
        }
    }
}
