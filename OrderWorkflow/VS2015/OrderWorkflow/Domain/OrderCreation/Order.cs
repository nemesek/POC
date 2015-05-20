using System;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.OrderCreation
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
		
		public Order Create(Address address)
		{
			_address = address;
		    this.Save();
            return this;
		}
		
		private void Save()
		{
			Console.WriteLine("Saving orderId, cmsId, address, and status to DB.");
		}
	}
}