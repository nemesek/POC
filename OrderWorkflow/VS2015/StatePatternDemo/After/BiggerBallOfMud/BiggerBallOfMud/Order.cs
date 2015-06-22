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
        

        //public OrderStatus ProcessNextStep()
        //{
        //    return Status.ProcessNextStep();
        //}
 
    }
}
