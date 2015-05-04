using System;
using System.Linq;

namespace BigBallOfMud
{
    public enum OrderStatus
    {
        Unassigned = 0,
        Assigned = 1,
        Accepted = 2,
        Submitted = 3,
        Rejected = 4,
        WithClient = 5,
        Closed = 6
    }

    public class Order
    {
        public void Save()
        {
            Console.WriteLine("Saving {0} State to DB", Status);
        }

        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public Vendor Vendor { get; set; }

        public void ProcessUnassigned()
        {
            var vendorRepo = new VendorRepository();
            var vendor = vendorRepo
                .GetVendors()
                .FirstOrDefault();

            Console.WriteLine("About to assign order to {0}", vendor.Name);
            Vendor = vendor;
            this.Status = OrderStatus.Assigned;
        }

        public void ProcessAssigned()
        {
            if (Vendor.AcceptOrder(this))
            {
                this.Status = OrderStatus.Accepted;
                Console.WriteLine("Vendor accepted.");
                return;
            }

            Console.WriteLine("Vendor declined.");
            this.Status = OrderStatus.Unassigned;

        }

        public void ProcessAccepted()
        {
            this.Status = OrderStatus.Closed;
        }
    }

}
