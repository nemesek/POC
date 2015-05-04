using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBallOfMud.Controllers
{
    public class OrdersController
    {
        public Order ProcessOrder(int cmsId)
        {
            var cms = new Cms(cmsId);
            var order = cms.CreateNewOrder();
            while (order.Status != OrderStatus.Closed && order.Status != OrderStatus.WithClient)
            {
                order.Save();
                Console.WriteLine("Incoming Request about to be processed.");
                var processor = new OrderProcessor();
                processor.UpdateOrder(order);
            }

            return order;
        }
    }
}
