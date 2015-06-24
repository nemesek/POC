using System.Collections.Generic;
using System.Linq;

namespace LittleInterfaces
{
    public class AutoAssignService
    {
        public Vendor FindVendorToAssignOrderTo(string zipCode, IEnumerable<Vendor> vendors)
        {
            var vendor = vendors.FirstOrDefault(v => v.ZipCode == zipCode);
            return vendor;
        }
    }
}
