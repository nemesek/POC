using System;

namespace LittleInterfaces
{
    public class Order : ICanDoCmsOrderActions, IHaveOrderId, ICanBeAutoAssigned//IBigOrderInterface
    {
        private readonly int _id;
        private Vendor _assignedVendor;
        private OrderStatus _status;

        public Order(int orderId, OrderStatus status)
        {
            _id = orderId;
            _status = status;
        }
        public int OrderId => _id;
        public OrderStatus Status => _status;
        public string ZipCode => "38655";
        public Vendor Vendor => _assignedVendor;
        
        public void UpdateOrderStatus(OrderStatus newStatus)
        {
            if (newStatus == OrderStatus.VendorAccepted)
            {
                if (!(_status == OrderStatus.Unassigned || _status == OrderStatus.ManualAssign))
                {
                    throw new Exception("Order is not in a status in which in can be accepted by the vendor.");
                }

            }

            _status = newStatus;
            this.Save();
        }

        public decimal PayAppraiserForWorkDone()
        {
            return 1000000m;
        }

        public void AssignVendor(Vendor vendor)
        {
            if (this.Status != OrderStatus.Unassigned) throw new Exception("Order is not in the correct status to be assigned");
            _assignedVendor = vendor;
            this.Save();
        }

        public void Save()
        {
            Console.WriteLine("Saving state of order.");
        }
    }
}
