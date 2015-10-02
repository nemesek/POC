namespace AkkaSample.Tsi
{
    public class OrderReviewedEvent
    {
        private readonly OrderMessage _orderMessage;

        public OrderReviewedEvent(OrderMessage orderMessage)
        {
            _orderMessage = orderMessage;
        }

        public OrderMessage OrderMessage => _orderMessage;
    }
}
