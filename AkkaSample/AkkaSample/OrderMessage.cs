using System;

namespace AkkaSample
{
    public class OrderMessage
    {

        private readonly Guid _correlationId;
        private readonly OrderDto _orderDto;

        public OrderMessage(Guid correlationId, OrderDto orderDto)
        {
            _correlationId = correlationId;
            _orderDto = orderDto;
        }


        public Guid CorrelationId => _correlationId;

        public OrderDto OrderDto => _orderDto;
    }
}
