namespace OrderWorkflow.Domain
{
    public enum OrderStatus
    {
        Unassigned = 0,
        Assigned = 1,
        VendorAccepted = 2,
        Submitted = 3,
        Rejected = 4,
        OnHold = 5,
        Closed = 6,
        ReviewSubmission = 7,
        ReviewAcceptance = 8,
        ClientAccepted = 9
    }
}
