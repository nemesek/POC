using System;
using Strategy.Domain.StrategyOCP;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            //DemoDoSomething();
            //DemoAutoAssignConditionally();
            //DemoFoobarDelegation();
            //DemoFoobarWithStrategy();
            //DemoFoobarWithStrategy(Randomizer.GetRandomFromRange(1,4));
        }

        #region Strategy/OCP Demo

        static void DemoDoSomething()
        {
            var id = Randomizer.RandomYes() ? 1 : 2;
            var controller = new OrdersController();
            controller.DoSomething(id);
        }

        static void DemoAutoAssignConditionally()
        {
            var id = Randomizer.GetRandomFromRange(1, 25);
            var controller = new OrdersController();
            controller.RunAutoAssignLogicConditionally(id);
        }


        // demo delegation by newing up foobar service
        static void DemoFoobarDelegation()
        {
            var id = Randomizer.RandomYes() ? 1 : 2;
            var controller = new OrdersController();
            controller.DoSomethingWithDelegation(id);
        }

        // demo strategy pattern via foobarservice
        // demo something changing bar to baz
        static void DemoFoobarWithStrategy()
        {
            var id = Randomizer.RandomYes() ? 1 : 2;
            var controller = new OrdersController();
            var service = new FoobarService();
            controller.DoSomethingWithStrategy(id, service);
        }
        
        // demo something being added
        static void DemoFoobarWithStrategy(int id)
        {
            var controller = new OrdersController();
            var service = new BetterFoobarService();
            controller.DoSomethingWithStrategy(id, service);
        }
        // have class walk you through  strategy pattern implementation of auto assign
        // add another case
        // show how OrdersController automatically picked up ability to run new case without changing OCP
        #endregion

        #region Factory/SRP/DIP demo
        // show how changing the ctor requires all callers to change
        // show how adding a new class requires all callers to know about it
        // demo foobarservice factory
        // To show DIP add dep on foobar for dataAccess to do logging

        #endregion

    }
}
