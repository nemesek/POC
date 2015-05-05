using System;

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
                Console.WriteLine("++++++++++++++Incoming Request about to be processed.+++++++++++++");
                order.ProcessNextStep();
            }

            return order;
        }
    }
}
