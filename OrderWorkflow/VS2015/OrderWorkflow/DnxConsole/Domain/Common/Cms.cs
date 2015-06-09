using System;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.Events;
using DnxConsole.Domain.OrderEditContext;
using DnxConsole.Domain.OrderWorkflowContext.AutoAssign;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Services;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;
using DnxConsole.Infrastructure.Utilities;
using Order = DnxConsole.Domain.OrderCreationContext.Order;

namespace DnxConsole.Domain.Common
{
    public class Cms
    {
        private readonly int _id;
        private readonly ILogEvents _logger;
        private readonly ISendExternalMessenges _messenger;

        public Cms(int id, ILogEvents logger, ISendExternalMessenges messenger)
        {
            _id = id;
            _logger = logger;
            _messenger = messenger;
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

        public void EditOrderAddress(Address newAddress)
        {
            var order = OrderEditRepository.GetOrder(_id);
            order.UpdateAddress(newAddress);
        }
        
        public Order CreateOrder()
        {
            var order = new Order(_id);
            var address = new Address("Dallas", "TX", "75019", "Elm", "456");
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

        private void RegisterEventHandlers()
        {
            // in the spirit of the observer pattern
            // http://www.dofactory.com/net/observer-design-pattern
            // order creation handlers
            DomainEvents.SubscribeTo<OrderCreatedEvent>(async e => await _logger.LogOrderCreationAsync(e));
            DomainEvents.SubscribeTo<OrderCreatedEvent>(async e => await _messenger.SendOrderCreationNotificationAsync(e));
            DomainEvents.SubscribeTo<OrderCreatedEvent>(async e => await _messenger.SendToWorkflowQueue(e));

            // order update handlers
            DomainEvents.SubscribeTo<OrderUpdatedEvent>(async e => await _logger.LogOrderUpdatedAsync(e));

            // order closed handlers
            DomainEvents.SubscribeTo<OrderClosedEvent>(async e => await _messenger.SendToBillingSystem(e));
            DomainEvents.SubscribeTo<OrderClosedEvent>(async e => await _logger.LogOrderClosedAsync(e));
        }
    }
}
