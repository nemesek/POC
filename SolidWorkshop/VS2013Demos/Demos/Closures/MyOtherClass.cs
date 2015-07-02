using System;

namespace Closures
{
    public class MyOtherClass
    {
        public void DoSomething()
        {
            Console.WriteLine("Doing Something from MyOtherClass");
        }

        // Higher order baby
        public void YouTellMeWhatToDo(Func<int, int> doSomething)
        {
            Console.WriteLine(doSomething(30));
        }

        public void YouTellMeWhatToDo(IGetStuffDone doSomething)
        {
            Console.WriteLine(doSomething.DoSomething(30));
            
        }
    }
}
