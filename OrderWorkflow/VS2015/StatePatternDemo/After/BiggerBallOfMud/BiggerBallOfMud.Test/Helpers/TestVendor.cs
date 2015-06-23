using BiggerBallOfMud.OrderStatuses;

namespace BiggerBallOfMud.Test.Helpers
{
    public class TestVendor : Vendor
    {
        public TestVendor(int orderCount, string zipCode, string name) : base(orderCount, zipCode, name)
        {
        }

        public bool AcceptAction { get; set; }

        public override bool AcceptOrder(Order order)
        {
            return AcceptAction;
        }

        public override bool AcceptOrder(BiggerBallOfMud.After.Order order)
        {
            return AcceptAction;
        }
    }
}
