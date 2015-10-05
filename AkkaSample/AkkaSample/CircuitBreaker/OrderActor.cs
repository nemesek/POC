using System;
using Akka.Actor;
using AkkaSample.Alee;

namespace AkkaSample.CircuitBreaker
{
    public class OrderActor : ReceiveActor, IWithUnboundedStash
    {
        public OrderActor()
        {
            Receive<CreateOrderCommand>(c => HandleCreateOrderCommand(c.OrderMessage));
            Receive<DbStatusMessage>(m => HandleDbStatusMessage(m));
        }

        public IStash Stash { get; set; }


        private void HandleDbStatusMessage(DbStatusMessage dbStatusMessage)
        {
            if (dbStatusMessage.IsAvailable)
            {
                Stash.UnstashAll();
            }
        }

        private void HandleCreateOrderCommand(OrderMessage orderMessage)
        {
            try
            {
                //Console.WriteLine("About to call db");
                StubbedDatabase.RunQuery();
                Console.WriteLine($"Ran Query for order with message correlationId {orderMessage.CorrelationId}");
            }
           
            catch (InfrastructureException)
            {
                Console.WriteLine("Down goes the DB!");
                Stash.Stash();
                Sender.Tell(new DbStatusMessage(false, false));
                
            }
        }

        
    }
}
