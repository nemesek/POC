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
            Console.Write("Doing More Buisness Logic");
            if (base.AcceptSubmittedReport()) return base.MakeTransition();
            Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage because I am John!!!!!!!!!!!!!!!!");
            
            return base.MakeTransition();
        }
    }
}
