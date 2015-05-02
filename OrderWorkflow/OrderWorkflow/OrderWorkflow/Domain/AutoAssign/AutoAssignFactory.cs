using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.AutoAssign
{
    public class AutoAssignFactory
    {
        public static IProcessAutoAssign CreateAutoAssign(int id)
        {
            return new DefaultAutoAssign();
        }
    }
}
