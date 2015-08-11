using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter.Providers
{
    internal class ColumnDeclaration
    {
        internal ColumnDeclaration(string name, Expression expression)
        {
            this.Name = name;
            this.Expression = expression;
        }
        internal string Name { get; }
        internal Expression Expression { get; }
    }
}
