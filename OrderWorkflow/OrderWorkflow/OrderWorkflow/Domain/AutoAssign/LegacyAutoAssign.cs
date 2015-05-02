using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.AutoAssign
{
    public class LegacyAutoAssign : IProcessAutoAssign
    {
        public Vendor FindBestVendor(IOrderWithZipCode order)
        {
            return new NullVendor();
        }
    }
}
