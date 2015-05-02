namespace OrderWorkflow.Domain.Contracts
{
    public interface IOrderWithZipCode : IOrder
    {
        string ZipCode { get; }
    }
}
