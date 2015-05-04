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
            switch (order.Status)
            {
                case OrderStatus.Unassigned:
                    {
                        order.Status = OrderStatus.Assigned;
                        return;
                    }
                case OrderStatus.Assigned:
                    {
                        order.Status = OrderStatus.Accepted;
                        return;
                    }
                case OrderStatus.Accepted:
                    {
                        order.Status = OrderStatus.Closed;
                        return;
                    }
            }


            throw new ArgumentOutOfRangeException(string.Format("Status {0} is not valid.", order.Status));
        }
    }
}
