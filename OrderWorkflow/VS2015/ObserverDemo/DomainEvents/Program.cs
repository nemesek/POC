using System;
using System.IO;
using System.Threading.Tasks;

namespace DomainEvents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello Brian");
            DomainEvents.Register<OrderCreatedEvent>(async _ => await DoSomethingAsync());
            DomainEvents.Register<OrderCreatedEvent>(async _ => await DoSomethingElseAsync());
            var order = new Order();
            order.CreateOrder();
            Console.WriteLine(true);
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
    }
}
