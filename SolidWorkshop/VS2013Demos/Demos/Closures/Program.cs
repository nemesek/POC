using System;

namespace Closures
{
    class Program
    {
        static void Main(string[] args)
        {
            // static field
            Func<int, int> squareIt = x => x * x;
            Console.WriteLine(squareIt(4));

            // display class due to closure
            var myLocal = 5;
            var myOtherLocal = 10;
            Func<int, int> addWithLocalVariableMyLocal = x => x + myLocal;
            Console.WriteLine(addWithLocalVariableMyLocal(10));
            myLocal = myOtherLocal;
            Console.WriteLine(addWithLocalVariableMyLocal(10));

            // pass func (display class) to other method sig
            var otherClass = new MyOtherClass();
            otherClass.YouTellMeWhatToDo(addWithLocalVariableMyLocal);


        }
    }

    public class MyOtherClass
    {
        public void DoSomething()
        {
            Console.WriteLine("Doing Something from MyOtherClass");
        }

        public void YouTellMeWhatToDo(Func<int, int> doSomething)
        {
            Console.WriteLine(doSomething(30));
        }
    }
}
