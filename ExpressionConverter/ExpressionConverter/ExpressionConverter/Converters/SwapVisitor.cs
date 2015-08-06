using System.Linq.Expressions;

namespace ExpressionConverter.Converters
{
    public class SwapVisitor : ExpressionVisitor
    {
        private readonly Expression _from, _to;
        public SwapVisitor(Expression from, Expression to)
        {
            this._from = from;
            this._to = to;
        }
        public override Expression Visit(Expression node)
        {
            return node == _from ? _to : base.Visit(node);
        }
    }
}
