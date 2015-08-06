using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter
{
    public class Document
    {
        public Service Service { get; set; }

        public string GenerateSuffix(string s)
        {
            return s + "abc";
        }
    }
}
