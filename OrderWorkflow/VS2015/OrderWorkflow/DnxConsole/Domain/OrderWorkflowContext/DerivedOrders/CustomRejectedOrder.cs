using System;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.DerivedOrders
{
    public class CustomRejectedOrder : RejectedOrder
    {
        public CustomRejectedOrder(Guid id, OrderWorkflowDto orderWorkflowDto, IOrderRepository repository):base(id,orderWorkflowDto, repository) {}

        protected override IWorkflowOrder MakeTransition()
        {
            const string output =
                "**************Applying custom rejected order business logic implementation details******************";
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkYellow, ConsoleColor.White, output);
            return base.MakeTransition();
        }
    }
}
