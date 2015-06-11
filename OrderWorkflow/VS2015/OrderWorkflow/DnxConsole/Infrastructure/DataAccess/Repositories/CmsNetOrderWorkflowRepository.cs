using System;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.OrderWorkflowContext;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Infrastructure.DataAccess.DataTransferObjects;

namespace DnxConsole.Infrastructure.DataAccess.Repositories
{
    public class CmsNetOrderWorkflowRepository : IOrderRepository
    {
        public void PersistThisUpdatedDataSomeWhere(Func<OrderWorkflowDto> orderWorkflowDtoFunc)
        {
            UpdateOrderFromOrderWorkFlowDto(orderWorkflowDtoFunc());
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkMagenta, ConsoleColor.White, 
                $"CmsNet Repo persisting order {orderWorkflowDtoFunc().OrderId} data somewhere and mapping order Id to DocId");
        }

        private static void UpdateOrderFromOrderWorkFlowDto(OrderWorkflowDto orderWorkflowDto)
        {
            var order = GetOrder(orderWorkflowDto.OrderId);
            order.Status = orderWorkflowDto.Status;
            order.VendorPk = orderWorkflowDto.Vendor?.ClientUserId;
            // make a call to the DB persisting
        }

        private static CmsNetOrderDto GetOrder(Guid orderId)
        {
            // here is where we could query the DB to get the row where we persist our data about this order.
            return new CmsNetOrderDto
            {
                DocId= orderId,
                VendorPk = "1234",
                CmsId = 1,
                ZipCode = "38655"
            };
        }

    }
}
