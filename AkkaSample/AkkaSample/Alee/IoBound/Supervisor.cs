using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaSample.Alee.MaybeSimpler.Commands;

namespace AkkaSample.Alee.IoBound
{
    public class Supervisor : ReceiveActor
    {
        private readonly IActorRef _actorRef;

        public Supervisor()
        {
            Receive<CreateOrderCommand>(c => HandleCreateOrderCommand(c));
            Receive<OrderCreatedEvent>(e => HandleOrderCreatedEvent(e));
            Receive<OrderAssignedEvent>(e => HandleOrderAssignedEvent(e));
            Receive<OrderReviewedEvent>(e => HandleOrderReviewedEvent(e));
            Receive<OrderClosedEvent>(e => HandleOrderClosedEvent(e));

            var actorProps = Props.Create<OrderActor>();
            _actorRef = Context.ActorOf(actorProps, "OrderActor");
        }

        private void HandleCreateOrderCommand(CreateOrderCommand createOrderCommand)
        {
            Console.WriteLine($"Command Received for correlationId {createOrderCommand.OrderMessage.CorrelationId}");
            _actorRef.Tell(createOrderCommand);
        }

        private void HandleOrderCreatedEvent(OrderCreatedEvent orderCreatedEvent)
        {
            Console.WriteLine($"Order created for correlationId {orderCreatedEvent.OrderMessage.CorrelationId}");
            var orderAssignCommand = new AssignOrderCommand(orderCreatedEvent.OrderMessage);
            _actorRef.Tell(orderAssignCommand);
        }

        private void HandleOrderAssignedEvent(OrderAssignedEvent orderAssignedEvent)
        {
            Console.WriteLine($"Order Assigned for correlationId {orderAssignedEvent.OrderMessage.CorrelationId}");
            var orderReviewCommand = new ReviewOrderCommand(orderAssignedEvent.OrderMessage);
            _actorRef.Tell(orderReviewCommand);
        }

        private void HandleOrderReviewedEvent(OrderReviewedEvent orderReviewedEvent)
        {
            Console.WriteLine($"Order reviewed  for correlationId {orderReviewedEvent.OrderMessage.CorrelationId}");
            var closeOrderCommand = new CloseOrderCommand(orderReviewedEvent.OrderMessage);
            _actorRef.Tell(closeOrderCommand);
        }

        private void HandleOrderClosedEvent(OrderClosedEvent orderClosedEvent)
        {
            Console.WriteLine($"Order Closed for correlationId {orderClosedEvent.OrderMessage.CorrelationId}");

        }
    }
}
