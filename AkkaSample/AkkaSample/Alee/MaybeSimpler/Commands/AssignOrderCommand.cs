using System;

namespace AkkaSample.Alee.MaybeSimpler.Commands
{
    public class AssignOrderCommand
    {
        private readonly OrderMessage _orderMessage;

        public AssignOrderCommand(Guid correlationId, OrderDto orderDto)
        {
            _orderMessage = new OrderMessage(correlationId, orderDto);
        }

        public AssignOrderCommand(OrderMessage orderMessage)
        {
            _orderMessage = orderMessage;
        }

        public OrderMessage OrderMessage => _orderMessage;
    }
}
