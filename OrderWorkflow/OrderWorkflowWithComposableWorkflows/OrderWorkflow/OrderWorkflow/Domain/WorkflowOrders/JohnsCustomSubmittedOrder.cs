using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class JohnsCustomSubmittedOrder : SubmittedOrder
    {
        public JohnsCustomSubmittedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
        }

        public override IWorkflowOrder MakeTransition()
        {
            Console.WriteLine("Doing John Additional Order Submitted Buisness Logic");
            return base.MakeTransition();
        }
    }
}
