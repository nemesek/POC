﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiggerBallOfMud.OrderStatuses;

namespace BiggerBallOfMud.Events
{
    public class VendorRejectedEvent : IDomainEvent
    {
        public Order Order { get; set; }
    }
}
