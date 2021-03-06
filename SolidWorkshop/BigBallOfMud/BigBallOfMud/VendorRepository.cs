﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBallOfMud
{
    public class VendorRepository
    {
        public IEnumerable<Vendor> GetVendors()
        {
            var vendor1 = new Vendor(11, "75019", "Dan Nemesek");
            var vendor2 = new Vendor(3, "38655", "Sarah Odom");
            var vendor3 = new Vendor(2, "38655", "Allen Thigpen");
            var vendors = new List<Vendor> { vendor1, vendor2, vendor3 };
            return vendors;
        }
    }
}
