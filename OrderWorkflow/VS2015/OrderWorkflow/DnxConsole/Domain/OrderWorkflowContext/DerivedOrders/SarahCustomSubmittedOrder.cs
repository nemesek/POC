using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.OrderStates;

namespace DnxConsole.Domain.OrderWorkflowContext.DerivedOrders
{
    public class SarahCustomSubmittedOrder : SubmittedOrder
    {
        public SarahCustomSubmittedOrder(Guid id, OrderWorkflowDto orderWorkflowDto, IOrderRepository repository) : base(id, orderWorkflowDto, repository)
        {
        }

        protected override IWorkflowOrder MakeTransition()
        {
            const string output = "Doing Sarah Custom Additional Order Submitted Buisness Logic implementation details";
            ConsoleHelper.WriteWithStyle(ConsoleColor.Magenta, ConsoleColor.White, output);
            return base.MakeTransition();
        }
    }
}
