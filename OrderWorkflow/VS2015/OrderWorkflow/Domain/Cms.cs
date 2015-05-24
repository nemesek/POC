using System;
using OrderWorkflow.Domain.AutoAssign;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.WorkflowOrders.Services;
using OrderWorkflow.Domain.OrderEdit;
using OrderWorkflow.Domain.OrderCreation;
using OrderWorkflow.Domain.Events;
using System.Threading.Tasks;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain
{
    public class Cms
    {
        private readonly int _id;

        public Cms(int id)
        {
            _id = id;
        }


        public IWorkflowOrder GetWorkflowOrder()
        {
            var serviceId = GetServiceId();
            // calling into a factory to get cms/service specific transitions
            var stateMachine = OrderTransitionerFactory.GetTransitionLogic(_id, serviceId, _ => null);
            // injecting in AA logic to statemachine - Strategy Pattern
            // http://www.dofactory.com/net/strategy-design-pattern
            var order =  stateMachine.GetUnassignedOrder(FindBestVendor(), _id);
            order.Save();
            return order;
        }
        
        public OrderEdit.Order GetEditableOrder()
        {
            var order = OrderEditRepository.GetOrder(_id);
            return order;
        }
        
        public OrderCreation.Order CreateOrder()
        {
            var order = new OrderCreation.Order(_id);
            var address = new Address("Dallas", "TX", "75019", "Elm", "456");
            DomainEvents.Register<OrderCreatedEvent>(async _ => await LogOrderCreationAsync());
            DomainEvents.Register<OrderCreatedEvent>(async e => await SendOrderCreationNotificationAsync(e));
            DomainEvents.Register<OrderCreatedEvent>(async e => await SendOrderToWorkflowQueueAsync(e));
            order.Create(address);
            Console.WriteLine("Order Created from CMS");
            return order;
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
        
        private static async Task<bool> LogOrderCreationAsync()
        {
            await Task.Delay(100);
            Console.WriteLine("Logging Order Creation.");
            await Task.Delay(1000);
            return true;
        }
        
        private static async Task<bool> SendOrderCreationNotificationAsync(OrderCreatedEvent evt)
        {
            await Task.Delay(100);
            Console.WriteLine("Sending notificatin for Order {0}", evt.Order.Id);
            return true;
        }
        
        private static async Task<bool> SendOrderToWorkflowQueueAsync(OrderCreatedEvent evt)
        {
            await Task.Delay(100);
            Console.WriteLine("Queueing up Order {0} to workflow context", evt.Order.Id);
            return true;
        }
    }
}
