using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.DerivedOrders
{
    public class CustomRejectedOrder : RejectedOrder
    {
        public CustomRejectedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto) {}

        public override IWorkflowOrder MakeTransition()
        {
            ConsoleHelper.SetColors(ConsoleColor.DarkYellow, ConsoleColor.White);
            Console.WriteLine("**************Applying custom rejected order business logic implementation details******************");
            ConsoleHelper.ResetColors();
            return base.MakeTransition();
        }
    }
}
