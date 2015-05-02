namespace OrderWorkflow.Domain
{
    public class OrderProcessor
    {
        public IOrder ProcessNextStep(IOrder order)
        {
            var processedOrder = order.MakeTransition();
            return processedOrder;
        }
    }
}
