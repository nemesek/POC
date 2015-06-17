using System;
using System.Collections.Generic;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.Services
{
    public class StateMachineFactory
    {
        private readonly WorkflowOrderFactory _orderFactory;

        private Dictionary<TransitionType, Func<Func<ICanBeAutoAssigned, Vendor>, StateMachine>> _customTransitionsMap;

        public StateMachineFactory(WorkflowOrderFactory orderFactory)
        {
            _orderFactory = orderFactory;
            InitializeTransitionMap();
        }
        public StateMachine GetTransitionLogic(int clientId,int serviceId, Func<ICanBeAutoAssigned, Vendor> safeAssign)
        {
            // Note that we would never really use the mod of the clientId to determine what we are really going to return
            // as it could cause conflicts with other functions that determine which state machine to return
            // this is solely to demo the dynamic nature of the application
            var serviceTransitionType = ServiceClientFilter(serviceId, clientId);
            if (serviceTransitionType != TransitionType.Undefined)
            {
                return _customTransitionsMap[serviceTransitionType].Invoke(safeAssign);
            }

            var clientTransitionType = ClientFilter(clientId);
            return clientTransitionType != TransitionType.Undefined
                ? _customTransitionsMap[clientTransitionType].Invoke(safeAssign)
                : new StateMachine(safeAssign, _orderFactory);
        }


        private static TransitionType ServiceClientFilter(int serviceId, int clientId)
        {
            if (serviceId == 2)
            {
                const string output = "$$$$Applying custom state machine implementation details for serviceId 2$$$$";
                ConsoleHelper.WriteWithStyle(ConsoleColor.Magenta, ConsoleColor.White, output);
                return TransitionType.Custom;
            }

            if (serviceId <= 1 || clientId != 30) return TransitionType.Undefined;
    	   
            const string output30 = "%%%%%%Applying John custom state machine implementation details for clientId 30 and service 2 and 3";
            ConsoleHelper.WriteWithStyle(ConsoleColor.Magenta, ConsoleColor.White, output30);
            return TransitionType.JohnCustom;
        }

        private static TransitionType ClientFilter(int clientId)
        {
            if (clientId%21 == 0 || clientId%14 == 0 || clientId%24 == 0)
            {
                ConsoleHelper.WriteWithStyle(ConsoleColor.Magenta, ConsoleColor.White,
                    $"$$$$Applying John custom state machine implementation details for clientId {clientId}$$$$");
                return TransitionType.JohnCustom;
            }

            if (clientId%5 == 0)
            {
                ConsoleHelper.WriteWithStyle(ConsoleColor.Magenta, ConsoleColor.White,
                    $"$$$$Applying custom state machine implementation details for clientId {clientId}$$$$");
                return TransitionType.Custom;
            }

            return TransitionType.Undefined;
        }

        private void InitializeTransitionMap()
        {
            _customTransitionsMap = new Dictionary<TransitionType, Func<Func<ICanBeAutoAssigned, Vendor>, StateMachine>>
            {
                {TransitionType.Custom, (sa) => new CustomStateMachine(sa, _orderFactory)},
                {TransitionType.JohnCustom, (sa) => new JohnsStateMachine(sa, _orderFactory)}
            };
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
