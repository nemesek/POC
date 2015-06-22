using System;
using System.Linq;

namespace BiggerBallOfMud.After
{
    public class UnassignedOrder : Order
    {
        public UnassignedOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public override OrderStatus Status => OrderStatus.Unassigned;
        
        public override Order ProcessNextStep()
        {
            Vendor tempVendor;
            var vendorRepo = new VendorRepository();

            switch (this.CmsId % 4)
            {
                case 0:
                    tempVendor = vendorRepo
                        .GetVendors()
                        .FirstOrDefault();

                    break;
                case 1:
                    tempVendor = vendorRepo
                        .GetVendors()
                        .FirstOrDefault(v => v.ZipCode == this.ZipCode);

                    break;
                case 2:
                    tempVendor = vendorRepo
                        .GetVendors()
                        .Where(v => v.ZipCode == this.ZipCode)
                        .OrderBy(v => v.OrderCount)
                        .FirstOrDefault();

                    break;
                default:
                    tempVendor = null;
                    break;
            }

            if (tempVendor != null)
            {
                Console.WriteLine("About to assign order to {0}", tempVendor.Name);
                base.AssignVendor(tempVendor);
                return new AssignedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
            }

            Console.WriteLine("Going to have to manully assign.");
            return new UnassignedOrder(base.CmsId, base.ZipCode, base.AssignedVendor);
        }
    }
}
