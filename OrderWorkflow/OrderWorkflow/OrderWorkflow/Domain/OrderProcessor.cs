using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain
{
    public class OrderProcessor
    {
        public IOrder ProcessNextStep(Func<IOrder> orderTransitionFunc)
        {
            //var processedOrder = order.MakeTransition();
            var processedOrder = orderTransitionFunc();
            return processedOrder;
        }
    }
}
