using System.Linq;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.AutoAssign
{
    public class CmsNetAutoAssign : IProcessAutoAssign
    {
        public Vendor FindBestVendor(IOrderWithZipCode order)
        {
            var repo = new VendorRepository();
            var vendor = repo
                .GetVendors()
                .First(v => v.ZipCode == order.ZipCode);

            return vendor;
        }
    }
}
