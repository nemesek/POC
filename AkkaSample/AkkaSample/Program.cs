using System;
using System.Diagnostics;
using Akka.Actor;
using Akka.Routing;
using AkkaSample.Alee;
using AkkaSample.Alee.IoBound;
using AkkaSample.Alee.MaybeSimpler;
using AkkaSample.Science;
using AkkaSample.Versioning;
using OrderActor = AkkaSample.Versioning.OrderActor;

namespace AkkaSample
{
    public class Program
    {
        private static ActorSystem _orderProcessorActorSystem;

        private static void Main(string[] args)
        {
            _orderProcessorActorSystem = ActorSystem.Create("OrderProcessorActorSystem"); // should this be static?
            //RunSingleActor();
            //CreateActorPool();
            //RunExperiment();
            //AleeFanOut();
            //AleeFanOutPool();
            //AleeSimpler();
            //BlockedDemo();
            BlockedPoolDemo();
            //AsyncDemo();
            //AsyncPoolDemo();
        }

        public static ActorSystem ActorSystem => _orderProcessorActorSystem;

        private static void LoopThroughPool(Action<OrderMessage> routerAction)
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

            var router =
                _orderProcessorActorSystem.ActorOf(Props.Create<OrderActor>().WithRouter(new RoundRobinPool(10)),
                    "some-pool");
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
            SendActorMessage(actorRef, (a, o) =>
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

        private static void SendActorMessage(IActorRef actorRef, Action<IActorRef, OrderMessage> orderAction)
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
            SendActorMessage(actorRef, (a, o) =>
            {
                a.Tell(o);
                Console.ReadKey();
            });
        }

        private static void AleeFanOut()
        {
            var aleeActorProps = Props.Create<AleeFanOutActor>();
            var actorRef = _orderProcessorActorSystem.ActorOf(aleeActorProps, "AleeFanOutActor");
            var command = new CreateOrderCommand(Guid.NewGuid(), new OrderDto(1, "38655", "CHQ"));
            actorRef.Tell(command);

            Console.ReadKey();
        }

        private static void AleeFanOutPool()
        {
            var poolSize = 10;
            var router =
                _orderProcessorActorSystem.ActorOf(
                    Props.Create<AleeFanOutActor>().WithRouter(new RoundRobinPool(poolSize)), "some-pool");

            for (var i = 0; i < poolSize; i++)
            {
                var command = new CreateOrderCommand(Guid.NewGuid(), new OrderDto(1, "38655", "CHQ"));
                router.Tell(command);
            }

            Console.ReadKey();
        }

        private static void AleeSimpler()
        {
            var poolSize = 5;
            var router =
                _orderProcessorActorSystem.ActorOf(
                    Props.Create<AleeSupervisor>().WithRouter(new RoundRobinPool(poolSize)), "some-pool");

            for (var i = 0; i < poolSize; i++)
            {
                var command = new CreateOrderCommand(Guid.NewGuid(), new OrderDto(1, "38655", "CHQ"));
                router.Tell(command);
            }

            Console.ReadKey();
        }

        private static void BlockedDemo()
        {
            var poolSize = 25;
            var actorProps = Props.Create<BlockedSupervisor>();
            var actorRef = _orderProcessorActorSystem.ActorOf(actorProps, "BlockedSupervisor");
            var timer = Stopwatch.StartNew();
            for (var i = 0; i < poolSize; i++)
            {
                var command = new CreateOrderCommand(Guid.NewGuid(), new OrderDto(1, "38655", "CHQ"));
                actorRef.Tell(command);
            }


            Console.ReadKey();
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
        }

        private static void BlockedPoolDemo()
        {
            var poolSize = 100; // 100 around 97s
            var timer = Stopwatch.StartNew();
            var router = _orderProcessorActorSystem.ActorOf(Props.Create<BlockedSupervisor>().WithRouter(new RoundRobinPool(poolSize)), "some-pool");
            for (var i = 0; i < poolSize; i++)
            {
                var command = new CreateOrderCommand(Guid.NewGuid(), new OrderDto(1, "38655", "CHQ"));
                router.Tell(command);
            }

            Console.ReadKey();
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);

        }

        private static void AsyncDemo()
        {
            var poolSize = 25;
            var actorProps = Props.Create<Supervisor>();
            var actorRef = _orderProcessorActorSystem.ActorOf(actorProps, "BlockedSupervisor");
            var timer = Stopwatch.StartNew();
            for (var i = 0; i < poolSize; i++)
            {
                var command = new CreateOrderCommand(Guid.NewGuid(), new OrderDto(1, "38655", "CHQ"));
                actorRef.Tell(command);
            }


            Console.ReadKey();
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
        }

        private static void AsyncPoolDemo()
        {
            var poolSize = 100000; // roundrobin size 7500 is the tops so far ~94-105s
            var timer = Stopwatch.StartNew();
            var router = _orderProcessorActorSystem.ActorOf(Props.Create<Supervisor>().WithRouter(new RoundRobinPool(7500)), "some-pool2");
            for (var i = 0; i < poolSize; i++)
            {
                var command = new CreateOrderCommand(Guid.NewGuid(), new OrderDto(1, "38655", "CHQ"));
                router.Tell(command);
            }

            Console.ReadKey();
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);

        }
    }
}
