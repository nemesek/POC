using System;

namespace DnxConsole.Domain.OrderEditContext
{
	public class OrderEditRepository
	{
	
		public static Order GetOrder(int cmsId)
		{
			// todo : bring in the orderId
			return new Order(Guid.NewGuid(), cmsId);
		}
		
		public static Order CreateOrder(int cmsId)
		{
			var order = new Order(Guid.NewGuid(), cmsId);
			Console.WriteLine("Created order with Id {0}", order.Id);
			return order;
		}
	}
}