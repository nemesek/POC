using System;
using System.Threading;
using OrderWorkflow.Domain;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.OrderEdit;
using OrderWorkflow.Domain.Common;

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
            var order = cms.GetEditableOrder();
            var address = order.GetAddress();
            var newAddress = new Address("Dallas", "TX", "75019", "Elm", "456");
            order.UpdateAddress(newAddress);
        }
        
        public void CreateOrder(int cmsId)
        {
            var cms = new Cms(cmsId);
            var order = cms.CreateOrder();
        }

    }
}
