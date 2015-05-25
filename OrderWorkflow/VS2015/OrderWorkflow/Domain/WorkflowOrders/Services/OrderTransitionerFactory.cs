﻿using System;
using System.Collections.Generic;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class OrderTransitionerFactory
    {
        private static readonly Dictionary<TransitionType, Func<Func<ICanBeAutoAssigned, Vendor>, OrderTransitioner>>
            CustomTransitionsDictionary = new Dictionary<TransitionType, Func<Func<ICanBeAutoAssigned, Vendor>, OrderTransitioner>>
            {
                {TransitionType.Custom, (sa) => new CustomOrdertransitioner(sa)},
                {TransitionType.JohnCustom, (sa) => new JohnsCustomTransitioner(sa)}
            };

        public static OrderTransitioner GetTransitionLogic(int clientId,int serviceId, Func<ICanBeAutoAssigned, Vendor> safeAssign)
        {
            // Note that we would never really use the mod of the clientId to determine what we are really going to return
            // as it could cause conflicts with other functions that determine which state machine to return
            // this is solely to demo the dynamic nature of the application
            var serviceTransitionType = ServiceClientFilter(serviceId, clientId);
            if (serviceTransitionType != TransitionType.Undefined)
            {
                return CustomTransitionsDictionary[serviceTransitionType].Invoke(safeAssign);
            }

            var clientTransitionType = ClientFilter(clientId);
            return clientTransitionType != TransitionType.Undefined
                ? CustomTransitionsDictionary[clientTransitionType].Invoke(safeAssign)
                : new OrderTransitioner(safeAssign);
        }


        private static TransitionType ServiceClientFilter(int serviceId, int clientId)
        {
            if (serviceId == 2)
            {
                Console.WriteLine("$$$$Applying custom state machine implementation details for serviceId 2$$$$");
                return TransitionType.Custom;
            }

            if (serviceId <= 1 || clientId != 30) return TransitionType.Undefined;

            Console.WriteLine("%%%%%%Applying John custom state machine implementation details for clientId 30 and service 2 and 3");
            return TransitionType.JohnCustom;
        }

        private static TransitionType ClientFilter(int clientId)
        {
            if (clientId%21 == 0 || clientId%14 == 0 || clientId%24 == 0)
            {
                Console.WriteLine("$$$$Applying John custom state machine implementation details for clientId {0}$$$$", clientId);
                return TransitionType.JohnCustom;
            }

            if (clientId%5 == 0)
            {
                Console.WriteLine("$$$$Applying custom state machine implementation details for clientId {0}", clientId);
                return TransitionType.Custom;
            }

            return TransitionType.Undefined;
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