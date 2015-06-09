using System;
using DnxConsole.Domain.OrderWorkflowContext;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Infrastructure.Utilities;

namespace DnxConsole.Infrastructure.DataAccess
{
    public class LegacyOrderWorkflowRepository : IOrderRepository
    {
        public void PersistThisUpdatedDataSomeWhere(OrderWorkflowDto orderWorkflowDto)
        {
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkGreen, ConsoleColor.White,
                $"Legacy Repo calling a stored proc somewhere to persist order {orderWorkflowDto.OrderId} and mapping order Id to document_Id");
        }
    }
}
