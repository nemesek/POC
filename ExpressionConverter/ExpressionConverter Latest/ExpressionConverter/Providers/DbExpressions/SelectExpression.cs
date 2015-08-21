using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace ExpressionConverter.Providers.DbExpressions
{
    internal class SelectExpression : Expression
    {
        readonly string _alias;
        readonly ReadOnlyCollection<ColumnDeclaration> _columns;
        readonly Expression _from;
        readonly Expression _where;
        internal SelectExpression(Type type, string alias, IEnumerable<ColumnDeclaration> columns, Expression from, Expression where): base((ExpressionType)DbExpressionType.Select, type)
        {
            _alias = alias;
            _columns = columns as ReadOnlyCollection<ColumnDeclaration> ?? new List<ColumnDeclaration>(columns).AsReadOnly();
            _from = from;
            _where = where;
        }
        internal string Alias => _alias;

        internal ReadOnlyCollection<ColumnDeclaration> Columns => _columns;

        internal Expression From => _from;

        internal Expression Where => _where;
    }
}