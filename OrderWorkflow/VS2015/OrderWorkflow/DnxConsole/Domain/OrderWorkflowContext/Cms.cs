using System;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.AutoAssign;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Services;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext
{
    public class Cms : Common.Cms
    {
        private readonly OrderTransitionerFactory _transitionFactory;

        public Cms(int id, ILogEvents logger, ISendExternalMessenges messenger, OrderTransitionerFactory transitionFactory) : base(id, logger, messenger)
        {
            _transitionFactory = transitionFactory;
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
            var autoAssign = AutoAssignFactory.CreateAutoAssign(base.Id);
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

        private int GetServiceId()
        {
            var serviceId = 1;      // default this show cases how you can get different functionality based off of service Id
            if (base.Id > 24) serviceId = Randomizer.RandomYes() ? 2 : 3;
            return serviceId;
        }
    }
}
