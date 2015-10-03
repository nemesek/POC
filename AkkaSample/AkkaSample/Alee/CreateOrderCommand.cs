using System;

namespace AkkaSample.Alee
{
    public class CreateOrderCommand
    {
        private readonly OrderMessage _orderMessage;

        public CreateOrderCommand(Guid correlationId, OrderDto orderDto)
        {
            _orderMessage = new OrderMessage(correlationId, orderDto);
        }

        public OrderMessage OrderMessage => _orderMessage;
    }
}
