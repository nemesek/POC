using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class CustomRejectedOrder : RejectedOrder
    {
        public CustomRejectedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto) {}

        public override IWorkflowOrder MakeTransition()
        {
            Console.WriteLine("**************Applying custom rejected order business logic******************");
            return base.MakeTransition();
        }
    }
}
