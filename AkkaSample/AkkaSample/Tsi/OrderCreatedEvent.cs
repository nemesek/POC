namespace AkkaSample.Tsi
{
    public class OrderCreatedEvent
    {
        private readonly OrderMessage _orderMessage;

        public OrderCreatedEvent(OrderMessage orderMessage)
        {
            _orderMessage = orderMessage;
        }

        public OrderMessage OrderMessage => _orderMessage;
    }
}
