namespace OrderWorkflow.Domain.Contracts
{
    public interface IOrderWithZipCode : IWorkflowOrder
    {
        string ZipCode { get; }
    }
}
