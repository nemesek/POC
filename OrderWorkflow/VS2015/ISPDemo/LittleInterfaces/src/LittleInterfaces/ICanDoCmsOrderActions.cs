using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleInterfaces
{
    public interface ICanDoCmsOrderActions : ICanUpdateOrderStatus, IAssignOrders
    {
        int OrderId { get; }
        string ZipCode { get; }
    }
}
