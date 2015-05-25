using System.Linq;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.AutoAssign
{
    public class DefaultAutoAssign : IProcessAutoAssign
    {
        public Vendor FindBestVendor(ICanBeAutoAssigned order)
        {
            var vendors = new VendorRepository().GetVendors();
            return vendors.FirstOrDefault();
        }
    }
}
