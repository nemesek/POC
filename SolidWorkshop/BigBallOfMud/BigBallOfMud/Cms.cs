using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBallOfMud
{
    public class Cms
    {
        private int _id;

        public Cms(int id)
        {
            _id = id;    
        }

        public Order CreateNewOrder()
        {
            var order = new Order {OrderId = Guid.NewGuid(),Status = OrderStatus.Unassigned};
            return order;
        }
    }
}
