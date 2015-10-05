using System;
using Akka.Actor;

namespace AkkaSample.CircuitBreaker
{
    public class DbCheckActor : ReceiveActor
    {
        public DbCheckActor()
        {
            Receive<DbStatusCheckCommand>(c => HandleDbStatusCheckCommand(c));
        }

        private void HandleDbStatusCheckCommand(DbStatusCheckCommand command)
        {
            var sender = Sender;
            Console.WriteLine("Checking status of database.");
            StubbedDatabase.IsDown = false;
            Sender.Tell(new DbStatusMessage(true, true));
        }
    }
}
