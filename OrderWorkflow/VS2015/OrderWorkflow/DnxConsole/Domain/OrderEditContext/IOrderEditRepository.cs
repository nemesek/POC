namespace DnxConsole.Domain.OrderEditContext
{
    public interface IOrderEditRepository
    {
        Order GetOrder(int cmsId);
    }
}
