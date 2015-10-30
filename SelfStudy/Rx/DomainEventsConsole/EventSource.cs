using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DomainEventsConsole
{
    public class EventSource
    {
        private readonly Subject<Func<DomainEvent>> _eventStream  = new Subject<Func<DomainEvent>>();

        public IObservable<Func<DomainEvent>> EventStreamObservable => _eventStream;

        public bool DoWork()
        {
            for (var i = 1; i < 6; i++)
            {
                Thread.Sleep(1000);
                if (i == 4) _eventStream.OnError(new Exception("FooBar"));
                _eventStream.OnNext(() => new DomainEvent { Name = $"Event {i}" });
            }

            _eventStream.OnCompleted();
            return true;
        }
    }
}
