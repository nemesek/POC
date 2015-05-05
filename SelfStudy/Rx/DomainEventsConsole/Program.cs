﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DomainEventsConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            // http://www.introtorx.com/Content/v1.0.10621.0/04_CreatingObservableSequences.html
            //SimpleSubjectExample();
            //BlockingAndNonBlockingExample();
            //FixNonBlockingEvenDrivenExample();
            //UnfoldExample();
            //RangeExample();

            // From delegates examples
            //StartActionExample(); // test
            StartFuncExample();
        }

        static void StartFuncExample()
        {
            StartFunc();
            LoopXSleepYAndRead(20, 50);
        }
        static void StartFunc()
        {
            var start = Observable.Start(() =>
            {
                Console.Write("Working away");
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.Write(".");
                }
                return "Published value";
            });
            start.Subscribe(
            Console.WriteLine,
            () => Console.WriteLine("Action completed"));
        }

        static void StartActionExample()
        {
            StartAction();
            LoopXSleepYAndRead(20,50);
        }

        private static void LoopXSleepYAndRead(int loop, int sleep)
        {
            for (var i = 0; i < loop; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(sleep);
            }
            Console.Read();
        }

        static void StartAction()
        {
            var start = Observable.Start(() =>
            {
                Console.Write("Working Away");
                for (var i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.Write(".");
                }
            });

            start.Subscribe(
                unit => Console.WriteLine("Unit published."),
                () => Console.WriteLine("Action completed."));
        }
        
        static void BlockingAndNonBlockingExample()
        {
            var q = BlockingMethod();
            Console.WriteLine(q.TakeLast(1).Single());
            Console.WriteLine("Beyond Blocking.");
            var r = NonBlocking();
            Console.WriteLine(r.TakeLast(1).Single());
            Console.WriteLine("Beyond Non Blocking");
               
        }
        static void FixNonBlockingEvenDrivenExample()
        {
            FixedNonBlockingEventDriven();
            for (var i = 0; i < 20; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(300);
            }
        }
        static void UnfoldExample()
        {
            var naturalNumbers = Unfold(1, i => i + 1);
            Console.WriteLine("1st 10 Natural numbers");
            foreach (var naturalNumber in naturalNumbers.Take(20))
            {
                Console.WriteLine(naturalNumber);
            }

        }

        static void RangeExample()
        {
            var reactiveNumbers = Range(1, 20);
            reactiveNumbers.Subscribe(Console.WriteLine, () => Console.WriteLine("Done Sucka"));
        }

        static void SimpleSubjectExample()
        {
            var values = new Subject<int>();
            values.Subscribe(
                    value => Console.WriteLine("subscription received {0}.", value),
                    ex => Console.WriteLine("Caught an exception: {0}", ex)
                );

            values.OnNext(0);
            values.OnError(new Exception("Dummy Exception."));
        }

        private static IObservable<string> BlockingMethod()
        {
            var subject = new ReplaySubject<string>();
            subject.OnNext("a");
            subject.OnNext("b");
            subject.OnCompleted();
            Thread.Sleep(1000);
            return subject;
        }

        private static IObservable<string> NonBlocking()
        {
            return Observable.Create<string>(
                observer =>
                {
                    observer.OnNext("a");
                    observer.OnNext("b");
                    observer.OnCompleted();
                    Thread.Sleep(1000);
                    return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed."));
                });
        }

        //Example code only
        public static void NonBlockingEventDriven()
        {
            var observable = Observable.Create<string>(
            o =>
            {
                var timer = new System.Timers.Timer {Interval = 1000};
                timer.Elapsed += (s, e) => o.OnNext("tick");
                timer.Elapsed += OnTimerElapsed;
                timer.Start();
                //return Disposable.Empty;
                return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed."));
            });

            var subscription = observable.Subscribe(Console.WriteLine);
            Console.ReadLine();
            subscription.Dispose();
        }

        public static void FixedNonBlockingEventDriven()
        {
            var observable = Observable.Create<string>(
            o =>
            {
                var timer = new System.Timers.Timer { Interval = 1000 };
                timer.Elapsed += (s, e) => o.OnNext("tick");
                timer.Elapsed += OnTimerElapsed;
                timer.Start();
                return timer;
            });

            var subscription = observable.Subscribe(Console.WriteLine);
            Console.ReadLine();
            subscription.Dispose();
        }

        public static IObservable<string> NonBlockingEventDriven2()
        {
            var observable = Observable.Create<string>(
            o =>
            {
                var timer = new System.Timers.Timer { Interval = 1000 };
                timer.Elapsed += (s, e) => o.OnNext("tick");
                timer.Elapsed += OnTimerElapsed;
                timer.Start();
                //return Disposable.Empty;
                return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed."));
            });

            return observable;
        }

        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }

        private static IEnumerable<T> Unfold<T>(T seed, Func<T, T> accumulator)
        {
            var nextValue = seed;
            while (true)
            {
                yield return nextValue;
                nextValue = accumulator(nextValue);
            }
        }

        //Example code only
        public static IObservable<int> Range(int start, int count)
        {
            var max = start + count;
            return Observable.Generate(
            start,
            value => value < max,
            value => value + 1,
            value => value);
        }
    }
}
