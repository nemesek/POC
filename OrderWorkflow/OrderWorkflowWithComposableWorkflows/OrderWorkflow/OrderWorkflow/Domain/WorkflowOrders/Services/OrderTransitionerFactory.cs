using System;
using System.Collections.Generic;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class OrderTransitionerFactory
    {
        private static readonly Dictionary<TransitionType, Func<Func<ICanBeAutoAssigned, Vendor>, OrderTransitioner>>
            FuncDictionary = new Dictionary<TransitionType, Func<Func<ICanBeAutoAssigned, Vendor>, OrderTransitioner>>
            {
                {TransitionType.Default, (sa) => new OrderTransitioner(sa)},
                {TransitionType.Custom, (sa) => new CustomOrdertransitioner(sa)},
                {TransitionType.JohnCustom, (sa) => new JohnsCustomTransitioner(sa)}
            };

        public static OrderTransitioner GetTransitionLogic(int clientId,int serviceId, Func<ICanBeAutoAssigned, Vendor> safeAssign)
        {
            // Note that we would never really use the mod of the clientId to determine what we are really going to return
            // as it could cause conflicts with other functions that determine which state machine to return
            // this is solely to demo the dynamic nature of the application
            var serviceFilter = ServiceFilter(serviceId, clientId);
            if (serviceFilter.Item1)
            {
                return FuncDictionary[serviceFilter.Item2].Invoke(safeAssign);
            }

            var clientFilter = ClientFilter(clientId);
            return clientFilter.Item1 ? 
                FuncDictionary[clientFilter.Item2].Invoke(safeAssign) 
                : new OrderTransitioner(safeAssign);
        }


        private static Tuple<bool, TransitionType> ServiceFilter(int serviceId, int clientId)
        {
            if (serviceId == 2)
            {
                Console.WriteLine("$$$$Applying custom state machine for serviceId 2$$$$");
                return new Tuple<bool, TransitionType>(true, TransitionType.Custom);
            }

            if (serviceId <= 1 || clientId != 30) return new Tuple<bool, TransitionType>(false, TransitionType.Undefined);

            Console.WriteLine("%%%%%%Applying John custom state machine for clientId 30 and service 2 and 3");
            return new Tuple<bool, TransitionType>(true, TransitionType.JohnCustom);
        }

        private static Tuple<bool, TransitionType> ClientFilter(int clientId)
        {
            if (clientId%21 == 0 || clientId%14 == 0 || clientId%24 == 0) return new Tuple<bool, TransitionType>(true, TransitionType.JohnCustom);

            return clientId % 5 == 0 ?
                new Tuple<bool,TransitionType>(true,TransitionType.Custom) 
                : new Tuple<bool, TransitionType>(false, TransitionType.Undefined);
        }

        private enum TransitionType
        {
            Undefined = 0,
            Default = 1,
            Custom = 2,
            JohnCustom = 3
        };
    }
}
