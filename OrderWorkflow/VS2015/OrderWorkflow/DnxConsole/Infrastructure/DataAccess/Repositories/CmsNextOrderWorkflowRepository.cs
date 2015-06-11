using System;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.OrderWorkflowContext;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Infrastructure.DataAccess.DataTransferObjects;
using Order = DnxConsole.Domain.OrderEditContext.Order;

namespace DnxConsole.Infrastructure.DataAccess.Repositories
{
    public class CmsNextOrderWorkflowRepository : IOrderRepository, IOrderContextRepository
    {
        public void PersistThisUpdatedDataSomeWhere(Func<OrderWorkflowDto> orderWorkflowDtoFunc)
        {
            UpdateOrderFromOrderWorkFlowDto(orderWorkflowDtoFunc());
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkBlue, ConsoleColor.White,
             $"CmsNext Repo persisting order {orderWorkflowDtoFunc().OrderId} data somewhere and doing it faster");
        }

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

        private static void UpdateOrderFromOrderWorkFlowDto(OrderWorkflowDto orderWorkflowDto)
        {
            var order = GetOrder(orderWorkflowDto.OrderId);
            order.Status = orderWorkflowDto.Status;
            order.VendorPk = orderWorkflowDto.Vendor?.ClientUserId;
            // make a call to the DB persisting
        }

        private static CmsNextOrderDto GetOrder(Guid orderId)
        {
            // here is where we could query the DB to get the row where we persist our data about this order.
            return new CmsNextOrderDto
            {
                OrderId = orderId,
                VendorPk = "1234",
                CmsId = 1,
                ZipCode = "38655"
            };
        }
    }
}
