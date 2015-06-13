using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Common.Events;
using DnxConsole.Domain.Common.Utilities;

namespace DnxConsole.Domain.OrderCreationContext
{
    public class Order
    {
		private readonly int _cmsId;
        private readonly OrderStatus _status;

        private readonly Guid _id;
        private Address _address;
        private readonly IOrderCreationRepository _repository;
        private readonly int _serviceId;

        public Order(int cmsId, IOrderCreationRepository repository, int serviceId)
        {
            _cmsId = cmsId;
            _serviceId = serviceId;
            _status = OrderStatus.Unassigned;
            _id = Guid.NewGuid();
            _repository = repository;
        }

        public Guid Id => _id;
        public int ServiceId => _serviceId;

        public Order Create(Address address)
        {
            _address = address;
            this.Validate();
            this.Save();
            return this;
        }

        private void Save()
        {
            _repository.CreateOrder(this);
            DomainEvents.Publish(new OrderCreatedEvent { Order = this });
        }
        
        private void Validate()
        {
            ConsoleHelper.WriteWithStyle(ConsoleColor.Blue, ConsoleColor.White,
                "Validating order data within order object");
        }
    }

}