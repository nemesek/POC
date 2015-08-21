using System.Linq.Expressions;

namespace ExpressionConverter.Providers.DbExpressions
{
    internal class ColumnDeclaration
    {
        readonly string _name;
        readonly Expression _expression;
        internal ColumnDeclaration(string name, Expression expression)
        {
            _name = name;
            _expression = expression;
        }
        internal string Name => _name;

        internal Expression Expression => _expression;
    }
}