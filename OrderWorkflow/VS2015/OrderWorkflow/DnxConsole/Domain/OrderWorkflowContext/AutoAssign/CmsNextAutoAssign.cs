using System.Linq;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.AutoAssign
{
    public class CmsNextAutoAssign : IProcessAutoAssign
    {
        private readonly IVendorRepository _repository;

        public CmsNextAutoAssign(IVendorRepository repository)
        {
            _repository = repository;
        }
        public Vendor FindBestVendor(ICanBeAutoAssigned order)
        {
            var vendor = _repository
                .GetVendors()
                .Where(v => v.ZipCode == order.ZipCode)
                .OrderBy(v => v.OrderCount)
                .FirstOrDefault();
            return vendor;
        }
    }
}
