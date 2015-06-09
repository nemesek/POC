using System;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.Vendors
{
    public class NullVendor : Vendor
    {
        public NullVendor() : base(0, string.Empty, "MAGIC_STRING_VALUE_INDICATING_NULL_VENDOR") {}

        public override void SendMeNotification(IWorkflowOrder order)
        {
            Console.WriteLine("Order id: {0}, is going to have to be assigned manually.", order.OrderId);
            Console.WriteLine("I am going to have to log this or notify the processor.");
        }

        public override bool AcceptOrder(IWorkflowOrder order)
        {
            Console.WriteLine("I Don't want this order!!!!!!!!!!!!!!!!!!!!!");
            return false;
        }
    }
}
