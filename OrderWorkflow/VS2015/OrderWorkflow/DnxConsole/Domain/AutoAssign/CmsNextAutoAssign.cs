using System.Linq;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.AutoAssign
{
    public class CmsNextAutoAssign : IProcessAutoAssign
    {
        public Vendor FindBestVendor(ICanBeAutoAssigned order)
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
