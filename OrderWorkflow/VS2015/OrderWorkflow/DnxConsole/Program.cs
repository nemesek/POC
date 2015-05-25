using System;
using System.Collections.Generic;
using System.Threading;
using DnxConsole.Controllers;

namespace DnxConsole
{
    public class Program
    {
        public void Main(string[] args)
        {
            // app root - DI Container would go here
            Console.WriteLine("Here we go from Application Root.");
            Action<string, int> outputAction = (str, id) => Console.WriteLine("About to {0} order for CMS with ID: {1}", str, id);
            var cmsId = GetCmsId(args);
            var demoMode = IsDemo(args);
            if (demoMode) Console.WriteLine("Running in demo mode");
            var controller = new OrdersController();

            if (cmsId > 99)
            {
                if (cmsId == 100)
                {
                    outputAction("create", cmsId);
                    controller.CreateOrder(cmsId);
                    Reset();
                    return;
                }

                outputAction("edit", cmsId);
                controller.EditOrderAddress(cmsId);
                Reset();
                return;
            }

            outputAction("process", cmsId);
            controller.ProcessOrder(cmsId, demoMode);
            Reset();
        }

        static void Reset()
        {
            // to allow the event handler to display output
            // we wouldn't want to re enter the request thread 
            // in a real app
            Thread.Sleep(1000);
            Console.ResetColor();
            Console.WriteLine("Au Revoir");
            Console.ReadLine();
        }

        static int GetCmsId(IReadOnlyList<string> args)
        {
            var cmsId = 0;
            if (args.Count > 0)
            {
                int.TryParse(args[0], out cmsId);
            }

            if (cmsId >= 1) return cmsId;
            var random = new Random();
            cmsId = random.Next(1, 25);

            return cmsId;
        }

        static bool IsDemo(IReadOnlyList<string> args)
        {
            var isDemo = false;
            if (args.Count > 1)
            {
                isDemo = string.Compare(args[1].Trim(), "demo", StringComparison.OrdinalIgnoreCase) == 0;
            }

            return isDemo;
        }
    }
}
