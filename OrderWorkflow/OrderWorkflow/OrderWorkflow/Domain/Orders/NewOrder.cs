using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.Orders
{
    public class NewOrder : IOrderWithZipCode
    {
        private readonly Guid _id;
        private readonly Func<IOrderWithZipCode,Vendor> _assignFunc;
        private readonly Func<Guid, OrderDto, bool,IOrder> _transitionFunc;
        private readonly string _zipCode;
        private readonly int _clientId;
        private readonly OrderDto _orderDto;

        public NewOrder(Guid id, OrderDto orderDto)
        {
            _id = id;
            _assignFunc = orderDto.AssignFunc;
            _transitionFunc = orderDto.ConditionalTransitionFunc;
            _zipCode = orderDto.ZipCode;
            _clientId = orderDto.ClientId;
            _orderDto = orderDto;
        }
        public IOrder MakeTransition()
        {
            var vendorToAssign = _assignFunc(this);
            var vendorAssigned = vendorToAssign != null;
            _orderDto.Vendor = vendorToAssign;
            var assignedOrder = _transitionFunc(_id, _orderDto,vendorAssigned);
            return assignedOrder;
        }

        public OrderStatus Status { get { return OrderStatus.New; } }
        public Guid OrderId { get { return _id; }}
        public string ZipCode { get { return _zipCode; } }
        public int ClientId { get { return _clientId; } }
        public void Save()
        {
            Console.WriteLine("Saving New State to DB");
        }
    }
}
