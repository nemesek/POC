using System;

namespace AkkaSample.Alee.MaybeSimpler.Commands
{
    public class ReviewOrderCommand
    {
        private readonly OrderMessage _orderMessage;

        public ReviewOrderCommand(Guid correlationId, OrderDto orderDto)
        {
            _orderMessage = new OrderMessage(correlationId, orderDto);
        }

        public ReviewOrderCommand(OrderMessage orderMessage)
        {
            _orderMessage = orderMessage;
        }

        public OrderMessage OrderMessage => _orderMessage;
    }
}
