using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter.Converters
{
    public class Renamer : ExpressionVisitor
    {
        public Expression Rename(Expression expression)
        {
            return Visit(expression);
        }

        //protected override Expression VisitParameter(ParameterExpression node)
        //{
        //    //if (node.Name == "a")
        //    //    return Expression.Parameter(node.Type, "something_else");
        //    //else
        //    //    return node;
        //    if (node.Type.Name == "OrderDto")
        //    {
        //        //    var result = ParameterReplacer.Replace<Func<DomainOrder, string, int, bool>, Func<OrderDto, string, int, bool>>
        //        //   (expressionToTransform, Expression.Parameter(typeof(DomainOrder)), Expression.Parameter(typeof(OrderDto)));
        //        var result = ParameterReplacer
        //            .Replace<ParameterExpression>
        //            (node, Expression.Parameter(typeof (OrderDto)), Expression.Parameter(typeof (OrderDto));

        //    }
        //    return node;
        //}
    }
}
