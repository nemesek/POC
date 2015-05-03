using System;
using OrderWorkflow.Domain.AutoAssign;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain
{
    public class Client
    {
        private readonly int _id;
        private readonly OrderTransitioner _orderTransitioner;

        public Client(int id):this(id, new OrderTransitioner()) {}

        public Client(int id, OrderTransitioner orderTransitioner)
        {
            _id = id;
            _orderTransitioner = orderTransitioner;
        }

        public IWorkflowOrder CreateNewOrder()
        {
            return _orderTransitioner.CreateNewOrder(FindBestVendor(), _id);
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
