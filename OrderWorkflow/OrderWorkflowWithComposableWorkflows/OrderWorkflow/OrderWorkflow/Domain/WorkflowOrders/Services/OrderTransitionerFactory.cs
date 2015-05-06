using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class OrderTransitionerFactory
    {
        public static OrderTransitioner GetTransitionLogic(int clientId,int serviceId, Func<ICanBeAutoAssigned, Vendor> safeAssign)
        {
            if (serviceId == 2)
            {
                Console.WriteLine("$$$$Applying custom state machine for serviceId 2$$$$");
                return new CustomOrdertransitioner(safeAssign);
            }

            if (serviceId > 1 && clientId == 30)
            {
                Console.WriteLine("%%%%%%Applying John custom state machine for clientId 30 and service 2 and 3");
                return new JohnsCustomTransitioner(safeAssign);
            }
            if (clientId % 21 == 0 || clientId % 14 == 0 || clientId % 24 == 0) return new JohnsCustomTransitioner(safeAssign);
            if (clientId%5 == 0) return new CustomOrdertransitioner(safeAssign);
            
            return new OrderTransitioner(safeAssign);
        }
    }
}
