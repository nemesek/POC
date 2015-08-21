using System;
using System.Linq.Expressions;

namespace ExpressionConverter.Providers.DbExpressions
{
    internal class TableExpression : Expression
    {
        readonly string _alias;
        readonly string _name;
        internal TableExpression(Type type, string alias, string name): base((ExpressionType)DbExpressionType.Table, type)
        {
            _alias = alias;
            _name = name;
        }
        internal string Alias => _alias;

        internal string Name => _name;
    }
}