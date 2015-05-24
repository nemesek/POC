using System;
using System.Threading;
using OrderWorkflow.Domain;
using OrderWorkflow.Domain.Common;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Controllers
{
    public class OrdersController
    {
        public IWorkflowOrder ProcessOrder(int cmsId)
        {
            var cms = new Cms(cmsId);
            var order = cms.GetWorkflowOrder();
            while (order.Status != OrderStatus.Closed)
            {
                Thread.Sleep(1000);
                Console.WriteLine("++++++++++++++Incoming Request about to be processed.+++++++++++++");
                order = order.ProcessNextStep();
            }

            return order;
        }
        
        public void EditOrderAddress(int cmsId)
        {
            var cms = new Cms(cmsId);
            var newAddress = new Address("Dallas", "TX", "75019", "Elm", "456");
            cms.EditOrderAddress(newAddress);
        }
        
        public void CreateOrder(int cmsId)
        {
            var cms = new Cms(cmsId);
            var order = cms.CreateOrder();
            Console.WriteLine("Mapping order {0} to DTO", order.Id);
        }

    }
}
