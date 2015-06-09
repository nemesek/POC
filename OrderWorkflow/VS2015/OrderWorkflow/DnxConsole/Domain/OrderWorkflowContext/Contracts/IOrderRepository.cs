using System;

namespace DnxConsole.Domain.OrderWorkflowContext.Contracts
{
    public interface IOrderRepository
    {
        void PersistThisUpdatedDataSomeWhere(Func<OrderWorkflowDto> orderWorkflowDtoFunc);
    }
}
