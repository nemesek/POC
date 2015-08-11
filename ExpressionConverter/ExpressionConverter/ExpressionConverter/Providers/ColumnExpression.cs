using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter.Providers
{
    internal class ColumnExpression : Expression
    {
        internal ColumnExpression(Type type, string alias, string name, int ordinal)
            : base((ExpressionType)DbExpressionType.Column, type)
        {
            this.Alias = alias;
            this.Name = name;
            this.Ordinal = ordinal;
        }
        internal string Alias { get; }

        internal string Name { get; }

        internal int Ordinal { get; }
    }
}
