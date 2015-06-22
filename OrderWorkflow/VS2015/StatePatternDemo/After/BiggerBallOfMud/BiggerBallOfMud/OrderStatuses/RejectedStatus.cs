using System;

namespace BiggerBallOfMud.OrderStatuses
{
    public class RejectedStatus : OrderStatus
    {
        public RejectedStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            IOrderStatus newStatus;

            if (Order.ClientId % 3 == 0)
            {
                Console.WriteLine("**************Applying custom rejected order business logic******************");
                // bbom 
                newStatus = new ReviewSubmissionStatus(Order);
            }
            else
            {
                newStatus = new ReviewSubmissionStatus(Order);
            }

            return newStatus;
        }
    }
}
