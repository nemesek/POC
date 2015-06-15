using System;

namespace DnxConsole.Domain.OrderWorkflowContext.Contracts
{
    public interface IHaveAnOrderId
    {
        Guid OrderId { get; }
    }
}
