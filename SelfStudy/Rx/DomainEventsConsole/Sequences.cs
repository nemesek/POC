using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Timers;

namespace DomainEventsConsole
{
    // http://www.introtorx.com/Content/v1.0.10621.0/04_CreatingObservableSequences.html
    class Sequences
    {

        public static void SelectExample()
        {
            var source = Observable.Range(0, 5);
            source.Select(i => i + 3).Dump("+3");
        }
        public static void GroupByExample()
        {
            var source = Observable.Range(0, 20);
            //var source = Observable.Interval(TimeSpan.FromMilliseconds(100));
            source.Take(10);
            var group = source
                .GroupBy(i => i%3)
                .SelectMany(grp => grp.Average().Select(value => new {grp.Key, value}));

            group.Dump("Group1");

            Console.Read();
        }

        public static void ScanExample()
        {
            var numbers = new Subject<int>();
            var scan = numbers.Scan(0, (acc, current) => acc + current);
            numbers.Dump("numbers");
            scan.Dump("scan");
            numbers.OnNext(1);
            numbers.OnNext(2);
            numbers.OnNext(3);
            numbers.OnCompleted();
        }
        public static void AggregationExample()
        {
            var numbers = Observable.Range(0, 10);
            var sum = numbers.Aggregate(0, (acc, currentValue) => acc + currentValue);
            var count = numbers.Aggregate(0, (acc, _) => acc + 1);
            sum.Dump("sum");
            count.Dump("count");


        }
        public static void MinMaxSumAverageExample()
        {
            var numbers = new Subject<int>();
            numbers.Dump("numbers");
            numbers.Min().Dump("Min");
            numbers.Average().Dump("Average");
            numbers.OnNext(1);
            numbers.OnNext(2);
            numbers.OnNext(3);
            numbers.OnCompleted();
            numbers.Max().Dump("Max");
        }
        public static void CountExample()
        {
            var numbers = Observable.Range(0, 3);
            numbers.Dump("numbers");
            numbers.Count().Dump("count");
        }
        public static void DefaultIfEmptyExample()
        {
            var subject = new Subject<int>();
            subject.Subscribe(Console.WriteLine, () => Console.WriteLine("Subject Completed"));
            var defaultIfEmpty = Randomizer.RandomYes() ? subject.DefaultIfEmpty(42) : subject.DefaultIfEmpty();
            defaultIfEmpty.Subscribe(b => Console.WriteLine("defaultIfEmpty value: {0}", b),
                () => Console.WriteLine("defaultIfEmpty completed"));
            if (Randomizer.RandomYes())
            {
                subject.OnNext(1);
                subject.OnNext(2);
                subject.OnNext(3);
            }
            subject.OnCompleted();
        }

        public static void ContainsExample()
        {
            var subject = new Subject<int>();
            subject.Subscribe(Console.WriteLine, () => Console.WriteLine("Subject Completed"));
            var contains = subject.Contains(2);
            contains.Subscribe(b => Console.WriteLine("Contains the value 2? {0}", b),
                () => Console.WriteLine("Contains completed."));
            subject.OnNext(1);
            if (Randomizer.RandomYes()) subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();
        }

        public static void AllExample()
        {
            var subject = new Subject<int>();
            subject.Subscribe(Console.WriteLine, () => Console.WriteLine("Subject Completed."));
            var all = subject.All(i => i < 5);
            all.Subscribe(b => Console.WriteLine("All values less than 5? {0}", b));
            subject.OnNext(1);
            subject.OnNext(2);
            if(Randomizer.RandomYes()) subject.OnNext(6);
            subject.OnNext(3);
            subject.OnCompleted();
        }

        public static void SkipUntilAndTakeUntilExample()
        {
            Action<int, Subject<int>, Subject<Unit>> rangeAction = (i, sub, otherSub) =>
            {
                sub.OnNext(i);
                if (i == 3) otherSub.OnNext(Unit.Default);
            };
            Console.WriteLine("SkipUntil");
            var subject = new Subject<int>();
            var otherSubject = new Subject<Unit>();
            subject.SkipUntil(otherSubject).Subscribe(Console.WriteLine, () => Console.WriteLine("Skip Until Completed"));
            Enumerable.Range(1, 8).ToList().ForEach((i) => rangeAction(i, subject, otherSubject));
            subject.OnCompleted();

            Console.WriteLine("TakeUntil");
            var takeSubject = new Subject<int>();
            var otherTakeSubject = new Subject<Unit>();
            takeSubject.TakeUntil(otherTakeSubject).Subscribe(Console.WriteLine, () => Console.WriteLine("Take Until Completed."));
            Enumerable.Range(1, 8).ToList().ForEach((i => rangeAction(i, takeSubject, otherTakeSubject)));
            takeSubject.OnCompleted();

        }

