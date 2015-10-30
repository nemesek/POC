using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DomainEventsConsole
{
    public class DomainEventPublisher
    {
        public static void StreamEventsViaSubject()
        {

            var stream = new Subject<DomainEvent>();
            stream.Subscribe(o => Console.WriteLine(o.Name));

            for (var i = 1; i < 6; i++)
            {
                stream.OnNext(new DomainEvent {Name = $"Event {i}"});
            }

            Console.ReadKey();
            stream.Dispose();

        }

        public static void StreamEvents()
        {
            int workerThreads;
            int ioThreads;
            ThreadPool.SetMaxThreads(10, 20);
            ThreadPool.GetMaxThreads(out workerThreads, out ioThreads);
            Console.WriteLine($"workerthreads = {workerThreads} iothreads = {1}");
            var eventSource = new EventSource();
            var subscription = eventSource.EventStreamObservable.Subscribe(
                //e => Console.WriteLine($"Publishing event {e.Name}"),
                e => PublishEvent(e),
                ex => Console.WriteLine($"Logging {ex.Message}"),
                () => Console.WriteLine(("Done Sucka")));

            var subscription2 = eventSource.EventStreamObservable.Subscribe(
                    e => Console.WriteLine($"Logging event {e().Name}"),
                    ex => Console.WriteLine($"Foo {ex.Message}"),
                    () => Console.WriteLine(("Bar"))
                );

            var subscription3 = eventSource
                .EventStreamObservable
                .Where(e => e().Name.Contains('2'))
                .Subscribe(
                    e => Console.WriteLine($"!!!!!Logging filtered!!!!! {e().Name}"),
                    ex => Console.WriteLine($"Baz {ex.Message}"),
                    () => Console.WriteLine(("Bar"))
                );

            eventSource.DoWork();
            subscription.Dispose();
            subscription2.Dispose();
            Console.WriteLine("Au revoir");

            Console.ReadKey();

        }

        public static void FooWriter(DomainEvent domainEvent)
        {
            Console.WriteLine(domainEvent.Name);
        }

        private static bool PublishEvent(Func<DomainEvent> domainEvent)
        {
            Task.Delay(2);
            ThreadPool.QueueUserWorkItem(_ => Console.WriteLine($"Publishing {domainEvent().Name}"));
            return true;
        }


        public static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }
    }
}
