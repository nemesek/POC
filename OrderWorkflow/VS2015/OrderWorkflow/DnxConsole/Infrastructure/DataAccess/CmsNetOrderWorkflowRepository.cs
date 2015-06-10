using System;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.OrderWorkflowContext;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Infrastructure.DataAccess
{
    public class CmsNetOrderWorkflowRepository : IOrderRepository
    {
        public void PersistThisUpdatedDataSomeWhere(Func<OrderWorkflowDto> orderWorkflowDtoFunc)
        {
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkMagenta, ConsoleColor.White, 
                $"CmsNet Repo persisting order {orderWorkflowDtoFunc().OrderId} data somewhere and mapping order Id to DocId");
        }

    }
}
