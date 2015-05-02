using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class AssignedOrder : IOrder
    {
        private readonly Vendor _vendor;
        private readonly Guid _id;
        private readonly Func<Guid, OrderDto, bool, IOrder> _transitionFunc;
        private readonly int _clientId;
        private readonly OrderDto _orderDto;

        public AssignedOrder(Guid id, OrderDto orderDto)
        {
            _id = id;
            _vendor = orderDto.Vendor;
            _transitionFunc = orderDto.ConditionalTransitionFunc;
            _clientId = orderDto.ClientId;
            _orderDto = orderDto;
        }

        public IOrder MakeTransition()
        {
            _vendor.SendMeNotification(this);
            var vendorAccepted = _vendor.AcceptOrder(this);
            return _transitionFunc(_id, _orderDto,vendorAccepted);
        }

        public OrderStatus Status { get { return OrderStatus.Assigned; } }
        public Guid OrderId { get { return _id; } }
        public int ClientId { get { return _clientId; } }
        public void Save()
        {
            Console.WriteLine("Saving Assigned State to DB");
        }
    }
}
