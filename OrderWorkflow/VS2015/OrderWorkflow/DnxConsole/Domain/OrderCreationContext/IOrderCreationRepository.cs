namespace DnxConsole.Domain.OrderCreationContext
{
    public interface IOrderCreationRepository
    {
        void CreateOrder(Order order);
        Loan GetLoan(int id);
    }
}
