using System;
using System.Threading;
using BiggerBallOfMud.Events;

namespace BiggerBallOfMud
{
    public class Vendor
    {
        public Vendor(int orderCount, string zipCode, string name)
        {
            OrderCount = orderCount;
            ZipCode = zipCode;
            Name = name;
        }

        public int OrderCount { get; } = 0;

        public string ZipCode { get; } = string.Empty;

        public string Name { get; } = "Dan Nemesek";

        public virtual void SendMeNotification(Order order)
        {
            Console.WriteLine("Sending Email and text to user {0} about orderId: {1}", Name, order.OrderId);
        }

        public virtual bool AcceptOrder(Order order)
        {
            Thread.Sleep(100); // helps with the randomization
            var random = new Random();
            if (random.Next(1, 100) % 2 == 0)
            {
                //Console.WriteLine("{0} has accepted order {1}", Name, order.OrderId);
                DomainEvents.Publish(new VendorAssignedEvent());
                return true;
            }

            //Console.WriteLine("{0} has rejected order {1}", Name, order.OrderId);
            DomainEvents.Publish(new VendorRejectedEvent());
            return false;

        }
    }
}
