using System;
using System.Threading.Tasks;
using DnxConsole.Domain.Common.Utilities;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.Events;

namespace DnxConsole.Infrastructure.Services
{
    public class EventLogger : ILogEvents
    {
        public async Task<bool> LogOrderCreationAsync(OrderCreatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkYellow, ConsoleColor.White, BuildLogMessage("created", evt.Order.Id));
            return true;

        }

        public async Task<bool> LogOrderUpdatedAsync(OrderUpdatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkRed, ConsoleColor.White, BuildLogMessage("updated", evt.Order.Id));
            return true;
        }

        public async Task<bool> LogOrderClosedAsync(OrderClosedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkYellow, ConsoleColor.White, BuildLogMessage("closed", evt.Order.OrderId));
            return true;
        }

        private static string BuildLogMessage(string action, Guid id)
        {
            return $"Logging {action} event for order Id {id}";
        }
    }
}
