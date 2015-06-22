using System;

namespace BiggerBallOfMud.OrderStatuses
{
    public class SubmittedStatus : OrderStatus
    {
        public SubmittedStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            IOrderStatus newStatus;

            switch (Order.ClientId)
            {
                case 21:
                case 14:
                case 24:
                    newStatus = new ClosedStatus(Order);
                    break;
                case 17:
                case 16:
                case 22:
                    // bbom
                    Console.WriteLine("Doing John Additional Order Submitted Buisness Logic");
                    newStatus = new ReviewSubmissionStatus(Order);
                    break;
                default:
                    newStatus = new ReviewSubmissionStatus(Order);
                    break;
            }

            return newStatus;
        }
    }
}
