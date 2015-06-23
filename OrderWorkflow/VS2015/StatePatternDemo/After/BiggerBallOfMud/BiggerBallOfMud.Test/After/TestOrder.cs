using System;
using BiggerBallOfMud.After;

namespace BiggerBallOfMud.Test.After
{
    public class TestOrder : Order
    {
        private OrderStatus _status = OrderStatus.Unassigned;
        public TestOrder(int cmsId, string zipCode, Vendor vendor) : base(cmsId, zipCode, vendor)
        {
        }

        public void SetStatus(OrderStatus orderStatus)
        {
            _status = orderStatus;
        }

        public new void AssignVendor(Vendor vendor)
        {
            base.AssignVendor(vendor);
        }
        public override OrderStatus Status => _status;
        public override Order ProcessNextStep()
        {
            // we test these in the individual order status specific classes
            throw new NotImplementedException();
        }
    }
}
