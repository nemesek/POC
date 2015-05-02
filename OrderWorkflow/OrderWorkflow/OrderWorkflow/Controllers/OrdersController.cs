using System;
using OrderWorkflow.Domain;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Controllers
{
    public class OrdersController
    {
        private readonly OrderProcessor _orderProcessor;

        public OrdersController(OrderProcessor orderProcessor)
        {
            _orderProcessor = orderProcessor;
        }

        public IOrder ProcessOrder(int clientId)
        {
            var client = new Client(clientId);
            var order = client.CreateNewOrder();
            while (order.Status != OrderStatus.Closed && order.Status != OrderStatus.WithClient)
            {
                order.Save();
                Console.WriteLine("Incoming Request about to be processed.");
                order = ProcessOrder(order);
            }

            return order;
        }

        private IOrder ProcessOrder(IOrder order)
        {
            var updatedOrder = _orderProcessor.ProcessNextStep(order.MakeTransition);
            return updatedOrder;
        }
    }
}
