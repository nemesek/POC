using System.Linq.Expressions;

namespace ExpressionConverter.Providers
{
    internal class Replacer : DbExpressionVisitor
    {
        Expression _searchFor;
        Expression _replaceWith;
        internal Expression Replace(Expression expression, Expression searchFor, Expression replaceWith)
        {
            _searchFor = searchFor;
            _replaceWith = replaceWith;
            return Visit(expression);
        }
        protected override Expression Visit(Expression exp)
        {
            return exp == _searchFor ? _replaceWith : base.Visit(exp);
        }
    }
}
