using System.Collections.Generic;
using System.Linq;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.AutoAssign
{
    public class CmsNetAutoAssign : IProcessAutoAssign
    {
        private readonly IVendorRepository _repository;

        public CmsNetAutoAssign(IVendorRepository repository)
        {
            _repository = repository;
        }
        public Vendor FindBestVendor(ICanBeAutoAssigned order)
        {
            var vendor = _repository
                .GetVendors()
                .FirstOrDefault(v => v.ZipCode == order.ZipCode);
            return vendor;
        }
    }
}
