using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter.Providers
{
    internal class SelectExpression : Expression
    {
        internal SelectExpression(Type type, string alias, IEnumerable<ColumnDeclaration> columns, Expression from, Expression where)
            : base((ExpressionType)DbExpressionType.Select, type)
        {
            this.Alias = alias;
            this.Columns = columns as ReadOnlyCollection<ColumnDeclaration> ?? new List<ColumnDeclaration>(columns).AsReadOnly();
            this.From = from;
            this.Where = where;
        }
        internal string Alias { get; }

        internal ReadOnlyCollection<ColumnDeclaration> Columns { get; }

        internal Expression From { get; }

        internal Expression Where { get; }
    }
}
