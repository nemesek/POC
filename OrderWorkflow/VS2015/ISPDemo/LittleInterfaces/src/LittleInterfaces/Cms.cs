using System;
using System.Collections.Generic;
using System.Linq;

namespace LittleInterfaces
{
    public class Cms
    {
        private readonly AutoAssignService _autoAssignService;

        public Cms(AutoAssignService autoAssignService)
        {
            _autoAssignService = autoAssignService;
        }
        public void AssignOrder (int orderId)
        {
            var order = GetMyOrders(orderId).SingleOrDefault();
            if (order == null) throw new Exception("This order doesn't belong to me");
            var vendorToAssign = _autoAssignService.FindVendorToAssignOrderTo(order, this.LinkedVendors());
            order.AssignVendor(vendorToAssign);
            vendorToAssign.AcceptOrder(order);
            order.UpdateOrderStatus(OrderStatus.VendorAccepted);
        }

        private IEnumerable<IBigOrderInterface> GetMyOrders(int id)
        {
            return new List<IBigOrderInterface> {new Order(id, OrderStatus.Unassigned)};
        }

        private IEnumerable<Vendor> LinkedVendors()
        {
            var vendor1 = new Vendor(11, "75019", "Dan Nemesek");
            var vendor2 = new Vendor(3, "38655", "Sarah Odom");
            var vendor3 = new Vendor(2, "38655", "Allen Thigpen");
            var vendors = new List<Vendor> { vendor1, vendor2, vendor3 };
            return vendors;
        }
    }
}
