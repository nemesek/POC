using System;
using DnxConsole.Domain.OrderWorkflowContext;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Infrastructure.Utilities;

namespace DnxConsole.Infrastructure.DataAccess
{
    public class CmsNextOrderWorkflowRepository : IOrderRepository
    {
        public void PersistThisUpdatedDataSomeWhere(Func<OrderWorkflowDto> orderWorkflowDtoFunc)
        {
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkBlue, ConsoleColor.White,
             $"CmsNext Repo persisting order {orderWorkflowDtoFunc().OrderId} data somewhere and doing it faster");
        }
    }
}
