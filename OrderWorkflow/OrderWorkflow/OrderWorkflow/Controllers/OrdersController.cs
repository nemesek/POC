using System;
using OrderWorkflow.Domain;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.WorkflowOrders.Services;

namespace OrderWorkflow.Controllers
{
    public class OrdersController
    {
        private readonly OrderProcessor _orderProcessor;

        public OrdersController(OrderProcessor orderProcessor)
        {
            _orderProcessor = orderProcessor;
        }

        public IWorkflowOrder ProcessOrder(int cmsId)
        {
            var cms = new Cms(cmsId);
            var order = cms.CreateNewOrder();
            while (order.Status != OrderStatus.Closed && order.Status != OrderStatus.WithClient)
            {
                order.Save();
                Console.WriteLine("++++++++++++++Incoming Request about to be processed.+++++++++++++");
                order = ProcessOrder(order);
            }

            return order;
        }

        private IWorkflowOrder ProcessOrder(IWorkflowOrder order)
        {
            var updatedOrder = _orderProcessor.ProcessNextStep(order.MakeTransition);
            return updatedOrder;
        }
    }
}
