using System;
using System.Threading.Tasks;

namespace AkkaSample.Domain
{
    public class SlowOrderProcessor
    {
        private static readonly Random RandomGenerator = new Random();
        public void ProcessOrder(OrderDto orderDto)
        {
            Console.WriteLine($"Processing {orderDto.OrderId} the old way");
        }

        public void ProcessOrderNew(OrderDto orderDto)
        {
            Console.WriteLine($"+++++++++++++++++++++++++Processing {orderDto.OrderId} the new way++++++++++++++++++++++++");
        }

        public async Task<OrderDto> GetOrderAsync(int orderId)
        {
            await Task.Delay(1000);
            return new OrderDto(orderId, "38655", "CHQ");
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
