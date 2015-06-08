using System;
using System.Threading.Tasks;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.Events;
using DnxConsole.Utilities;

namespace DnxConsole.Services
{
    public class EventLogger : ILogEvents
    {
        public async Task<bool> LogOrderCreationAsync(OrderCreatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkYellow, ConsoleColor.White, BuildLogMessage("Created", evt.Order.Id));
            return true;

        }

        public async Task<bool> LogOrderUpdatedAsync(OrderUpdatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkRed, ConsoleColor.White, BuildLogMessage("Updated", evt.Order.Id));
            return true;
        }

        public async Task<bool> LogOrderClosedAsync(OrderClosedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkYellow, ConsoleColor.White, BuildLogMessage("Closed", evt.Order.OrderId));
            return true;
        }

        private static string BuildLogMessage(string action, Guid id)
        {
            return $"Logging Order {action} Event for Order Id {id}";
        }
    }
}
