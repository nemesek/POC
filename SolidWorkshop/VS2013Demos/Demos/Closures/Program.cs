using System;

namespace Closures
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> squareIt = x => x * x;
            Console.WriteLine(squareIt(4));
            var myLocal = 5;
            Func<int, int> addWithLocalVariableMyLocal = x => x + myLocal;
            Console.WriteLine(addWithLocalVariableMyLocal(10));
            myLocal = 10;
            Console.WriteLine(addWithLocalVariableMyLocal(10));


        }
    }

    public class MyOtherClass
    {
        public void DoSomething()
        {
            Console.WriteLine("Doing Something from MyOtherClass");
        }
    }
}
