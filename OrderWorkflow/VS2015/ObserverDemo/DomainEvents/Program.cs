using System;
using System.Threading.Tasks;

namespace DomainEvents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello Class");
	    var isLongTime = false;
            DomainEvents.Subscribe<OrderCreatedEvent>(async _ => await DoSomethingAsync());
            DomainEvents.Subscribe<OrderCreatedEvent>(async _ => await DoSomethingElseAsync());
	    DomainEvents.Subscribe<OrderCreatedEvent>(async _  => await DoSomethingLongAsync(3));
            var order = new Order();
            order.CreateOrder();
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
