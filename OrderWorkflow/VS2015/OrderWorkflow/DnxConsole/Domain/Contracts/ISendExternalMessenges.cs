using System.Threading.Tasks;
using DnxConsole.Domain.Events;

namespace DnxConsole.Domain.Contracts
{
    public interface ISendExternalMessenges
    {
        Task<bool> SendToBillingSystem(OrderClosedEvent evt);
        Task<bool> SendToWorkflowQueue(OrderCreatedEvent evt);
        Task<bool> SendOrderCreationNotificationAsync(OrderCreatedEvent evt);
    }
}
