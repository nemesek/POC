using System.Linq.Expressions;

namespace ExpressionConverter.Providers.DbExpressions
{
    internal class ProjectionExpression : Expression
    {
        readonly SelectExpression _source;
        readonly Expression _projector;
        internal ProjectionExpression(SelectExpression source, Expression projector) : base((ExpressionType)DbExpressionType.Projection, source.Type)
        {
            _source = source;
            _projector = projector;
        }
        internal SelectExpression Source => _source;

        internal Expression Projector => _projector;
    }
}