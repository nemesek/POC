using System;
using Akka.Actor;

namespace AkkaSample.CircuitBreaker
{
    public class DbCheckActor : ReceiveActor
    {
        private readonly Random _randomGenerator = new Random();

        public DbCheckActor()
        {
            Receive<DbStatusCheckCommand>(c => HandleDbStatusCheckCommand(c));
        }

        private void HandleDbStatusCheckCommand(DbStatusCheckCommand command)
        {
            Console.WriteLine("Checking status of database.");
            var randomNumber = _randomGenerator.Next();

            if (randomNumber%2 == 0)
            {
                Console.WriteLine("DbStatus == Up");
                StubbedDatabase.IsDown = false;
                Sender.Tell(new DbStatusMessage(true, true));
                return;
            }

            Console.WriteLine("DbStatus == Down");
            StubbedDatabase.IsDown = true;
            Sender.Tell(new DbStatusMessage(false, false));

        }
    }
}
