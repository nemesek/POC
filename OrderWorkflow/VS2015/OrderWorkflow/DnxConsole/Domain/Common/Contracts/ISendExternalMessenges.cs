using System.Threading.Tasks;
using DnxConsole.Domain.Common.Events;

namespace DnxConsole.Domain.Common.Contracts
{
    public interface ISendExternalMessenges
    {
        Task<bool> SendToBillingSystem(OrderClosedEvent evt);
        Task<bool> SendToWorkflowQueue(OrderCreatedEvent evt);
        Task<bool> SendOrderCreationNotificationAsync(OrderCreatedEvent evt);
    }
}
