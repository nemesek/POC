using Akka.Actor;
using AkkaSample.Domain;

namespace AkkaSample.Alee
{
    public class OrderAssignActor : ReceiveActor
    {
        public OrderAssignActor()
        {
            Receive<OrderMessage>(o => HandleOrderMessage(o));
        }

        private void HandleOrderMessage(OrderMessage orderMessage)
        {
            var orderDto = orderMessage.OrderDto;
            var orderProcessor = new OrderProcessor();
            orderProcessor.AssignUser(orderDto.OrderId);
            var assignedOrderDto = orderProcessor.GetOrder(orderDto.OrderId);
            Sender.Tell(new OrderAssignedEvent(new OrderMessage(orderMessage.CorrelationId, assignedOrderDto)));
        }
    }
}
