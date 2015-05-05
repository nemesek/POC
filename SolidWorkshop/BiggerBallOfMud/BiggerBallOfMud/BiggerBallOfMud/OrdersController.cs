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
                else if (order.Status == OrderStatus.Submitted)
                {
                    order.ProcessSubmitted();
                }
                else if (order.Status == OrderStatus.Rejected)
                {
                    order.ProcessRejected();
                }
                else if (order.Status == OrderStatus.OnHold)
                {
                    order.ProcessOnHold();
                }
                else if (order.Status == OrderStatus.Review)
                {
                    order.ProcessReview();
                }

            }

            return order;
        }
    }
}
