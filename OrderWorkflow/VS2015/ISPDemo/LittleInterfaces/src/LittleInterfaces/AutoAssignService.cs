using System.Collections.Generic;
using System.Linq;

namespace LittleInterfaces
{
    public class AutoAssignService
    {
        public Vendor FindVendorToAssignOrderTo(IBigOrderInterface order, IEnumerable<Vendor> vendors)
        {
            var vendor = vendors.FirstOrDefault(v => v.ZipCode == order.ZipCode);
            return vendor;
        }
    }
}
