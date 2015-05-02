using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.AutoAssign
{
    public class AutoAssignFactory
    {
        public static IProcessAutoAssign CreateAutoAssign(int id)
        {
            switch (id)
            {
                case 1: return new DefaultAutoAssign();
                case 2: return new CmsNetAutoAssign();
                case 3: return new CmsNextAutoAssign();
                case 4: return new LegacyAutoAssign();
            }

            return new LegacyAutoAssign();
        }
    }
}
