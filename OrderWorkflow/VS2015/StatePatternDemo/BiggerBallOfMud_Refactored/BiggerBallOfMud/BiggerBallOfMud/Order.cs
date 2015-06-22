using System;
using BiggerBallOfMud.OrderStatuses;

namespace BiggerBallOfMud
{
    //public enum OrderStatus
    //{
    //    Unassigned = 0,
    //    Assigned = 1,
    //    VendorAccepted = 2,
    //    Submitted = 3,
    //    Rejected = 4,
    //    ManualAssign = 5,
    //    Closed = 6,
    //    ReviewSubmission = 7,
    //    ReviewAcceptance = 8,
    //    ClientAccepted = 9
    //}

    public class Order
    {
        public Order()
        {
            Status = new UnassignedStatus(this);
        }

        public void Save()
        {
            Console.WriteLine("Saving {0} State to DB", Status);
        }

        public Guid OrderId { get; set; }
        public IOrderStatus Status { get; set; }
        public Vendor Vendor { get; set; }
        public int ClientId { get; set; }
        public string ZipCode { get; set; }
        public int ServiceId { get; set; }

        //public void ProcessNextStep()
        //{
        //    Vendor tempVendor;

        //    switch (Status)
        //    {
        //        case OrderStatus.Unassigned:
        //            var vendorRepo = new VendorRepository();

        //            switch (ClientId % 4)
        //            {
        //                case 0:
        //                    tempVendor = vendorRepo
        //                        .GetVendors()
        //                        .FirstOrDefault();

        //                    Console.WriteLine("About to assign order to {0}", tempVendor.Name);
        //                    Vendor = tempVendor;
        //                    Status = OrderStatus.Assigned;
        //                    break;
        //                case 1:
        //                    tempVendor = vendorRepo
        //                        .GetVendors()
        //                        .FirstOrDefault(v => v.ZipCode == ZipCode);

        //                    Console.WriteLine("About to assign order to {0}", tempVendor.Name);
        //                    Vendor = tempVendor;
        //                    Status = OrderStatus.Assigned;
        //                    break;
        //                case 2:
        //                    tempVendor = vendorRepo
        //                        .GetVendors()
        //                        .Where(v => v.ZipCode == ZipCode)
        //                        .OrderBy(v => v.OrderCount)
        //                        .FirstOrDefault();

        //                    Console.WriteLine("About to assign order to {0}", tempVendor.Name);
        //                    Vendor = tempVendor;
        //                    Status = OrderStatus.Assigned;
        //                    break;
        //                default:
        //                    Status = OrderStatus.ManualAssign;
        //                    Console.WriteLine("Going to have to manully assign.");
        //                    break;
        //            }
        //            break;

        //        case OrderStatus.ManualAssign:
        //        {
        //            Thread.Sleep(100); // helps with the randomization
        //            var random = new Random();
        //            if (random.Next(1, 100)%2 != 0)
        //            {
        //                Console.WriteLine("^^^^^^^^Reassignment was not successful^^^^^^^^^^");
        //            }
        //            else
        //            {
        //                if (random.Next(1, 100)%2 != 0)
        //                {
        //                    tempVendor = new Vendor(0, "38655", "Daniel Garrett");
        //                    Console.WriteLine("About to assign order to {0}", tempVendor.Name);
        //                    Vendor = tempVendor;
        //                    Status = OrderStatus.Assigned;
        //                }
        //                else
        //                {
        //                    tempVendor = new Vendor(0, "38655", "Dwain Richardson");
        //                    Console.WriteLine("About to assign order to {0}", tempVendor.Name);
        //                    Vendor = tempVendor;
        //                    Status = OrderStatus.Assigned;
        //                }

        //            }
                
        //        }
        //        break;

        //        case OrderStatus.Assigned:
        //            if (Vendor.AcceptOrder(this))
        //            {
        //                Status = OrderStatus.VendorAccepted;
        //                Console.WriteLine("Vendor accepted.");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Vendor declined.");
        //                Status = OrderStatus.Unassigned;
        //            }
        //            break;

        //        case OrderStatus.VendorAccepted:
        //            Status = ClientId%5 == 0 ? OrderStatus.Closed : OrderStatus.ReviewAcceptance;
        //            break;

        //        case OrderStatus.ReviewAcceptance:
        //        {
        //            // randomly determine if its rejected
        //            Thread.Sleep(100); // helps with the randomization
        //            var random = new Random();
        //            Status = random.Next(1, 100)%2 == 0 ? OrderStatus.ClientAccepted : OrderStatus.Unassigned;
        //        }
        //            break;

        //        case OrderStatus.ClientAccepted:
        //            Status = OrderStatus.Submitted;
        //            break;

        //        case OrderStatus.Submitted:
        //            switch (ClientId)
        //            {
        //                case 21:
        //                case 14:
        //                case 24:
        //                    Status = OrderStatus.Closed;
        //                    break;
        //                case 17:
        //                case 16:
        //                case 22:
        //                    // bbom
        //                    Console.WriteLine("Doing John Additional Order Submitted Buisness Logic");
        //                    break;
        //                default:
        //                    Status = OrderStatus.ReviewSubmission;
        //                    break;
        //            }
        //            break;

        //        case OrderStatus.ReviewSubmission:
        //        {
        //            if (Status != OrderStatus.ReviewSubmission)
        //            {
        //                throw new Exception("Order is not in correct state to have report Submitted.");
        //            }

        //            // randomly determine if its rejected
        //            Thread.Sleep(100); // helps with the randomization
        //            var random = new Random();

        //            if (random.Next(1, 100) % 2 == 0)
        //            {
        //                // means we accepted
        //                Status = OrderStatus.Closed;
        //            }
        //            else
        //            {
        //                Status = OrderStatus.Rejected;
        //                Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage!!!!!!!!!!!!!!!!");        
                    
        //            }
        //        }
        //            break;

        //        case OrderStatus.Rejected:
        //            if (ClientId%3 == 0)
        //            {
        //                Console.WriteLine("**************Applying custom rejected order business logic******************");
        //                // bbom 
        //            }
        //            else
        //            {
        //                Status = OrderStatus.ReviewSubmission;    
        //            }
        //            break;
        //    }
        //}
    }
}
