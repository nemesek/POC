using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BiggerBallOfMud.Refactored
{
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
        public int ServiceId { get; set; }

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

                    Console.WriteLine("About to assign order to {0}", vendor.Name);
                    Vendor = vendor;
                    this.Status = OrderStatus.Assigned;
                }
                else if (ClientId % 4 == 1)
                {
                    vendor = vendorRepo
                    .GetVendors()
                    .FirstOrDefault(v => v.ZipCode == this.ZipCode);

                    Console.WriteLine("About to assign order to {0}", vendor.Name);
                    Vendor = vendor;
                    this.Status = OrderStatus.Assigned;
                }
                else if (ClientId % 4 == 2)
                {
                    vendor = vendorRepo
                        .GetVendors()
                        .Where(v => v.ZipCode == this.ZipCode)
                        .OrderBy(v => v.OrderCount)
                        .FirstOrDefault();

                    Console.WriteLine("About to assign order to {0}", vendor.Name);
                    Vendor = vendor;
                    this.Status = OrderStatus.Assigned;
                }
                else
                {
                    this.Status = OrderStatus.ManualAssign;
                    Console.WriteLine("Going to have to manully assign.");
                }


            }
            else if (Status == OrderStatus.ManualAssign)
            {
                Thread.Sleep(100); // helps with the randomization
                var random = new Random();
                if (random.Next(1, 100) % 2 != 0)
                {
                    Console.WriteLine("^^^^^^^^Reassignment was not successful^^^^^^^^^^");
                }
                else
                {
                    if (random.Next(1, 100) % 2 != 0)
                    {
                        var vendor = new Vendor(0, "38655", "Daniel Garrett");
                        Console.WriteLine("About to assign order to {0}", vendor.Name);
                        Vendor = vendor;
                        this.Status = OrderStatus.Assigned;
                    }
                    else
                    {
                        var vendor = new Vendor(0, "38655", "Dwain Richardson");
                        Console.WriteLine("About to assign order to {0}", vendor.Name);
                        Vendor = vendor;
                        this.Status = OrderStatus.Assigned;
                    }

                }

            }
            else if (Status == OrderStatus.Assigned)
            {
                if (Vendor.AcceptOrder(this))
                {
                    this.Status = OrderStatus.VendorAccepted;
                    Console.WriteLine("Vendor accepted.");
                }
                else
                {
                    Console.WriteLine("Vendor declined.");
                    this.Status = OrderStatus.Unassigned;
                }
            }
            else if (Status == OrderStatus.VendorAccepted)
            {
                if (ClientId % 5 == 0)
                {
                    this.Status = OrderStatus.Closed;

                }
                else
                {
                    this.Status = OrderStatus.ReviewAcceptance;
                }
            }
            else if (Status == OrderStatus.ReviewAcceptance)
            {
                // randomly determine if its rejected
                Thread.Sleep(100); // helps with the randomization
                var random = new Random();
                if (random.Next(1, 100) % 2 == 0)
                {
                    this.Status = OrderStatus.ClientAccepted;
                }
                else
                {
                    this.Status = OrderStatus.Unassigned;
                }
            }
            else if (Status == OrderStatus.ClientAccepted)
            {
                this.Status = OrderStatus.Submitted;
            }
            else if (Status == OrderStatus.Submitted)
            {
                if (ClientId == 21 || ClientId == 14 || ClientId == 24)
                {
                    this.Status = OrderStatus.Closed;
                }
                else if (ClientId == 17 || ClientId == 16 || ClientId == 22)
                {
                    // bbom
                    Console.WriteLine("Doing John Additional Order Submitted Buisness Logic");
                    this.Status = OrderStatus.ReviewSubmission;
                }
                else
                {
                    this.Status = OrderStatus.ReviewSubmission;
                }

            }
            else if (Status == OrderStatus.ReviewSubmission)
            {
                if (this.Status != OrderStatus.ReviewSubmission)
                {
                    throw new Exception("Order is not in correct state to have report Submitted.");
                }

                // randomly determine if its rejected
                Thread.Sleep(100); // helps with the randomization
                var random = new Random();

                if (random.Next(1, 100) % 2 == 0)
                {
                    // means we accepted
                    this.Status = OrderStatus.Closed;
                }
                else
                {
                    this.Status = OrderStatus.Rejected;
                    Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage!!!!!!!!!!!!!!!!");

                }
            }
            else if (Status == OrderStatus.Rejected)
            {
                if (ClientId % 3 == 0)
                {
                    Console.WriteLine("**************Applying custom rejected order business logic******************");
                    // bbom 
                    this.Status = OrderStatus.ReviewSubmission;
                }
                else
                {
                    this.Status = OrderStatus.ReviewSubmission;
                }
            }
        }
    }
}
