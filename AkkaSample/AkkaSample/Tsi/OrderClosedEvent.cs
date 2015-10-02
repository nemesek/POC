namespace AkkaSample.Tsi
{
    public class OrderClosedEvent
    {
        private readonly OrderMessage _orderMessage;

        public OrderClosedEvent(OrderMessage orderMessage)
        {
            _orderMessage = orderMessage;
        }

        public OrderMessage OrderMessage => _orderMessage;
    }
}
