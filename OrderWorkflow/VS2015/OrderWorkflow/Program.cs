using System;
using OrderWorkflow.Controllers;

namespace OrderWorkflow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var controller = new OrdersController();
            controller.ProcessOrder();            
        }
    }
}
