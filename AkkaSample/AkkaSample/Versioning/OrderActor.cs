using System;
using Akka.Actor;

namespace AkkaSample.Versioning
{
    public class OrderActor : ReceiveActor
    {
        public OrderActor()
        {
            Receive<OrderMessage>(o => ProcessOrderOld(o.OrderDto));
            Receive<VersionMessage>(v => SetUpVersion(v));
        }

        protected override void PreStart()
        {
            Console.WriteLine("Starting Up");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
        }

        protected override void PostStop()
        {
            Console.WriteLine("Stopped");
            base.PostStop();
        }

        private static void ProcessOrderOld(OrderDto orderDto)
        {
            var orderProcessor = new OrderProcessor();
            orderProcessor.ProcessOrder(orderDto);
        }

        private static void ProcessOrderNew(OrderDto orderDto)
        {
            var orderProcessor = new OrderProcessor();
            orderProcessor.ProcessOrderNew(orderDto);
        }

        private void SetUpVersion(VersionMessage versionMessage)
        {
            if (versionMessage.UseNew)
            {
                Become(BecomeNew);
                return;
            }

            Become(BecomeOld);

        }

        private void BecomeNew()
        {
            Receive<OrderMessage>(o => ProcessOrderNew(o.OrderDto));
            Receive<VersionMessage>(v => SetUpVersion(v));
        }

        private void BecomeOld()
        {
            Receive<OrderMessage>(o => ProcessOrderOld(o.OrderDto));
            Receive<VersionMessage>(v => SetUpVersion(v));

        }
    }
}
