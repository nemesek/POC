using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.Contracts
{
    public interface IProcessAutoAssign
    {
        Vendor FindBestVendor(ICanBeAutoAssigned order);
    }
}
