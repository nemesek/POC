using System;
using System.Threading;
using OrderWorkflow.Domain;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Controllers
{
    public class OrdersController
    {
        public IWorkflowOrder ProcessOrder(int cmsId)
        {
            var cms = new Cms(cmsId);
            var order = cms.CreateNewOrder();
            while (order.Status != OrderStatus.Closed)
            {
                Thread.Sleep(1000);
                Console.WriteLine("++++++++++++++Incoming Request about to be processed.+++++++++++++");
                order = order.ProcessNextStep();
            }

            return order;
        }

    }
}
