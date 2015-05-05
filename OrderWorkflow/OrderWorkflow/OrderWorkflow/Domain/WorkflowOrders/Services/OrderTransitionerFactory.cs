using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class OrderTransitionerFactory
    {
        public static OrderTransitioner GetTransitionLogic(int id, Func<ICanBeAutoAssigned, Vendor> safeAssign)
        {
            if (id % 21 == 0 || id % 14 == 0 || id % 24 == 0) return new JohnsCustomTransitioner(safeAssign);
            if (id%5 == 0) return new CustomOrdertransitioner(safeAssign);
            
            return new OrderTransitioner(safeAssign);
        }
    }
}
