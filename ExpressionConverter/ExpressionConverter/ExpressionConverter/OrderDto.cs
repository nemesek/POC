using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter
{
    public class OrderDto : DomainOrder
    {
        public int DocId { get; set; }
        //public new int OrderId { get; set; }
        //public string ZipCode { get; set; }
    }
}
