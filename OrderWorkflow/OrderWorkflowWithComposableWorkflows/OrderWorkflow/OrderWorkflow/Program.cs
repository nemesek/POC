﻿using System;
using OrderWorkflow.Controllers;

namespace OrderWorkflow
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmsId = 0;
            if (args.Length > 0)
            {
                Int32.TryParse(args[0], out cmsId);    
            }

            if (cmsId < 1)
            {
                var random = new Random();
                cmsId = random.Next(1, 25);
            }

            // app root - DI Container would go here
            Console.WriteLine("About to process order for Cms with ID: {0}", cmsId);
            var controller = new OrdersController();
            controller.ProcessOrder(cmsId);
        }
    }
}
