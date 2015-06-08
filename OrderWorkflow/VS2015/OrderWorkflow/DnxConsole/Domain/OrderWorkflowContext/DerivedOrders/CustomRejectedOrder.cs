using System;
using DnxConsole.Domain.Contracts;
using DnxConsole.Infrastructure.Utilities;

namespace DnxConsole.Domain.OrderWorkflowContext.DerivedOrders
{
    public class CustomRejectedOrder : RejectedOrder
    {
        public CustomRejectedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto) {}

        protected override IWorkflowOrder MakeTransition()
        {
            const string output =
                "**************Applying custom rejected order business logic implementation details******************";
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkYellow, ConsoleColor.White, output);
            return base.MakeTransition();
        }
    }
}
