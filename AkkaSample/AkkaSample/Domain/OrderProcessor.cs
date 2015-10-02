using System;

namespace AkkaSample
{
    public class OrderProcessor
    {
        public void ProcessOrder(OrderDto orderDto)
        {
            Console.WriteLine($"Processing {orderDto.OrderId} the old way");
        }

        public void ProcessOrderNew(OrderDto orderDto)
        {
            Console.WriteLine($"+++++++++++++++++++++++++Processing {orderDto.OrderId} the new way++++++++++++++++++++++++");
        }

        public OrderDto GetOrder(int orderId)
        {
            return new OrderDto(orderId, "38655", "CHQ");
        }

        public OrderDto GetOrderNew(int orderId)
        {
            return new OrderDto(orderId, "75019", "CHQ");
        }

        public void AssignUser(int orderId)
        {
            
        }

        public void ReviewOrder(int orderId)
        {
            
        }

        public void CloseOrder(int orderId)
        {
            
        }
    }
}
