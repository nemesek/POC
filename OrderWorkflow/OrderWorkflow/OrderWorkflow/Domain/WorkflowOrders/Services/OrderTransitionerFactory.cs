namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class OrderTransitionerFactory
    {
        public static OrderTransitioner GetTransitionLogic(int id)
        {
            return id%5 == 0 ? new CustomOrdertransitioner() : new OrderTransitioner();
        }
    }
}
