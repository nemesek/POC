using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleInterfaces
{
    public interface ICanUpdateOrderStatus
    {
        void UpdateOrderStatus(OrderStatus newStatus);
    }
}
