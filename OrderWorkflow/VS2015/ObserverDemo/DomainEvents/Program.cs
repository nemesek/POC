using System;
using System.Threading.Tasks;

namespace DomainEvents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var success = false;
            Console.WriteLine("Hello Class");
            DomainEvents.SubscribeTo<OrderCreatedEvent>(async _ => await DoSomethingAsync());
            DomainEvents.SubscribeTo<OrderCreatedEvent>(async _ => await DoSomethingElseAsync());
            DomainEvents.SubscribeTo<OrderCreatedEvent>(async _ => await DoSomethingLongAsync(3));
            //DomainEvents.SubscribeTo<OrderCreatedEvent>(_ => success = DoSomethingLongAsync(3).Result);
            var order = new Order();
            order.CreateOrder();
            //order.CreateOrder((o) => DomainEvents.Publish(new OrderCreatedEvent {Order = o}));
            Console.ReadLine();

        }

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
    }
}
