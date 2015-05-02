using System.Linq;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.AutoAssign
{
    public class CmsNextAutoAssign : IProcessAutoAssign
    {
        public Vendor FindBestVendor(IOrderWithZipCode order)
        {
            var repo = new VendorRepository();
            var vendor = repo
                .GetVendors()
                .Where(v => v.ZipCode == order.ZipCode)
                .OrderBy(v => v.OrderCount)
                .FirstOrDefault();

            return vendor;
        }
    }
}
