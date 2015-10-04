using System;
using Akka.Actor;
using AkkaSample.Alee;

namespace AkkaSample.CircuitBreaker
{
    public class CircuitBreakerSupervisor : ReceiveActor, IWithUnboundedStash
    {
        private readonly IActorRef _actorRef;

        public CircuitBreakerSupervisor()
        {
            Receive<DbStatusMessage>(m => HandleDbStatusMessage(m));
            Receive<CreateOrderCommand>(c => HandleCreateOrderCommandWhenCircuitClosed(c));

            var actorProps = Props.Create<OrderActor>();
            _actorRef = Context.ActorOf(actorProps, "OrderActor");
        }

        public IStash Stash { get; set; }

        private void HandleDbStatusMessage(DbStatusMessage dbStatusMessage)
        {
            if (dbStatusMessage.IsAvailable)
            {
                Stash.UnstashAll();
                Become(CircuitClosedBehavior);
                _actorRef.Tell(dbStatusMessage);
                return;
            }

            Become(CircuitOpenBehavior);
        }

        private void CircuitOpenBehavior()
        {
            Receive<CreateOrderCommand>(c => HandleCreateOrderCommandWhenCircuitOpen(c));
            Receive<DbStatusMessage>(m => HandleDbStatusMessage(m));
        }

        private void CircuitClosedBehavior()
        {
            Receive<CreateOrderCommand>(c => HandleCreateOrderCommandWhenCircuitClosed(c));
            Receive<DbStatusMessage>(m => HandleDbStatusMessage(m));
        }

        private void HandleCreateOrderCommandWhenCircuitOpen(CreateOrderCommand command)
        {
            Stash.Stash();
        }

        private void HandleCreateOrderCommandWhenCircuitClosed(CreateOrderCommand createOrderCommand)
        {
            Console.WriteLine($"Command Received for correlationId {createOrderCommand.OrderMessage.CorrelationId}");
            _actorRef.Tell(createOrderCommand);
        }

        //protected override SupervisorStrategy SupervisorStrategy()
        //{
        //    return new OneForOneStrategy(10, TimeSpan.FromSeconds(30),
        //         x =>
        //        {
        //            // Maybe ArithmeticException is not application critical
        //            // so we just ignore the error and keep going.
        //            if (x is ArithmeticException) return Directive.Resume;

        //            // Error that we have no idea what to do with
        //            //else if (x is InsanelyBadException) return Directive.Escalate;
        //            else if (x is InfrastructureException)
        //            {
        //                Self.Tell(new DbStatusMessage(false, false));
        //                return Directive.Restart;
        //            }


        //            // Error that we can't recover from, stop the failing child
        //            else if (x is NotSupportedException) return Directive.Stop;

        //            // otherwise restart the failing child
        //            else return Directive.Restart;
        //        });
        //}

    }
}
