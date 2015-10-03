using System;
using Akka.Actor;
using AkkaSample.Domain;

namespace AkkaSample.Science
{
    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<OrderMessage>(o => ProcessOrderOld(o.OrderDto));
            Receive<ScientificMessage>(m => SetUpExperiment(m));
        }

        private static void ProcessOrderOld(OrderDto orderDto)
        {
            var orderProcessor = new OrderProcessor();
            orderProcessor.GetOrder(orderDto.OrderId);
            Console.WriteLine("Doing it old school");
        }


        private void SetUpExperiment(ScientificMessage scientificMessage)
        {
            if (scientificMessage.IsExperiment)
            {
                Become(BecomeScientific);
                return;
            }

            Become(BecomeUnScientific);

        }

        private void BecomeScientific()
        {
            Receive<OrderMessage>(o => RunExperiment(o));
            Receive<ScientificMessage>(m => SetUpExperiment(m));
        }

        private void BecomeUnScientific()
        {
            Receive<OrderMessage>(o => ProcessOrderOld(o.OrderDto));
            Receive<ScientificMessage>(m => SetUpExperiment(m));
        }

        private void RunExperiment(OrderMessage orderMessage)
        {
            var orderProcessor = new OrderProcessor();
            Console.WriteLine($"Logging experiment for {orderMessage.CorrelationId}");
            var result1 = orderProcessor.GetOrder(orderMessage.OrderDto.OrderId);
            var result2 = orderProcessor.GetOrderNew(orderMessage.OrderDto.OrderId);
            var timeSpan1 = TimeSpan.FromMilliseconds(100);
            var timeSpan2 = TimeSpan.FromMilliseconds(40);
            Sender.Tell(new ScientificResult(orderMessage.CorrelationId, result1, result2, timeSpan1, timeSpan2));
        }
    }
}
