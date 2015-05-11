using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            var id = Randomizer.RandomYes() ? 1 : 2;
            var controller = new OrdersController();
            controller.DoSomething(id);
            
        }

        #region Strategy/OCP Demo
        static void DemoAutoAssignConditionally()
        {
            var id = Randomizer.GetRandomFromRange(1, 25);
            var controller = new OrdersController();
            controller.RunAutoAssignLogicConditionally(id);
        }


        // demo strategy pattern via foobarservice

        // have class walk you through  strategy pattern implementation of auto assign
        // add another case
        // show how Orders automatically picked up ability to run new case without changing OCP
        #endregion

    }
}
