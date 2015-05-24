﻿using System;
using OrderWorkflow.Controllers;

namespace OrderWorkflow
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Here we go.");
            // app root - DI Container would go here
            Action<string, int> outputAction = (str, id) => Console.WriteLine("About to {0} order for CMS with ID: {1}", str, id);
            var cmsId = GetCmsId(args);
            var controller = new OrdersController();
            
            if(cmsId > 99)
            {
                if(cmsId == 100)
                {
                    controller.CreateOrder(cmsId);
                    // to allow the event handler to display output
                    // we wouldn't want to re enter the request thread 
                    // in a real app
                    Console.ReadLine(); 
                    return;
                }
                outputAction("edit", cmsId);
                controller.EditOrderAddress(cmsId);
                return;
            }
            
            outputAction("process", cmsId);
            controller.ProcessOrder(cmsId);
        }
        
        static int GetCmsId(string[] args)
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
            
            return cmsId;
        }
    }
}
