using System;
using System.Linq.Expressions;
using System.Reflection;
using ExpressionConverter.Providers.DbExpressions;

namespace ExpressionConverter.Providers.Projections
{
    internal class ProjectionBuilder : DbExpressionVisitor
    {
        ParameterExpression _row;
        string _rowAlias;
        static MethodInfo _miGetValue;
        static MethodInfo _miExecuteSubQuery;
        
        internal ProjectionBuilder()
        {
            if (_miGetValue != null) return;
            _miGetValue = typeof(ProjectionRow).GetMethod("GetValue");
            _miExecuteSubQuery = typeof(ProjectionRow).GetMethod("ExecuteSubQuery");
        }

        internal LambdaExpression Build(Expression expression, string alias)
        {
            _row = Expression.Parameter(typeof(ProjectionRow), "row");
            _rowAlias = alias;
            Expression body = Visit(expression);
            return Expression.Lambda(body, _row);
        }

        protected override Expression VisitColumn(ColumnExpression column)
        {
            if (column.Alias == _rowAlias)
            {
                return Expression.Convert(Expression.Call(_row, _miGetValue, Expression.Constant(column.Ordinal)), column.Type);
            }
            return column;
        }

        protected override Expression VisitProjection(ProjectionExpression proj)
        {
            LambdaExpression subQuery = Expression.Lambda(base.VisitProjection(proj), _row);
            Type elementType = TypeSystem.GetElementType(subQuery.Body.Type);
            MethodInfo mi = _miExecuteSubQuery.MakeGenericMethod(elementType);
            return Expression.Convert(
                Expression.Call(_row, mi, Expression.Constant(subQuery)),
                proj.Type
                );
        }
    }
}