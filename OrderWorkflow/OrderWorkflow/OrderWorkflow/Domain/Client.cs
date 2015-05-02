using System;
using OrderWorkflow.Domain.AutoAssign;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain
{
    public class Client
    {
        private readonly int _id;
        private readonly OrderStateTransistor _orderStateTransistor;

        public Client(int id):this(id, new OrderStateTransistor()) {}

        public Client(int id, OrderStateTransistor orderStateTransistor)
        {
            _id = id;
            _orderStateTransistor = orderStateTransistor;
        }

        public IOrder CreateNewOrder()
        {
            return _orderStateTransistor.CreateNewOrder(FindBestVendor(), _id);
        }

        public Func<IOrderWithZipCode, Vendor> FindBestVendor()
        {
            var autoAssign = AutoAssignFactory.CreateAutoAssign(_id);
            return autoAssign.FindBestVendor;
        }

        public Func<IOrderWithZipCode, Vendor> ManualAssign()
        {
            return _ => null;
        }
    }
}
