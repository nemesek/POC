using System;
using DnxConsole.Domain.Contracts;
using DnxConsole.Utilities;

namespace DnxConsole.Domain.OrderWorkflowContext.DerivedOrders
{
    public class JohnsCustomSubmittedOrder : SubmittedOrder
    {
        public JohnsCustomSubmittedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto) {}

        public override IWorkflowOrder MakeTransition()
        {
            const string output = "Doing John Additional Order Submitted Buisness Logic implementation details";
            ConsoleHelper.WriteWithStyle(ConsoleColor.Magenta, ConsoleColor.White, output);
            return base.MakeTransition();
        }
    }
}
