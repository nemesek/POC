using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter.Providers
{
    internal class TableExpression : Expression
    {
        internal TableExpression(Type type, string alias, string name)
            : base((ExpressionType)DbExpressionType.Table, type) // todo: fix this
        {
            this.Alias = alias;
            this.Name = name;
        }
        internal string Alias { get; }

        internal string Name { get; }
    }
}
