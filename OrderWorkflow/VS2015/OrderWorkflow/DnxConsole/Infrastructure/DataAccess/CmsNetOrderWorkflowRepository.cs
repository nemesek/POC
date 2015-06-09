using System;
using DnxConsole.Domain.OrderWorkflowContext;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Infrastructure.Utilities;

namespace DnxConsole.Infrastructure.DataAccess
{
    public class CmsNetOrderWorkflowRepository : IOrderRepository
    {
        public void PersistThisUpdatedDataSomeWhere(OrderWorkflowDto orderWorkflowDto)
        {
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkMagenta, ConsoleColor.DarkGray, 
                $"CmsNet Repo persisting order {orderWorkflowDto.OrderId} data somewhere and mapping order Id to DocId");
        }
    }
}
