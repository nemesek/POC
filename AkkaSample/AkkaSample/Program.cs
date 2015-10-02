using System;
using Akka.Actor;
using Akka.Routing;
using AkkaSample.Science;
using AkkaSample.Tsi;
using OrderActor = AkkaSample.Versioning.OrderActor;

namespace AkkaSample
{
    public class Program
    {
        private static ActorSystem _orderProcessorActorSystem;
        static void Main(string[] args)
        {
            _orderProcessorActorSystem = ActorSystem.Create("OrderProcessorActorSystem"); // should this be static?
            //RunSingleActor();
            //CreateActorPool();
            //RunExperiment();
            //TsiFanOut();
            TsiFanOutPool();
        }

        public static ActorSystem ActorSystem => _orderProcessorActorSystem;

        private static void LoopThroughPool(Action<OrderMessage> routerAction )
        {
            for (var i = 0; i < 10; i++)
            {
                var orderId = i + 1;
                var orderDto = new OrderDto(orderId, "38655", "CHQ");
                var orderMessage = new OrderMessage(Guid.NewGuid(), orderDto);
                routerAction(orderMessage);

            }
        }

        private static void CreateActorPool()
        {

            var router = _orderProcessorActorSystem.ActorOf(Props.Create<OrderActor>().WithRouter(new RoundRobinPool(10)), "some-pool");
            router.Tell(new VersionMessage(true));
            router.Tell(new VersionMessage(true));
            LoopThroughPool(o => router.Tell(o));
            Console.ReadKey();
            Console.WriteLine("Let's do another run");
            LoopThroughPool(o => router.Tell(o));
            Console.ReadKey();
            Console.WriteLine(("Ruh Roh!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"));
            Console.WriteLine("Going to fix these 10 actors");
            Console.ReadKey();
            LoopThroughPool(_ => router.Tell(new VersionMessage(false)));
            LoopThroughPool(o => router.Tell(o));
            Console.ReadKey();
        }

        private static void RunSingleActor()
        {
            
            var actorProps = Props.Create<OrderActor>();
            var actorRef = _orderProcessorActorSystem.ActorOf(actorProps, "OrderActor");
            SendActorMessage(actorRef,(a, o) =>
            {
                a.Tell(o);
                Console.ReadKey();
                a.Tell(new VersionMessage(true));
                a.Tell(o);
                Console.WriteLine("Uh Oh noticed a problem");
                a.Tell(new VersionMessage(false));
                Console.ReadKey();
                a.Tell(o);
                //actorRef.GracefulStop(TimeSpan.Zero);
                Console.ReadKey();
            });
        }

        private static void SendActorMessage(IActorRef actorRef, Action<IActorRef,OrderMessage> orderAction )
        {
            var orderDto = new OrderDto(1, "38655", "CHQ");
            var orderMessage = new OrderMessage(Guid.NewGuid(), orderDto);
            orderAction(actorRef, orderMessage);

        }

        private static void RunExperiment()
        {
            var actorProps = Props.Create<ParentOrderActor>();
            var actorRef = _orderProcessorActorSystem.ActorOf(actorProps, "ParentOrderActor");
            SendActorMessage(actorRef, (a, o) =>
            {
                a.Tell(o);
                Console.ReadKey();
            });

            actorRef.Tell(new ScientificMessage(false));
            Console.WriteLine("Turning off experiment");
            Console.ReadKey();
            SendActorMessage(actorRef, (a,o) =>
            {
                a.Tell(o);
                Console.ReadKey();
            });
        }

        private static void TsiFanOut()
        {
            var tsiActorProps = Props.Create<TsiFanOutActor>();
            var actorRef = _orderProcessorActorSystem.ActorOf(tsiActorProps, "TsiFanOutActor");
            var command = new CreateOrderCommand(Guid.NewGuid(), new OrderDto(1, "38655", "CHQ"));
            actorRef.Tell(command);

            Console.ReadKey();
        }

        private static void TsiFanOutPool()
        {
            var poolSize = 10;
            var router = _orderProcessorActorSystem.ActorOf(Props.Create<TsiFanOutActor>().WithRouter(new RoundRobinPool(poolSize)), "some-pool");

            for (var i = 0; i < poolSize; i++)
            {
                var command = new CreateOrderCommand(Guid.NewGuid(), new OrderDto(1, "38655", "CHQ"));
                router.Tell(command);
            }

            Console.ReadKey();
        }
    }
}
