using MockCms.Infrastructure.Controllers;

namespace MockCms
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new OrdersController();
            controller.ProcessOrder();
        }
    }
}
