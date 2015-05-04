using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBallOfMud
{
    public class Vendor
    {
        private readonly int _orderCount = 0;
        private readonly string _zip = string.Empty;
        private readonly string _name = "Dan Nemesek";

        public Vendor(int orderCount, string zipCode, string name)
        {
            _orderCount = orderCount;
            _zip = zipCode;
            _name = name;
        }

        public int OrderCount
        {
            get { return _orderCount; }
        }

        public string ZipCode
        {
            get { return _zip; }
        }

        public string Name
        {
            get { return _name; }
        }

        public virtual void SendMeNotification(Order order)
        {
            Console.WriteLine("Sending Email and text to user {0} about orderId: {1}", _name, order.OrderId);
        }

        public virtual bool AcceptOrder(Order order)
        {
            Console.WriteLine("{0} has accepted order {1}", _name, order.OrderId);
            return true;
        }
    }
}
