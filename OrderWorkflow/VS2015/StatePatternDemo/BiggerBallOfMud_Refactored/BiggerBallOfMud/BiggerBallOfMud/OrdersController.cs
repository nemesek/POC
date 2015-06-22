using System;
using System.Threading;
using BiggerBallOfMud.OrderStatuses;

namespace BiggerBallOfMud
{
    public class OrdersController
    {
        public Order ProcessOrder(int cmsId)
        {
            var cms = new Cms(cmsId);
            var order = cms.CreateNewOrder();
            IOrderStatus newStatus;

            while ((newStatus = order.Status.ProcessNextStep()) != null)
            {
                order.Save();
                Thread.Sleep(1000);
                Console.WriteLine("++++++++++++++Incoming Request about to be processed.+++++++++++++");

                order.Status = newStatus;
            }

            return order;
        }
    }
}
