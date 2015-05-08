using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public class OrdersController
    {
        public void DoSomething(int clientId)
        {
            if (clientId == 1)
            {
                Console.WriteLine("I am doing foo for {0}", clientId);
            }
            else
            {
                Console.WriteLine("I am doing bar for {0}", clientId);
            }
            
        }
    }
}
