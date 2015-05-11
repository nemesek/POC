using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public class OrdersController
    {
        public void DoSomething(int cmsId)
        {
            if (cmsId == 1)
            {
                Console.WriteLine("I am doing foo for {0}", cmsId);
            }
            else
            {
                Console.WriteLine("I am doing bar for {0}", cmsId);
            }
            
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
    }
}
