using System;
using System.Threading.Tasks;

namespace DomainEvents
{
    public class Order
    {
        public Guid Id { get; private set; }

        public String Status { get; private set; }

        // business logic is to create a order with status new 
        // set the order Id
        // Save the Order
        // what else
        public void CreateOrder()
        {
            this.Id = Guid.NewGuid();
            this.Status = "New";
            this.Save();
            Console.WriteLine("Exiting CreateOrder");
        }


        public void Save()
        {
            Console.WriteLine("Saving the order to the DB");
        }

        #region comments
        //public void CreateOrder()
        //{
        //    this.Id = Guid.NewGuid();
        //    DomainEvents.Publish(new OrderCreatedEvent { Order = this });
        //    Console.WriteLine("Exiting CreateOrder");
        //}


        //// you can even decouple the event type being raised
        //// from the event raiser
        //public void CreateOrder(Action<Order> eventAction)
        //{
        //    this.Id = Guid.NewGuid();
        //    eventAction?.Invoke(this);
        //    Console.WriteLine("Exiting CreateOrder");
        //}

        //public async Task<bool> CreateOrderAsync(Action<IDomainEvent> action)
        //{
        //    this.Id = Guid.NewGuid();
        //    await Task.Delay(100);
        //    //await DomainEvents.RaiseAsync(new OrderCreatedEvent { Order = this });
        //    //DomainEvents.RaiseAsync(new OrderCreatedEvent {Order = this});
        //    action(new OrderCreatedEvent {Order = this});
        //    Console.WriteLine("Exiting CreateOrder");

        //    return true;
        //}
        #endregion

    }
}
