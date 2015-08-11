using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter.Providers
{
    internal class ProjectionBuilder : DbExpressionVisitor
    {
        private ParameterExpression row;
        private string rowAlias;
        private static MethodInfo miGetValue;
        private static MethodInfo miExecuteSubQuery;

        internal ProjectionBuilder()
        {
            if (miGetValue == null)
            {
                miGetValue = typeof (ProjectionRow).GetMethod("GetValue");
                miExecuteSubQuery = typeof (ProjectionRow).GetMethod("ExecuteSubQuery");
            }
        }

        internal LambdaExpression Build(Expression expression, string alias)
        {
            this.row = Expression.Parameter(typeof (ProjectionRow), "row");
            this.rowAlias = alias;
            Expression body = this.Visit(expression);
            return Expression.Lambda(body, this.row);
        }

        protected override Expression VisitColumn(ColumnExpression column)
        {
            if (column.Alias == this.rowAlias)
            {
                return Expression.Convert(Expression.Call(this.row, miGetValue, Expression.Constant(column.Ordinal)),
                    column.Type);
            }
            return column;
        }

        protected override Expression VisitProjection(ProjectionExpression proj)
        {
            LambdaExpression subQuery = Expression.Lambda(base.VisitProjection(proj), this.row);
            Type elementType = TypeSystem.GetElementType(subQuery.Body.Type);
            MethodInfo mi = miExecuteSubQuery.MakeGenericMethod(elementType);
            return Expression.Convert(
                Expression.Call(this.row, mi, Expression.Constant(subQuery)),
                proj.Type
                );
        }
    }
}
