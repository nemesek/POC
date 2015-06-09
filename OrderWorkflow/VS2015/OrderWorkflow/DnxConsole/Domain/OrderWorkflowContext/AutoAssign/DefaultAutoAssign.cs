using System.Linq;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.AutoAssign
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
