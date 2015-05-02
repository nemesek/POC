namespace OrderWorkflow.Domain.Contracts
{
    public interface IProcessAutoAssign
    {
        Vendor FindBestVendor(IOrder order);
    }
}
