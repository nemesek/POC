using System.Linq;

namespace OrderWorkflow.Domain
{
    public class DefaultAutoAssign : IProcessAutoAssign
    {
        public Vendor FindBestVendor(IOrder order)
        {
            var vendors = new VendorRepository().GetVendors();
            return vendors.FirstOrDefault();
        }
    }
}
