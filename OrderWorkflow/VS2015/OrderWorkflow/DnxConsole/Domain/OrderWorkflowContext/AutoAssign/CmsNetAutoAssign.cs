using System.Linq;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.AutoAssign
{
    public class CmsNetAutoAssign : IProcessAutoAssign
    {
        public Vendor FindBestVendor(ICanBeAutoAssigned order)
        {
            var repo = new VendorRepository();
            var vendor = repo
                .GetVendors()
                .FirstOrDefault(v => v.ZipCode == order.ZipCode);

            return vendor;
        }
    }
}
