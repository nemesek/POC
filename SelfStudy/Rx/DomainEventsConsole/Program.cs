namespace DomainEventsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //SimpleSubjectExample();
            //BlockingAndNonBlockingExample();
            //Sequences.BlockingMethod();
            //Sequences.NonBlocking();
            //Sequences.NonBlockingEventDriven();
            //Sequences.FixedNonBlockingEventDriven();//FixNonBlockingEvenDrivenExample();
            //UnfoldExample();
            //RangeExample();

            // From delegates examples
            //StartActionExample(); // test
            //StartFuncExample();
            //FilterExample();
            //DistinctExample();
            //DistintUntilChangedExample();
            //SkipTakeExample();
            //SkipUntilAndTakeUntilExample();
            // AllExample();
            //ContainsExample();
            //Sequences.DefaultIfEmptyExample();
            //Sequences.CountExample();
            //Sequences.MinMaxSumAverageExample();
            //Sequences.AggregationExample();
            //Sequences.ScanExample();
            //Sequences.GroupByExample();
            //Sequences.SelectExample();
            //DomainEventPublisher.StreamEventsViaSubject();
            DomainEventPublisher.StreamEvents();
        }
    }
}
