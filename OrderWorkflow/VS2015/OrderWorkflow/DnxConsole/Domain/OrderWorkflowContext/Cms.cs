using System;
using DnxConsole.Domain.Common.Contracts;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.OrderWorkflowContext.AutoAssign;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Services;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext
{
    public class Cms : Common.Cms
    {
        private readonly StateMachineFactory _transitionFactory;
        private readonly AutoAssignFactory _autoAssignFactory;

        public Cms(int id, 
            ILogEvents logger,
            ISendExternalMessenges messenger,
            StateMachineFactory transitionFactory,
            AutoAssignFactory autoAssignFactory) : base(id, logger, messenger)
        {
            _transitionFactory = transitionFactory;
            _autoAssignFactory = autoAssignFactory;
        }

        public IWorkflowOrder GetWorkflowOrder()
        {
            var serviceId = GetServiceId();
            // calling into a factory to get cms/service specific transitions
            var stateMachine = _transitionFactory.GetTransitionLogic(base.Id, serviceId, _ => null);
            // injecting in AA logic to statemachine - Strategy Pattern
            // http://www.dofactory.com/net/strategy-design-pattern
            var order = stateMachine.GetUnassignedOrder(FindBestVendor(), this);
            order.Save();
            return order;
        }

        public Func<ICanBeAutoAssigned, Vendor> FindBestVendor()
        {
            // calling into a factory to get cms specific auto assign
            var autoAssign = _autoAssignFactory.CreateAutoAssign(base.Id);
            return autoAssign.FindBestVendor;
        }

        public bool ManualAssign(IWorkflowOrder order)
        {
            // randomly return false to simulate rejection
            if (!Randomizer.RandomYes()) return false;
            var vendor = Randomizer.RandomYes()
                ? new Vendor(0, "38655", "Daniel Garrett")
                : new Vendor(0, "38655", "Dwain Richardson");
            order.AssignVendor(vendor);
            return true;
        }

        public bool ReviewAcceptance(IWorkflowOrder order)
        {
            return Randomizer.RandomYes();
        }


    }
}
