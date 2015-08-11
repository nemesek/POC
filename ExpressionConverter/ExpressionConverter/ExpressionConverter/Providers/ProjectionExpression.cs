using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter.Providers
{
    internal class ProjectionExpression : Expression
    {
        internal ProjectionExpression(SelectExpression source, Expression projector)
            : base((ExpressionType)DbExpressionType.Projection, projector.Type)
        {
            this.Source = source;
            this.Projector = projector;
        }
        internal SelectExpression Source { get; }

        internal Expression Projector { get; }
    }
}
