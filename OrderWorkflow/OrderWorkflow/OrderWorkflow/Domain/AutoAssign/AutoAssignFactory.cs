using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.AutoAssign
{
    public class AutoAssignFactory
    {
        public static IProcessAutoAssign CreateAutoAssign(int id)
        {
            var moddedId = id%4;
            switch (moddedId)
            {
                case 0: return new DefaultAutoAssign();
                case 1: return new CmsNetAutoAssign();
                case 2: return new CmsNextAutoAssign();
                case 3: return new LegacyAutoAssign();
            }

            return new LegacyAutoAssign();
        }
    }
}
