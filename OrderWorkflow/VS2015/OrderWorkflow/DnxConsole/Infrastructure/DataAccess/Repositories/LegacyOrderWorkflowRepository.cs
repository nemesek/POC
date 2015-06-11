using System;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.OrderWorkflowContext;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Infrastructure.DataAccess.Repositories
{
    public class LegacyOrderWorkflowRepository : IOrderRepository
    {
        public void PersistThisUpdatedDataSomeWhere(Func<OrderWorkflowDto> orderWorkflowDtoFunc)
        {
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkGreen, ConsoleColor.White,
                $"Legacy Repo calling a stored proc somewhere to persist order {orderWorkflowDtoFunc().OrderId} and mapping order Id to document_Id");
        }
    }
}
