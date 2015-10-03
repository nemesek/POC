using Akka.Actor;
using AkkaSample.Alee.MaybeSimpler.Commands;
using AkkaSample.Domain;

namespace AkkaSample.Alee.IoBound
{
    public class BlockedOrderActor : ReceiveActor
    {
        private readonly SlowOrderProcessor _orderProcessor = new SlowOrderProcessor();

        public BlockedOrderActor()
        {
            Receive<CreateOrderCommand>(c => HandleCreateOrderCommand(c.OrderMessage));
            Receive<AssignOrderCommand>(c => HandleAssignOrderCommand(c.OrderMessage));
            Receive<ReviewOrderCommand>(c => HandleReviewOrderCommand(c.OrderMessage));
            Receive<CloseOrderCommand>(c => HandleCloseOrderCommand(c.OrderMessage));
        }

        private void HandleCreateOrderCommand(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            _orderProcessor.ProcessOrder(orderDto);
            var createdOrderDto = _orderProcessor.GetOrderAsync(orderDto.OrderId).Result;
            Sender.Tell(new OrderCreatedEvent(new OrderMessage(orderMessage.CorrelationId, createdOrderDto)));
        }

        private void HandleAssignOrderCommand(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            _orderProcessor.AssignUser(orderDto.OrderId);
            var assignedOrderDto = _orderProcessor.GetOrderAsync(orderDto.OrderId).Result;
            Sender.Tell(new OrderAssignedEvent(new OrderMessage(orderMessage.CorrelationId, assignedOrderDto)));
        }

        private void HandleReviewOrderCommand(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            _orderProcessor.ReviewOrder(orderDto.OrderId);
            var reviewedOrderDto = _orderProcessor.GetOrderAsync(orderDto.OrderId).Result;
            Sender.Tell(new OrderReviewedEvent(new OrderMessage(orderMessage.CorrelationId, reviewedOrderDto)));
        }

        private void HandleCloseOrderCommand(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            _orderProcessor.CloseOrder(orderDto.OrderId);
            var closedOrderDto = _orderProcessor.GetOrderAsync(orderDto.OrderId).Result;
            Sender.Tell(new OrderClosedEvent(new OrderMessage(orderMessage.CorrelationId, closedOrderDto)));
        }
    }
}
