using MockCms.Domain;

namespace MockCms.Infrastructure.Controllers
{
    public class OrdersController
    {
        public string ProcessOrder()
        {
            var cms = new Cms();
            cms.CreateOrder();
            return null;
        }
    }
}
