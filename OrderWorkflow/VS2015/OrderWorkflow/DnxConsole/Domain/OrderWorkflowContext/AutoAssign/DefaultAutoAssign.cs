using System.Collections.Generic;
using System.Linq;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.AutoAssign
{
    public class DefaultAutoAssign : IProcessAutoAssign
    {
        private readonly IVendorRepository _repository;

        public DefaultAutoAssign(IVendorRepository repository)
        {
            _repository = repository;
        }
        public Vendor FindBestVendor(ICanBeAutoAssigned orders)
        {
            //var vendors = new VendorRepository().GetVendors();
            return _repository.GetVendors().FirstOrDefault();
        }
    }
}
