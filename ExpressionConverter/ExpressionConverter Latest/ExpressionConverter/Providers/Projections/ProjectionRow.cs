using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionConverter.Providers.Projections
{
    public abstract class ProjectionRow
    {
        public abstract object GetValue(int index);
        public abstract IEnumerable<TE> ExecuteSubQuery<TE>(LambdaExpression query);
    }
}