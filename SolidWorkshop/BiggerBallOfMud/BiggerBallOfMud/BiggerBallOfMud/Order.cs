using System;
using System.Linq;
using System.Threading;

namespace BiggerBallOfMud
{
    public enum OrderStatus
    {
        Unassigned = 0,
        Assigned = 1,
        VendorAccepted = 2,
        Submitted = 3,
        Rejected = 4,
        ManualAssign = 5,
        Closed = 6,
        ReviewSubmission = 7,
        ReviewAcceptance = 8,
        ClientAccepted = 9
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
        public int ClientId { get; set; }
        public string ZipCode { get; set; }

        public void ProcessNextStep()
        {
            if (Status == OrderStatus.Unassigned)
            {
                var vendorRepo = new VendorRepository();
                Vendor vendor;

                if (ClientId % 4 == 0)
                {
                    vendor = vendorRepo
                       .GetVendors()
                       .FirstOrDefault();
                }
                else if (ClientId % 4 == 1)
                {
                    vendor = vendorRepo
                    .GetVendors()
                    .FirstOrDefault(v => v.ZipCode == this.ZipCode);
                }
                else if (ClientId % 4 == 2)
                {
                    vendor = vendorRepo
                        .GetVendors()
                        .Where(v => v.ZipCode == this.ZipCode)
                        .OrderBy(v => v.OrderCount)
                        .FirstOrDefault();
                }
                else
                {
                    this.Status = OrderStatus.ManualAssign;
                    Console.WriteLine("Going to have to manully assign.");
                    return;
                }

                Console.WriteLine("About to assign order to {0}", vendor.Name);
                Vendor = vendor;
                this.Status = OrderStatus.Assigned;
                return;
            }

            if (Status == OrderStatus.ManualAssign)
            {
                Thread.Sleep(100); // helps with the randomization
                var random = new Random();
                if (random.Next(1, 100) % 2 != 0)
                {
                    Console.WriteLine("^^^^^^^^Reassignment was not successful^^^^^^^^^^");
                    return;
                }

                var vendor = new Vendor(0, "38655", "Daniel Garrett");
                Console.WriteLine("About to assign order to {0}", vendor.Name);
                Vendor = vendor;
                this.Status = OrderStatus.Assigned;
                return;
            }

            if (Status == OrderStatus.Assigned)
            {
                if (Vendor.AcceptOrder(this))
                {
                    this.Status = OrderStatus.VendorAccepted;
                    Console.WriteLine("Vendor accepted.");
                    return;
                }

                Console.WriteLine("Vendor declined.");
                this.Status = OrderStatus.Unassigned;
                return;
            }

            if (Status == OrderStatus.VendorAccepted)
            {
                if (ClientId % 5 == 0)
                {
                    this.Status = OrderStatus.Closed;
                    return;
                }

                this.Status = OrderStatus.ReviewAcceptance;
            }

            if (Status == OrderStatus.ReviewAcceptance)
            {
                // randomly determine if its rejected
                Thread.Sleep(100); // helps with the randomization
                var random = new Random();
                if (random.Next(1, 100)%2 == 0)
                {
                    this.Status = OrderStatus.ClientAccepted;
                    return;
                }

                this.Status = OrderStatus.Unassigned;
                return;

            }

            if (Status == OrderStatus.ClientAccepted)
            {
                this.Status = OrderStatus.Submitted;
            }

            if (Status == OrderStatus.Submitted)
            {
                if (ClientId == 21 || ClientId == 14 || ClientId == 24)
                {
                    this.Status = OrderStatus.Closed;
                    return;
                }

                if (ClientId == 17 || ClientId == 16 || ClientId == 22)
                {
                    Console.WriteLine("Doing More Submitted Status Buisness Logic");
                }

                this.Status = OrderStatus.ReviewSubmission;
                return;
            }

            if (Status == OrderStatus.ReviewSubmission)
            {
                if (AcceptSubmittedReport())
                {
                    this.Status = OrderStatus.Closed;
                    return;
                }

                this.Status = OrderStatus.Rejected;
                if (ClientId == 17 || ClientId == 16 || ClientId == 22)
                {
                    Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage because I am John!!!!!!!!!!!!!!!!");
                    return;
                }

                Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage!!!!!!!!!!!!!!!!");
                return;
            }

            if (Status == OrderStatus.Rejected)
            {
                if (ClientId % 3 == 0)
                {
                    Console.WriteLine("**************Applying custom rejected order business logic******************");
                }

                this.Status = OrderStatus.ReviewSubmission;
            }
        }

        public bool AcceptSubmittedReport()
        {
            if (this.Status != OrderStatus.ReviewSubmission)
            {
                throw new Exception("Order is not in correct state to have report Submitted.");
            }

            // randomly determine if its rejected
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            return random.Next(1, 100) % 2 == 0;
        }
    }

}
