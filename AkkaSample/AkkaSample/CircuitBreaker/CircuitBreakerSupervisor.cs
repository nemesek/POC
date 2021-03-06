﻿using System;
using System.Threading;
using Akka.Actor;
using AkkaSample.Alee;

namespace AkkaSample.CircuitBreaker
{
    public class CircuitBreakerSupervisor : ReceiveActor, IWithUnboundedStash
    {
        private readonly IActorRef _actorRef;
        private readonly IActorRef _dbStatusCheckActorRef;
        private readonly IActorRef _self;
        private Timer _timer;
        private bool _isCircuitOpen;

        public CircuitBreakerSupervisor()
        {
            CircuitClosedBehavior();

            var actorProps = Props.Create<OrderActor>();
            _actorRef = Context.ActorOf(actorProps, "OrderActor");
            var dbActorProps = Props.Create<DbCheckActor>();
            _dbStatusCheckActorRef = Context.ActorOf(dbActorProps, "DbCheckActor");
            _self = Self;
        }

        public IStash Stash { get; set; }

        private void HandleDbStatusMessage(DbStatusMessage dbStatusMessage)
        {
            if (dbStatusMessage.IsAvailable)
            {
                Stash.UnstashAll();
                Become(CircuitClosedBehavior);
                _actorRef.Tell(dbStatusMessage);
                return;
            }

            Become(CircuitOpenBehavior);
        }

        private void CircuitOpenBehavior()
        {
            if (_isCircuitOpen == false)
            {
                _isCircuitOpen = true;
                _timer = new Timer(CheckDbStatus, null, 15000, 10000);

            }

            Receive<CreateOrderCommand>(c => HandleCreateOrderCommandWhenCircuitOpen(c));
            Receive<DbStatusMessage>(m => HandleDbStatusMessage(m));
        }

        private void CircuitClosedBehavior()
        {
            if (_isCircuitOpen == true) _isCircuitOpen = false;

            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }

            Receive<CreateOrderCommand>(c => HandleCreateOrderCommandWhenCircuitClosed(c));
            Receive<DbStatusMessage>(m => HandleDbStatusMessage(m));
        }

        private void CheckDbStatus(object o)
        {
            _dbStatusCheckActorRef.Tell(new DbStatusCheckCommand("connectionString"), _self);
        }

        private void HandleCreateOrderCommandWhenCircuitOpen(CreateOrderCommand command)
        {
            Console.WriteLine($"Stashing command {command.OrderMessage.CorrelationId} because circuit is open.");
            Stash.Stash();
        }

        private void HandleCreateOrderCommandWhenCircuitClosed(CreateOrderCommand createOrderCommand)
        {
            Console.WriteLine($"Command Received for correlationId {createOrderCommand.OrderMessage.CorrelationId}");
            _actorRef.Tell(createOrderCommand);
        }
    }
}
