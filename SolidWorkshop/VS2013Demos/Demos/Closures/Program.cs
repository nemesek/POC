using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            // maybe better or more readable?  with this way we don't have to even have a display class because my local is passed by value
            otherClass.YouTellMeWhatToDo(x => x + myLocal);

            // now let's do it using the conventional approach with maybe uneccessary interfaces and classes
            var getStuffDoneGuy = new GetStuffDoneGuy();
            otherClass.YouTellMeWhatToDo(getStuffDoneGuy);

            // do you even linq bro?
            var list = new List<int> {1, 2, 4, 5, 3, 5};
            var jackson5 = FindTheElementsWithValue5(list);
            Debug.Assert(jackson5.Count() == 2);

            var benFoldsFive = FindTheElementsWithValue5Functional(list);
            Debug.Assert(benFoldsFive.Count() == 2);

        }

        // tell an idiot how to do it
        static IEnumerable<int> FindTheElementsWithValue5(IEnumerable<int> numbers)
        {
            var elementsWith5 = new List<int>();

            foreach (var x in numbers)
            {
                if (x == 5) elementsWith5.Add(x);
            }

            return elementsWith5;
        }

        // tell em what you want
        static IEnumerable<int> FindTheElementsWithValue5Functional(IEnumerable<int> numbers)
        {
            return numbers.Where(x => x == 5).ToList();
        }
    }

}
