using System;
using System.Threading.Tasks;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.Events;

namespace DnxConsole.Utilities
{
    public class EventLogger : ILogEvents
    {
        public async Task<bool> LogOrderCreationAsync(OrderCreatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkYellow, ConsoleColor.White, $"Logging Order Created Event for Order Id {evt.Order.Id}");
            return true;

        }

        public async Task<bool> LogOrderUpdatedAsync(OrderUpdatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkRed, ConsoleColor.White, $"Order {evt.Order.Id} updated");
            return true;
        }

        public async Task<bool> LogOrderClosedAsync(OrderClosedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.DarkYellow, ConsoleColor.White, $"Sending Order Closed notifcation for {evt.Order.OrderId}");
            return true;
        }
    }
}
