using System;
using System.Threading.Tasks;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.Events;
using DnxConsole.Infrastructure.Utilities;

namespace DnxConsole.Infrastructure.Services
{
    public class RestfulMessenger : ISendExternalMessenges
    {
        public async Task<bool> SendToBillingSystem(OrderClosedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.Green, ConsoleColor.White, $"Sending order closed details for order {evt.Order.OrderId} to billing system.");
            return true;
        }

        public async Task<bool> SendToWorkflowQueue(OrderCreatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.Green, ConsoleColor.White, $"Queueing up order {evt.Order.Id} to workflow context");
            return true;
        }

        public async Task<bool> SendOrderCreationNotificationAsync(OrderCreatedEvent evt)
        {
            await Task.Delay(100);
            ConsoleHelper.WriteWithStyle(ConsoleColor.Blue, ConsoleColor.White, $"Sending order created email notification for order {evt.Order.Id}");
            return true;
        }
    }
}
