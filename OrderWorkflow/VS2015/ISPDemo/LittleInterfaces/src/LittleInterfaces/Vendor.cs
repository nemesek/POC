using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleInterfaces
{
    public class Vendor
    {
        private readonly int _orderCount;
        private readonly string _zip;
        private readonly string _name;
        private readonly string _clientUserId;

        public Vendor(int orderCount, string zipCode, string name)
        {
            _orderCount = orderCount;
            _zip = zipCode;
            _name = name;
        }

        public int OrderCount => _orderCount;
        public string ZipCode => _zip;
        public string Name => _name;
        public string ClientUserId => _clientUserId;



        public virtual bool AcceptOrder(IBigOrderInterface order)
        {
            Console.WriteLine("{0} has accepted order {1}", _name, order.OrderId);
            return true;
        }
    }
}
