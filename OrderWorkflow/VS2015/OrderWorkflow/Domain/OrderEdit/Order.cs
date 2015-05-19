using System;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.OrderEdit
{
    public class Order
    {
        private Address _address = new Address("Oxford", "MS", "38655", "Lamar", "123");
        private int _cmsId;
        private Guid _id;
        
        public Order(Guid orderId, int cmsId)
        {
            _id = orderId;
            _cmsId = cmsId;
        }
        
        
        public Guid Id {get {return _id;}}
        public int ClientId {get {return _cmsId;}}
        public Address GetAddress()
        {
            return _address;                                         
        }
        
        public void UpdateAddress(Address address)
        {
            if(address == null)throw new ArgumentNullException("address");
            
            _address = address;
            this.Save();
            
        }
        
        private void Save()
        {
            Console.WriteLine("Saving edited order.");
        }
    }
}
