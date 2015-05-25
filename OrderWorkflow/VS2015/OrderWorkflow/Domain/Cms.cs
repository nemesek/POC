using System;
using System.Threading.Tasks;
using OrderWorkflow.Domain.AutoAssign;
using OrderWorkflow.Domain.Common;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Events;
using OrderWorkflow.Domain.OrderEdit;
using OrderWorkflow.Domain.WorkflowOrders.Services;

namespace OrderWorkflow.Domain
{
    public class Cms
    {
        private readonly int _id;

        public Cms(int id)
        {
            _id = id;
            RegisterEventHandlers();
        }

        public int Id => _id;

        public IWorkflowOrder GetWorkflowOrder()
        {
            var serviceId = GetServiceId();
            // calling into a factory to get cms/service specific transitions
            var stateMachine = OrderTransitionerFactory.GetTransitionLogic(_id, serviceId, _ => null);
            // injecting in AA logic to statemachine - Strategy Pattern
            // http://www.dofactory.com/net/strategy-design-pattern
            var order =  stateMachine.GetUnassignedOrder(FindBestVendor(), this);
            order.Save();
            return order;
        }

        public IWorkflowOrder GetWorkflowOrder(Guid orderId, int serviceId)
        {
            // calling into a factory to get cms/service specific transitions
            var stateMachine = OrderTransitionerFactory.GetTransitionLogic(_id, serviceId, _ => null);
            // injecting in AA logic to statemachine - Strategy Pattern
            // http://www.dofactory.com/net/strategy-design-pattern
            var order = stateMachine.GetUnassignedOrder(FindBestVendor(), this);
            order.Save();
            return order;
        }

        public void EditOrderAddress(Address newAddress)
        {
            var order = OrderEditRepository.GetOrder(_id);
            order.UpdateAddress(newAddress);
            //DomainEvents.ClearCallbacks();
        }
        
        public OrderCreation.Order CreateOrder()
        {
            var order = new OrderCreation.Order(_id);
            var address = new Address("Dallas", "TX", "75019", "Elm", "456");
            order.Create(address);
            Console.WriteLine("Order Created from CMS");
            //DomainEvents.ClearCallbacks();
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

        private static void RegisterEventHandlers()
        {
            // order creation handlers
            DomainEvents.Register<OrderCreatedEvent>(async _ => await LogOrderCreationAsync());
            DomainEvents.Register<OrderCreatedEvent>(async e => await SendOrderCreationNotificationAsync(e));
            DomainEvents.Register<OrderCreatedEvent>(async e => await SendOrderToWorkflowQueueAsync(e));

            // order update handlers
            DomainEvents.Register<OrderUpdatedEvent>(async e => await LogOrderUpdateAsync(e));

            // order closed handlers
            DomainEvents.Register<OrderClosedEvent>(async e => await SendOrderToBillingSystem(e));
            DomainEvents.Register<OrderClosedEvent>(async e => await SendOrderClosedNotificationAsync(e));
        }

        private static async Task<bool> LogOrderCreationAsync()
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkYellow, ConsoleColor.White, "Logging Order Creationl");
            await Task.Delay(1000);
            return true;
        }

        private static async Task<bool> SendOrderCreationNotificationAsync(OrderCreatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.Blue, ConsoleColor.White, $"Sending email notification for Order {evt.Order.Id}");
            return true;
        }

        private static async Task<bool> SendOrderToWorkflowQueueAsync(OrderCreatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.Green, ConsoleColor.White, $"Queueing up Order {evt.Order.Id} to workflow context");
            return true;
        }

        private static async Task<bool> LogOrderUpdateAsync(OrderUpdatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkRed, ConsoleColor.White, $"Order {evt.Order.Id} updated");
            return true;
        }

        private static async Task<bool> SendOrderClosedNotificationAsync(OrderClosedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.Yellow, ConsoleColor.White, $"Sending Order Closed notifcation for {evt.Order.OrderId}");
            return true;
        }

        private static async Task<bool> SendOrderToBillingSystem(OrderClosedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkCyan, ConsoleColor.White, $"Sending Order {evt.Order.OrderId} to billing system.");
            return true;
        }
    }
}
