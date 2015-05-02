﻿using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public abstract class Order : IOrder
    {
        private readonly Guid _id;
        private readonly int _clientId;
        private Vendor _vendor;

        protected Order(Guid id, OrderDto orderDto)
        {
            _id = id;
            _clientId = orderDto.ClientId;
            _vendor = orderDto.Vendor;
        }

        public abstract IOrder MakeTransition();
        public abstract OrderStatus Status { get; }

        protected Vendor Vendor{ get { return _vendor; } }
        
        public Guid OrderId { get { return _id; } }
        
        protected void AssignVendor(Vendor vendor)
        {
            // some additional business logic if required
            _vendor = vendor;
        }

        protected Func<OrderDto> MapToOrderDto()
        {
            return () => new OrderDto
            {
                ClientId = _clientId,
                Vendor = _vendor
            };
        }

        public void Save()
        {
            Console.WriteLine("Saving {0} State to DB", Status);
        }
    }
}
