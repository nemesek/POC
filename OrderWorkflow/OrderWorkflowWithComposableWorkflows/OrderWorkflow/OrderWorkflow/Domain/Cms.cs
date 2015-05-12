using System;
using OrderWorkflow.Domain.AutoAssign;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.WorkflowOrders.Services;

namespace OrderWorkflow.Domain
{
    public class Cms
    {
        private readonly int _id;

        public Cms(int id)
        {
            _id = id;
            
        }


        public IWorkflowOrder CreateNewOrder()
        {
            var serviceId = GetServiceId();
            // calling into a factory to get cms/service specific transitions
            var stateMachine = OrderTransitionerFactory.GetTransitionLogic(_id, serviceId, _ => null);
            // injecting in AA logic to statemachine - Strategy Pattern
            // http://www.dofactory.com/net/strategy-design-pattern
            return stateMachine.CreateNewOrder(FindBestVendor(), _id);
            
        }

        public Func<ICanBeAutoAssigned, Vendor> FindBestVendor()
        {
            // calling into a factory to get cms specific auto assign
            var autoAssign = AutoAssignFactory.CreateAutoAssign(_id);
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
            if (_id > 24) serviceId = Randomizer.RandomYes() ? 2 : 3;
            return serviceId;
        }
    }
}
