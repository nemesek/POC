using System;

namespace BiggerBallOfMud
{
    class Program
    {
        static void Main(string[] args)
        {
            // How to incorporate a new status?
            // How to change business logic?
            // How to change assignment logic?
            // How to redefine a workflow?
            var cmsId = 0;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out cmsId);
            }

            if (cmsId < 1)
            {
                var random = new Random();
                cmsId = random.Next(1, 25);
            }

            // app root - DI Container would go here
            Console.WriteLine("About to process order for Cms with ID: {0}", cmsId);
            var controller = new OrdersController();
            var order = controller.ProcessOrder(cmsId);
            order.Save();
            Console.ReadLine();
        }
    }
}
