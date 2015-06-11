using System.Threading.Tasks;
using DnxConsole.Domain.Common.Events;

namespace DnxConsole.Domain.Common.Contracts
{
    public interface ILogEvents
    {
        Task<bool> LogOrderCreationAsync(OrderCreatedEvent evt);
        Task<bool> LogOrderClosedAsync(OrderClosedEvent evt);
        Task<bool> LogOrderUpdatedAsync(OrderUpdatedEvent evt);
    }
}
