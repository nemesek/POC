using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.AutoAssign
{
    public class LegacyAutoAssign : IProcessAutoAssign
    {
        public Vendor FindBestVendor(ICanBeAutoAssigned order)
        {
            // null object pattern to avoid LSP violation
            // http://en.wikipedia.org/wiki/Null_Object_pattern
            return new NullVendor();
        }
    }
}
