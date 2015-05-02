using System.Linq;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.AutoAssign
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
