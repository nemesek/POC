using System;
using System.Threading;

namespace BiggerBallOfMud
{
    public class OrdersController
    {
        public Order ProcessOrder(int cmsId)
        {
            var cms = new Cms(cmsId);
            var order = cms.CreateNewOrder();
            while (order.Status != OrderStatus.Closed)
            {
                order.Save();
                //Thread.Sleep(1000);
                Console.WriteLine("++++++++++++++Incoming Request about to be processed.+++++++++++++");
                order.ProcessNextStep();
            }

            return order;
        }
    }
}
