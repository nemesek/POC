namespace DnxConsole.Domain.OrderWorkflowContext.Contracts
{
    public interface IOrderRepository
    {
        void PersistThisUpdatedDataSomeWhere(OrderWorkflowDto orderWorkflowDto);
    }
}
