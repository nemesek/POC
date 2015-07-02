using System;
using Strategy.Domain.StrategyOCP;

namespace Strategy
{
    public class OrdersController
    {
        public void DoSomething(int cmsId)
        {
            Console.WriteLine(cmsId == 1 ? "I am doing foo for {0}" : "I am doing bar for {0}", cmsId);
        }

        public void RunAutoAssignLogicConditionally(int cmsId)
        {
            var moddedId = cmsId%4;
            var output = string.Empty;
            switch (moddedId)
            {
                case 0:
                    output = "Default";
                    break;
                case 1:
                    output = "Custom 1";
                    break;
                case 2:
                    output = "Custom 2";
                    break;
                case 3:
                    output = "Custom 3";
                    break;
            }

            Console.WriteLine("Running {0} Logic for CMS {1}", output, cmsId);
        }

        public void DoSomethingWithDelegation(int cmsId)
        {
            var foobarService = new FoobarService();
            Console.WriteLine(foobarService.GetAction(cmsId));
        }

        public void DoSomethingWithStrategy(int cmsId, FoobarService service)
        {
            Console.WriteLine(service.GetAction(cmsId));
        }
        
        public void RunAutoAssignWithStrategy(IProcessAutoAssign autoAssigner)
        {
            Console.WriteLine(autoAssigner.RunAutoAssignLogic());
        }









        //public void RunAutoAssignWithStrategy(Func<string> autoAssign)
        //{
        //    Console.WriteLine(autoAssign());
        //}
    }
}
