﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strategy.Domain.StrategyOCP;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            //var id = Randomizer.RandomYes() ? 1 : 2;
            //var controller = new OrdersController();
            //controller.DoSomething(id);
            //DemoFoobarWithStrategy(3);
        }

        #region Strategy/OCP Demo
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
        static void DemoFoobarWithStrategy()
        {
            var id = Randomizer.RandomYes() ? 1 : 2;
            var controller = new OrdersController();
            var service = new FoobarService();
            controller.DoSomethingWithStrategy(id, service);
        }

        // demo something changing bar to baz
        // demo something being added
        //static void DemoFoobarWithStrategy(int id)
        //{
        //    var controller = new OrdersController();
        //    var service = new BetterFoobarService();
        //    controller.DoSomethingWithStrategy(id, service);
        //}
        // have class walk you through  strategy pattern implementation of auto assign
        // add another case
        // show how Orders automatically picked up ability to run new case without changing OCP
        #endregion

    }
}