using Akka.Actor;
using AkkaSample.Domain;

namespace AkkaSample.Alee
{
    public class OrderCloseActor : ReceiveActor
    {
        public OrderCloseActor()
        {
            Receive<OrderMessage>(o => HandleOrderMessage(o));
        }

        private void HandleOrderMessage(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            var orderProcessor = new OrderProcessor();
            orderProcessor.CloseOrder(orderDto.OrderId);
            var closedOrderDto = orderProcessor.GetOrder(orderDto.OrderId);
            Sender.Tell(new OrderClosedEvent(new OrderMessage(orderMessage.CorrelationId, closedOrderDto)));
        }
    }
}
