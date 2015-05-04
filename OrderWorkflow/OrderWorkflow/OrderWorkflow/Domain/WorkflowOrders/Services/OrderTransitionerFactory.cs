namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class OrderTransitionerFactory
    {
        public static OrderTransitioner GetTransitionLogic(int id)
        {
            if (id % 21 == 0 || id % 14 == 0 || id % 24 == 0) return new JohnsCustomTransitioner();
            if (id%5 == 0) return new CustomOrdertransitioner();
            
            return new OrderTransitioner();
            //return id%5 == 0 ? new CustomOrdertransitioner() : new OrderTransitioner();
        }
    }
}
