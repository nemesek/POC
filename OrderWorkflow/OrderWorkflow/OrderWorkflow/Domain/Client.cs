using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWorkflow.Domain
{
    public class Client
    {
        private int _id;
        private OrderRepository _orderRepository;

        public Client(int id):this(id, new OrderRepository())
        {
            
        }

        public Client(int id, OrderRepository orderRepository)
        {
            _id = id;
            _orderRepository = orderRepository;
        }

        public IOrder CreateNewOrder()
        {
            return _orderRepository.CreateNewOrder(FindBestVendor());
        }

        private Func<IOrder, Vendor> FindBestVendor()
        {
            var autoAssign = AutoAssignFactory.CreateAutoAssign(_id);
            return autoAssign.FindBestVendor;
        }
    }
}
