using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Common.Events;

namespace DnxConsole.Domain.OrderEditContext
{
    public class Order
    {
        private Address _address = new Address("Oxford", "MS", "38655", "Lamar", "123");
        private readonly int _cmsId;
        private readonly Guid _id;
        
        public Order(Guid orderId, int cmsId)
        {
            _id = orderId;
            _cmsId = cmsId;
        }
        
        public Guid Id => _id;
        public int ClientId => _cmsId;

        public Address GetAddress()
        {
            return _address;                                         
        }
        
        public void UpdateAddress(Address address)
        {
            if(address == null)throw new ArgumentNullException(nameof(address));
            
            _address = address;
            this.Save();
            DomainEvents.Publish(new OrderUpdatedEvent {Order = this});

        }
        
        private void Save()
        {
            Console.WriteLine("Saving edited order to DB.");
        }
    }
}
