using System;
using DnxConsole.Domain.OrderCreationContext;
using DnxConsole.Domain.OrderEditContext;
using Order = DnxConsole.Domain.OrderEditContext.Order;

namespace DnxConsole.Infrastructure.DataAccess.Repositories
{
	public class OrderRepository : IOrderEditRepository, IOrderCreationRepository
	{
		public Order GetOrder(int cmsId)
		{
			// todo : bring in the orderId
			return new Order(Guid.NewGuid(), cmsId);
		}

	    public void CreateOrder(Domain.OrderCreationContext.Order order)
	    {
            Console.WriteLine("Created order with Id {0}", order.Id);
        }
	}
}