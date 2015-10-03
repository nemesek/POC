using Akka.Actor;
using AkkaSample.Alee.MaybeSimpler.Commands;
using AkkaSample.Domain;

namespace AkkaSample.Alee.MaybeSimpler
{
    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<CreateOrderCommand>(c => HandleCreateOrderCommand(c.OrderMessage));
            Receive<AssignOrderCommand>(c => HandleAssignOrderCommand(c.OrderMessage));
            Receive<ReviewOrderCommand>(c => HandleReviewOrderCommand(c.OrderMessage));
            Receive<CloseOrderCommand>(c => HandleCloseOrderCommand(c.OrderMessage));
        }

        private void HandleCreateOrderCommand(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            var orderProcessor = new OrderProcessor();
            orderProcessor.ProcessOrder(orderDto);
            var createdOrderDto = orderProcessor.GetOrder(orderDto.OrderId);
            Sender.Tell(new OrderCreatedEvent(new OrderMessage(orderMessage.CorrelationId, createdOrderDto)));
        }

        private void HandleAssignOrderCommand(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            var orderProcessor = new OrderProcessor();
            orderProcessor.AssignUser(orderDto.OrderId);
            var assignedOrderDto = orderProcessor.GetOrder(orderDto.OrderId);
            Sender.Tell(new OrderAssignedEvent(new OrderMessage(orderMessage.CorrelationId, assignedOrderDto)));
        }

        private void HandleReviewOrderCommand(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            var orderProcessor = new OrderProcessor();
            orderProcessor.ReviewOrder(orderDto.OrderId);
            var reviewedOrderDto = orderProcessor.GetOrder(orderDto.OrderId);
            Sender.Tell(new OrderReviewedEvent(new OrderMessage(orderMessage.CorrelationId, reviewedOrderDto)));
        }

        private void HandleCloseOrderCommand(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            var orderProcessor = new OrderProcessor();
            orderProcessor.CloseOrder(orderDto.OrderId);
            var closedOrderDto = orderProcessor.GetOrder(orderDto.OrderId);
            Sender.Tell(new OrderClosedEvent(new OrderMessage(orderMessage.CorrelationId, closedOrderDto)));
        }

    }
}
