namespace MockCms.Domain
{
    public class Cms
    {
        public void CreateOrder()
        {
            // // add CMS biz logic that needs to happen prior to creating an order
            var order = new Order();
            order.CreateOrder();
            // add cms biz logic that needs to happen after order creation
        }
    }
}
