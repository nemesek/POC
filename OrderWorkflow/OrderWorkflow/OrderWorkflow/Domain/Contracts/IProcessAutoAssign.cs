namespace OrderWorkflow.Domain.Contracts
{
    public interface IProcessAutoAssign
    {
        Vendor FindBestVendor(IOrderWithZipCode order);
    }
}
