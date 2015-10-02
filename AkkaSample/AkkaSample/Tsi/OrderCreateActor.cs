using Akka.Actor;

namespace AkkaSample.Tsi
{
    public class OrderCreateActor : ReceiveActor
    {
        public OrderCreateActor()
        {
            Receive<OrderMessage>(o => HandleOrderMessage(o));
        }

        private void HandleOrderMessage(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            var orderProcessor = new OrderProcessor();
            orderProcessor.ProcessOrder(orderDto);
            var createdOrderDto = orderProcessor.GetOrder(orderDto.OrderId);
            Sender.Tell(new OrderCreatedEvent(new OrderMessage(orderMessage.CorrelationId, createdOrderDto)));
        }
    }


}
