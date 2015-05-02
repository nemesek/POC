namespace OrderWorkflow.Domain
{
    public interface IProcessAutoAssign
    {
        Vendor FindBestVendor(IOrder order);
    }
}
