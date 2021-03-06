﻿using System;
using System.Threading;

namespace AkkaSample.Domain
{
    
    public class OrderProcessor
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

        public OrderDto GetOrder(int orderId)
        {
            Thread.Sleep(RandomGenerator.Next(1,20) * RandomGenerator.Next(50, 150));
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
