using System.Threading.Tasks;
using DnxConsole.Domain.Events;

namespace DnxConsole.Domain.Contracts
{
    public interface ILogEvents
    {
        Task<bool> LogOrderCreationAsync(OrderCreatedEvent evt);
        Task<bool> LogOrderClosedAsync(OrderClosedEvent evt);
        Task<bool> LogOrderUpdatedAsync(OrderUpdatedEvent evt);
    }
}
