using System;
using System.Threading;

namespace BiggerBallOfMud.OrderStatuses
{
    public class ReviewSubmissionStatus : OrderStatus
    {
        public ReviewSubmissionStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            IOrderStatus newStatus;

            // randomly determine if its rejected
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();

            if (random.Next(1, 100) % 2 == 0)
            {
                // means we accepted
                newStatus = new ClosedStatus(Order);
            }
            else
            {
                newStatus = new RejectedStatus(Order);
                Console.WriteLine("!!!!!!!!!!!!!!!!Rejecting this garbage!!!!!!!!!!!!!!!!");
            }

            return newStatus;
        }
    }
}
