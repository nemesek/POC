using System;
using Akka.Actor;

namespace AkkaSample.Alee
{
    public class AleeFanOutActor : ReceiveActor
    {
        private readonly IActorRef _orderCreateActor;
        private readonly IActorRef _orderAssignActor;
        private readonly IActorRef _orderReviewActor;
        private readonly IActorRef _orderCloseActor;

        public AleeFanOutActor()
        {
            Receive<CreateOrderCommand>(c => HandleCreateOrderCommand(c));
            Receive<OrderCreatedEvent>(e => HandleOrderCreatedEvent(e));
            Receive<OrderAssignedEvent>(e => HandleOrderAssignedEvent(e));
            Receive<OrderReviewedEvent>(e => HandleOrderReviewedEvent(e));
            Receive<OrderClosedEvent>(e => HandleOrderClosedEvent(e));

            var orderCreateActorProps = Props.Create<OrderCreateActor>();
            _orderCreateActor = Context.ActorOf(orderCreateActorProps, "OrderCreateActor");

            var orderAssignActorProps = Props.Create<OrderAssignActor>();
            _orderAssignActor = Context.ActorOf(orderAssignActorProps, "OrderAssignActor");

            var orderReviewActorProps = Props.Create<OrderReviewActor>();
            _orderReviewActor = Context.ActorOf(orderReviewActorProps, "OrderReviewActor");

            var orderCloseActorProps = Props.Create<OrderCloseActor>();
            _orderCloseActor = Context.ActorOf(orderCloseActorProps, "OrderCloseActor");
        }

        private void HandleCreateOrderCommand(CreateOrderCommand createOrderCommand)
        {
            Console.WriteLine($"Command Received for correlationId {createOrderCommand.OrderMessage.CorrelationId}");
            _orderCreateActor.Tell(createOrderCommand.OrderMessage);
        }

        private void HandleOrderCreatedEvent(OrderCreatedEvent orderCreatedEvent)
        {
            Console.WriteLine($"Order created for correlationId {orderCreatedEvent.OrderMessage.CorrelationId}");
            _orderAssignActor.Tell(orderCreatedEvent.OrderMessage);
        }

        private void HandleOrderAssignedEvent(OrderAssignedEvent orderAssignedEvent)
        {
            Console.WriteLine($"Order Assigned for correlationId {orderAssignedEvent.OrderMessage.CorrelationId}");
            _orderReviewActor.Tell(orderAssignedEvent.OrderMessage);
        }

        private void HandleOrderReviewedEvent(OrderReviewedEvent orderReviewedEvent)
        {
            Console.WriteLine($"Order reviewed  for correlationId {orderReviewedEvent.OrderMessage.CorrelationId}");
            _orderCloseActor.Tell(orderReviewedEvent.OrderMessage);
        }

        private void HandleOrderClosedEvent(OrderClosedEvent orderClosedEvent)
        {
            Console.WriteLine($"Order Closed for correlationId {orderClosedEvent.OrderMessage.CorrelationId}");
        }
    }
}
