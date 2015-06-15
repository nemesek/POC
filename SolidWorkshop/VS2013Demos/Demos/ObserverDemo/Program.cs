using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverDemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            RegisterEventHandlers();
            Console.WriteLine("Hello Class");
            var order = new Order();
            order.CreateOrder();
            Console.ReadLine();

        }


        #region comments

        public static async Task<bool> DoSomethingAsync()
        {
            await Task.Delay(100);
            Console.WriteLine("Did Something Async");
            await Task.Delay(1000);
            return true;
        }

        public static async Task<bool> DoSomethingElseAsync()
        {
            await Task.Delay(100);
            Console.WriteLine("Did Something Else Async");
            return true;
        }

        private static async Task<bool> OutputAsync(string output)
        {
            await Task.Delay(100);
            Console.WriteLine(output);
            return true;
        }

        private static async Task<bool> DoSomethingLongAsync(int second)
        {
            await Task.Delay(second * 1000);
            Console.WriteLine("Okay finally done");
            return true;
        }

        public static void RegisterEventHandlers()
        {
            //var success = false;
            DomainEvents.SubscribeTo<OrderCreatedEvent>(async _ => await DoSomethingAsync());
            DomainEvents.SubscribeTo<OrderCreatedEvent>(async _ => await DoSomethingElseAsync());
            DomainEvents.SubscribeTo<OrderCreatedEvent>(async _ => await DoSomethingLongAsync(3));
            //DomainEvents.SubscribeTo<OrderCreatedEvent>(_ => success = DoSomethingLongAsync(3).Result);
            //order.CreateOrder((o) => DomainEvents.Publish(new OrderCreatedEvent {Order = o}));

        }
        #endregion
    }
}
