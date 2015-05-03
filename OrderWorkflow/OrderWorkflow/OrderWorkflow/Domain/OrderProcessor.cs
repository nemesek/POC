using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain
{
    public class OrderProcessor
    {
        public IWorkflowOrder ProcessNextStep(Func<IWorkflowOrder> orderTransitionFunc)
        {
            //var processedOrder = order.MakeTransition();
            var processedOrder = orderTransitionFunc();
            return processedOrder;
        }
    }
}
