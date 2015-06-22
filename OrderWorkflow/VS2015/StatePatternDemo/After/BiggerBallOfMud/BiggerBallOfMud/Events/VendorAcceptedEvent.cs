using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiggerBallOfMud.Events
{
    public class VendorAcceptedEvent : IDomainEvent
    {
        public Order Order { get; set; }
    }
}
