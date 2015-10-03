using System.Threading.Tasks;
using Akka.Actor;
using AkkaSample.Alee.MaybeSimpler.Commands;
using AkkaSample.Domain;

namespace AkkaSample.Alee.IoBound
{
    public class OrderActor : ReceiveActor
    {
        private readonly SlowOrderProcessor _orderProcessor = new SlowOrderProcessor();

        public OrderActor()
        {
            Receive<CreateOrderCommand>(c => HandleCreateOrderCommand(c.OrderMessage));
            Receive<AssignOrderCommand>(c => HandleAssignOrderCommand(c.OrderMessage));
            Receive<ReviewOrderCommand>(c => HandleReviewOrderCommand(c.OrderMessage));
            Receive<CloseOrderCommand>(c => HandleCloseOrderCommand(c.OrderMessage));
        }

        private async Task<bool> HandleCreateOrderCommand(OrderMessage orderMessage)
        {
            var sender = Sender;
            var orderDto = orderMessage.OrderDto;
            _orderProcessor.ProcessOrder(orderDto);
            var createdOrderDto = await _orderProcessor.GetOrderAsync(orderDto.OrderId);
            sender.Tell(new OrderCreatedEvent(new OrderMessage(orderMessage.CorrelationId, createdOrderDto)));
            return true;
        }

        private async Task<bool> HandleAssignOrderCommand(OrderMessage orderMessage)
        {
            var sender = Sender;
            var orderDto = orderMessage.OrderDto;
            _orderProcessor.AssignUser(orderDto.OrderId);
            var assignedOrderDto = await _orderProcessor.GetOrderAsync(orderDto.OrderId);
            sender.Tell(new OrderAssignedEvent(new OrderMessage(orderMessage.CorrelationId, assignedOrderDto)));
            return true;
        }

        private async Task<bool> HandleReviewOrderCommand(OrderMessage orderMessage)
        {
            var sender = Sender;
            var orderDto = orderMessage.OrderDto;
            _orderProcessor.ReviewOrder(orderDto.OrderId);
            var reviewedOrderDto = await _orderProcessor.GetOrderAsync(orderDto.OrderId);
            sender.Tell(new OrderReviewedEvent(new OrderMessage(orderMessage.CorrelationId, reviewedOrderDto)));
            return true;
        }

        private async Task<bool> HandleCloseOrderCommand(OrderMessage orderMessage)
        {
            var sender = Sender;
            var orderDto = orderMessage.OrderDto;
            _orderProcessor.CloseOrder(orderDto.OrderId);
            var closedOrderDto = await _orderProcessor.GetOrderAsync(orderDto.OrderId);
            sender.Tell(new OrderClosedEvent(new OrderMessage(orderMessage.CorrelationId, closedOrderDto)));
            return true;
        }
    }
}
