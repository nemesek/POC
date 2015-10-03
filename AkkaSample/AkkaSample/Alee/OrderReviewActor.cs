using Akka.Actor;
using AkkaSample.Domain;

namespace AkkaSample.Alee
{
    public class OrderReviewActor : ReceiveActor
    {
        public OrderReviewActor()
        {
            Receive<OrderMessage>(o => HandleOrderMessage(o));
        }

        private void HandleOrderMessage(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            var orderProcessor = new OrderProcessor();
            orderProcessor.ReviewOrder(orderDto.OrderId);
            var reviewedOrderDto = orderProcessor.GetOrder(orderDto.OrderId);
            Sender.Tell(new OrderReviewedEvent(new OrderMessage(orderMessage.CorrelationId, reviewedOrderDto)));
        }
    }
}
