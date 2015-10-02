using System;
using Akka.Actor;

namespace AkkaSample.Science
{
    public class ParentOrderActor : ReceiveActor
    {
        private IActorRef _actorRef;
        public ParentOrderActor()
        {
            Receive<OrderMessage>(o => RunExperiment(o));
            Receive<ScientificResult>(r => ViewResults(r));
            Receive<ScientificMessage>(m => StopExperiment(m));
            var actorProps = Props.Create<OrderActor>();
            //_actorRef = Program.ActorSystem.ActorOf(actorProps, "ScientificOrderActor");
            _actorRef = Context.ActorOf(actorProps, "ScientificOrderActor");
            _actorRef.Tell(new ScientificMessage(true));
        }

        private void RunExperiment(OrderMessage orderMessage)
        {
            _actorRef.Tell(orderMessage);
        }

        private void StopExperiment(ScientificMessage scientificMessage)
        {
            _actorRef.Tell(new ScientificMessage(false));
        }

        private static void ViewResults(ScientificResult scientificResult)
        {
            var control = scientificResult.ControlDto;
            var experiement = scientificResult.ExperimentalDto;
            var result = true;

            if (control.OrderId != experiement.OrderId)
            {
                Console.WriteLine("OrderId failed");
                result = false;
            }

            if (control.PortId != experiement.PortId)
            {
                Console.WriteLine("PortId failed");
                result = false;
            }

            if (control.Zip != experiement.Zip)
            {
                Console.WriteLine(("Zip failed"));
                result = false;
            }

            var controlTime = scientificResult.ControlTimeSpan.Milliseconds;
            var experimentTime = scientificResult.ExperimentalTimeSpan.Milliseconds;
            var timeDiff = controlTime - experimentTime;
            float timeSpanDiff = Math.Abs(timeDiff);
            var percentageChange = timeSpanDiff/scientificResult.ControlTimeSpan.Milliseconds * 100;
            var output = timeDiff > 0 ? "faster" : "slower";
            Console.WriteLine($"Experiment for CorrelationId {scientificResult.CorrelationId} completed");
            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Speed diff: {percentageChange} percent {output}");
        }
    }
}
