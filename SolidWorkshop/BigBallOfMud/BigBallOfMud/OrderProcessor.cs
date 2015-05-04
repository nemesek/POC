using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBallOfMud
{
    public class OrderProcessor
    {
        public void UpdateOrder(Order order)
        {
            ProcessOrder(order);
        }

        private void ProcessOrder(Order order)
        {
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
    }
}
