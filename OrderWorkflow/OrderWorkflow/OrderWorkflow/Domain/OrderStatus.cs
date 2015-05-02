namespace OrderWorkflow.Domain
{
    public enum OrderStatus
    {
        New = 0,
        Assigned = 1,
        Accepted = 2,
        Submitted = 3,
        Rejected = 4,
        WithClient = 5,
        Closed = 6
    }
}
