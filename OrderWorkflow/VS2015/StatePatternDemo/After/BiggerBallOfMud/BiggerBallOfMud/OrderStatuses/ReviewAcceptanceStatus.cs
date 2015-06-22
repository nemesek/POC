using System;
using System.Threading;

namespace BiggerBallOfMud.OrderStatuses
{
    public class ReviewAcceptanceStatus : OrderStatus
    {
        public ReviewAcceptanceStatus(Order order) : base(order)
        {
        }

        public override IOrderStatus ProcessNextStep()
        {
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            if (random.Next(1, 100) % 2 == 0)
                return new ClientAcceptedStatus(Order);
            else
                return new UnassignedStatus(Order);
        }
    }
}
