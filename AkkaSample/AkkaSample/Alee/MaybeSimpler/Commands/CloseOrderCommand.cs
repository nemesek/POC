using System;

namespace AkkaSample.Alee.MaybeSimpler.Commands
{
    public class CloseOrderCommand
    {
        private readonly OrderMessage _orderMessage;

        public CloseOrderCommand(Guid correlationId, OrderDto orderDto)
        {
            _orderMessage = new OrderMessage(correlationId, orderDto);
        }

        public CloseOrderCommand(OrderMessage orderMessage)
        {
            _orderMessage = orderMessage;
        }

        public OrderMessage OrderMessage => _orderMessage;
    }
}
