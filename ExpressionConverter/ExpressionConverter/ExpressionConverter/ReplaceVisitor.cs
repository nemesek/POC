using System;
using System.Linq.Expressions;

namespace ExpressionConverter
{
    class ReplaceVisitor : ExpressionVisitor
    {
        private ParameterExpression _parameter;

        public Expression Modify(Expression expression, ParameterExpression parameter)
        {
            this._parameter = parameter;
            return Visit(expression);
        }

        //protected override Expression VisitLambda<T>(Expression<T> node)
        //{
        //    //return Expression.Lambda<Func<OrderDto, bool>>(Visit(node.Body), Expression.Parameter(typeof(OrderDto)));
        //    return Expression.Lambda<Func<OrderDto, bool>>(Visit(node.Body), Expression.Parameter(typeof(int)));
        //}

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type == typeof(string))
            {
                return Expression.Property(_parameter, "ZipCode");
            }
            else if (node.Type == typeof(DateTime))
            {
                return Expression.Property(_parameter, "RanAt");
            }
            else if (node.Type == typeof(byte))
            {
                return Expression.Property(_parameter, "Result");
            }
            else if (node.Type == typeof(long))
            {
                return Expression.Property(_parameter, "Elapsed");
            }
            else if (node.Type == typeof (int))
            {
                return Expression.Property(_parameter, "DocId");

            }
            else if (node.Type == typeof (DomainOrder))
            {
                return Expression.Property(_parameter, "DocId");
            }
            throw new InvalidOperationException();
        }
    }
}