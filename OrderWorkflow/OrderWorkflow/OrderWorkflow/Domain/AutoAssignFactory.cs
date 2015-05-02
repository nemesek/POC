using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWorkflow.Domain
{
    public class AutoAssignFactory
    {
        public static IProcessAutoAssign CreateAutoAssign(int id)
        {
            return new DefaultAutoAssign();
        }
    }
}
