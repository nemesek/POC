namespace LittleInterfaces
{
    public interface IBigOrderInterface
    {
        int OrderId { get; }    // used by Vendor
        OrderStatus Status { get; } // only by order himself
        string ZipCode { get; } // only by AutoAssignService
        void AssignVendor(Vendor vendor); // only by CMS
        Vendor Vendor { get; } // only by order
        void UpdateOrderStatus(OrderStatus newStatus); // only by CMS
    }
}
