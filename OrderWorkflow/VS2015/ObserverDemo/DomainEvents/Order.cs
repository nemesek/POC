using System;
using System.Threading.Tasks;

namespace DomainEvents
{
    public class Order
    {
        public Guid Id { get; private set; }
        public void CreateOrder()
        {
            this.Id = Guid.NewGuid();
            DomainEvents.Raise(new OrderCreatedEvent { Order = this });
            Console.WriteLine("Exiting CreateOrder");
        }

        //public async Task<bool> CreateOrderAsync()
        //{
        //    this.Id = Guid.NewGuid();
        //    await DomainEvents.RaiseAsync(new OrderCreatedEvent { Order = this });
        //    Console.WriteLine("Exiting CreateOrder");
        //    return true;
        //}
    }
}
