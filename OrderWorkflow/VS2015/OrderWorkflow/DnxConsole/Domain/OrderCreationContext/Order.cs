using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Events;

namespace DnxConsole.Domain.OrderCreationContext
{
    public class Order
    {
		private readonly int _cmsId;
        private readonly OrderStatus _status;

        private readonly Guid _id;
        private Address _address;
        public Order(int cmsId)
        {
            _cmsId = cmsId;
            _status = OrderStatus.Unassigned;
            _id = Guid.NewGuid();
        }

        public Guid Id => _id;

        public Order Create(Address address)
        {
            _address = address;
            this.Validate();
            this.Save();
            return this;
        }

        private void Save()
        {
            Console.WriteLine("Saving orderId, cmsId, address, and status to DB.");
            DomainEvents.Publish(new OrderCreatedEvent { Order = this });
        }
        
        private void Validate()
        {
            // doing some business validation
        }
    }

}