        public static void SkipTakeExample()
        {
            Action completedAction = () => Console.WriteLine("Infinite Completed.");
            Action<int> infiniteIntervalAction = i => Observable.Interval(TimeSpan.FromMilliseconds(100))
                .Take(i)
                .Subscribe(Console.WriteLine, completedAction);

            Observable.Range(0, 10).Skip(3).Subscribe(Console.WriteLine, () => Console.WriteLine("Skip Completed"));
            Observable.Range(0, 10).Take(3).Subscribe(Console.WriteLine, () => Console.WriteLine("Take Completed."));
            Console.WriteLine("Take can work against Infinite sequences.");

            infiniteIntervalAction(20);
            Thread.Sleep(1000);
            infiniteIntervalAction(100);
            Console.Read();
        }

        public static void DistinctExample()
        {
            Action<string,int> displayAction = (s,i) => Console.WriteLine("{0} : {1}", s,i);
            var subject = new Subject<int>();
            var distinct = subject.Distinct();
            subject.Subscribe(
                (i) => displayAction("Subject On Next",i),
                () => Console.WriteLine("Subject Completed."));

            distinct.Subscribe(
                (i) => displayAction("Distinct On Next", i),
                () => Console.WriteLine("Distinct Completed."));

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(4);
            subject.OnCompleted();
        }

        public static void DistintUntilChangedExample()
        {
            Action<string, int> displayAction = (s, i) => Console.WriteLine("{0} : {1}", s, i);
            var subject = new Subject<int>();
            var distinct = subject.DistinctUntilChanged();
            subject.Subscribe((i) => displayAction("Subject On Next", i), () => Console.WriteLine("Subject Completed"));
            distinct.Subscribe((i) => displayAction("Distinct Until Changed On Next", i), () => Console.WriteLine("Distinct Until Changed Completed."));

            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(4);
            subject.OnCompleted();

        }

        public static void FilterExample()
        {
            var evenNumbers = Observable.Range(0, 10)
                .Where(i => i % 2 == 0)
                .Subscribe(
                    Console.WriteLine,
                    () => Console.WriteLine("Completed"));
        }

        public static void StartFuncExample()
        {
            StartFunc();
            LoopXSleepYAndRead(20, 50);
        }

        public static void StartFunc()
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

        public static void StartActionExample()
        {
            StartAction();
            LoopXSleepYAndRead(20,50);
        }

        public static void LoopXSleepYAndRead(int loop, int sleep)
        {
            for (var i = 0; i < loop; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(sleep);
            }
            Console.Read();
        }

        public static void StartAction()
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

        public static void BlockingAndNonBlockingExample()
        {
            var q = BlockingMethod();
            Console.WriteLine(Observable.TakeLast<string>(q, 1).Single());
            Console.WriteLine("Beyond Blocking.");
            var r = NonBlocking();
            Console.WriteLine(Observable.TakeLast<string>(r, 1).Single());
            Console.WriteLine("Beyond Non Blocking");
               
        }

        public static void FixNonBlockingEvenDrivenExample()
        {
            FixedNonBlockingEventDriven();
            for (var i = 0; i < 20; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(300);
            }
        }

        public static void UnfoldExample()
        {
            var naturalNumbers = Unfold(1, i => i + 1);
            Console.WriteLine("1st 10 Natural numbers");
            foreach (var naturalNumber in Enumerable.Take<int>(naturalNumbers, 20))
            {
                Console.WriteLine(naturalNumber);
            }

        }

        public static void RangeExample()
        {
            var reactiveNumbers = Range(1, 20);
            ObservableExtensions.Subscribe<int>(reactiveNumbers, Console.WriteLine, () => Console.WriteLine("Done Sucka"));
        }

        public static void SimpleSubjectExample()
        {
            var values = new Subject<int>();
            values.Subscribe(
                value => Console.WriteLine("subscription received {0}.", value),
                ex => Console.WriteLine("Caught an exception: {0}", ex)
                );

            values.OnNext(0);
            values.OnError(new Exception("Dummy Exception."));
        }

        public static IObservable<string> BlockingMethod()
        {
            var subject = new ReplaySubject<string>();
            subject.OnNext("a");
            subject.OnNext("b");
            subject.OnCompleted();
            Thread.Sleep(1000);
            return subject;
        }

        public static IObservable<string> NonBlocking()
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

            var subscription = ObservableExtensions.Subscribe<string>(observable, Console.WriteLine);
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

            var subscription = ObservableExtensions.Subscribe<string>(observable, Console.WriteLine);
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

        public static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }

        public static IEnumerable<T> Unfold<T>(T seed, Func<T, T> accumulator)
        {
            var nextValue = seed;
            while (true)
            {
                yield return nextValue;
                nextValue = accumulator(nextValue);
            }
        }

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