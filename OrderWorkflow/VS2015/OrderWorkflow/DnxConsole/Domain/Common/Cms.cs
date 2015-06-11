using DnxConsole.Domain.Common.Contracts;
using DnxConsole.Domain.Common.Events;

namespace DnxConsole.Domain.Common
{
    public abstract class Cms
    {
        private readonly int _id;
        private readonly ILogEvents _logger;
        private readonly ISendExternalMessenges _messenger;

        protected Cms(int id, ILogEvents logger,ISendExternalMessenges messenger)
        {
            _id = id;
            _logger = logger;
            _messenger = messenger;
            RegisterEventHandlers();
        }

        public int Id => _id;

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
