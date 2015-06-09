using System;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.Vendors
{
    public class Vendor
    {
        private readonly int _orderCount;
        private readonly string _zip;
        private readonly string _name;

        public Vendor(int orderCount, string zipCode, string name)
        {
            _orderCount = orderCount;
            _zip = zipCode;
            _name = name;
        }

        public int OrderCount => _orderCount;
        public string ZipCode => _zip;
        public string Name => _name;

        public virtual void SendMeNotification(IWorkflowOrder order)
        {
            Console.WriteLine("Sending Email and text to user {0} about orderId: {1}", _name, order.OrderId);
        }

        public virtual bool AcceptOrder(IWorkflowOrder order)
        {
            Console.WriteLine("{0} has accepted order {1}", _name, order.OrderId);
            return true;
        }
    }
}
