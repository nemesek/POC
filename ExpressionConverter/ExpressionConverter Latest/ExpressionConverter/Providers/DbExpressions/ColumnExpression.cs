using System;
using System.Linq.Expressions;

namespace ExpressionConverter.Providers.DbExpressions
{
    internal class ColumnExpression : Expression
    {
        string _alias;
        string _name;
        int _ordinal;
        internal ColumnExpression(Type type, string alias, string name, int ordinal): base((ExpressionType)DbExpressionType.Column, type)
        {
            _alias = alias;
            _name = name;
            _ordinal = ordinal;
        }
        internal string Alias => _alias;

        internal string Name => _name;

        internal int Ordinal => _ordinal;
    }
}