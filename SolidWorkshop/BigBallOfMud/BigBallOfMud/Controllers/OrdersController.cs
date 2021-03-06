﻿using System;

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
                Console.WriteLine("++++++++++++++Incoming Request about to be processed.+++++++++++++");
                if (order.Status == OrderStatus.Unassigned)
                {
                    order.ProcessUnassigned();
                }
                else if (order.Status == OrderStatus.Assigned)
                {
                    order.ProcessAssigned();
                }
                else if (order.Status == OrderStatus.Accepted)
                {
                    order.ProcessAccepted();
                }

            }

            return order;
        }
    }
}
