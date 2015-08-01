using System;
using System.Linq.Expressions;

namespace ExpressionConverter
{
    // http://stackoverflow.com/questions/11164009/using-a-linq-expressionvisitor-to-replace-primitive-parameters-with-property-ref
    public class OrderExpressionConverter
    {
        public Expression<Func<OrderDto, int, bool>> ConvertDomainExpressionToDtoExpression(Expression<Func<DomainOrder,int,bool>> expressionToTransform)
        {
            var result = ParameterReplacer.Replace<Func<DomainOrder, int, bool>, Func<OrderDto,int,bool>>
                (expressionToTransform,Expression.Parameter(typeof (DomainOrder)), Expression.Parameter(typeof (OrderDto)));

            return result;
        }

        public Expression Convert2(Expression<Func<DomainOrder, int, bool>> expressionToTransform)
        {
            var numParam = Expression.Parameter(typeof(OrderDto), "orderDto");
            var visitor = new ReplaceVisitor();
            var result = visitor.Modify(expressionToTransform.Parameters[0], numParam);
            return result;
            
        }

        public static Expression<Func<OrderDto, bool>> ConvertExpressionWithPrimitivesToOrderDtoExpression(Expression<Func<string,int, bool>> expression)
        {
            var jobAuditParameter = Expression.Parameter(typeof(OrderDto), "orderDto");
            var newExpression = Expression.Lambda<Func<OrderDto, bool>>(new ReplaceVisitor().Modify(expression.Body, jobAuditParameter), jobAuditParameter);
            return newExpression;
        }

        //public Expression Convert3(Expression<Func<DomainOrder, int, bool>> expressionToTransform)
        //{
        //    var body = expressionToTransform.Body;
        //    var targetParam = Expression.Parameter(typeof(OrderDto), "orderDto");
        //    var visitor = new ReplaceVisitor();
        //    //var result = visitor.Modify(expressionToTransform.Parameters[0], numParam);
        //    //return result;

        //}
    }
}
