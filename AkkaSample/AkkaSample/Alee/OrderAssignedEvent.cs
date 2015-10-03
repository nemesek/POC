namespace AkkaSample.Alee
{
    public class OrderAssignedEvent
    {
        private readonly OrderMessage _orderMessage;

        public OrderAssignedEvent(OrderMessage orderMessage)
        {
            _orderMessage = orderMessage;
        }

        public OrderMessage OrderMessage => _orderMessage;
    }
}
