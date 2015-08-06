using System;
using System.Linq.Expressions;

namespace ExpressionConverter
{
    // http://stackoverflow.com/questions/11164009/using-a-linq-expressionvisitor-to-replace-primitive-parameters-with-property-ref
    public class OrderExpressionConverter
    {
        public Expression<Func<OrderDto, int, bool>> ConvertDomainExpressionToDtoExpression(Expression<Func<DomainOrder,int,bool>> expressionToTransform)
        {
            // this requires the OrderDto to inherit from DomainOrder
            // we could handle the schema names differing by doing a column mapping on the EF Poco
            var result = ParameterReplacer.Replace<Func<DomainOrder, int, bool>, Func<OrderDto,int,bool>>
                (expressionToTransform,Expression.Parameter(typeof (DomainOrder)), Expression.Parameter(typeof (OrderDto)));

            return result;
        }

        public Expression BreakDownIntoPrimitives(Expression<Func<DomainOrder, int, bool>> expressionToTransform)
        {
            var numParam = Expression.Parameter(typeof(OrderDto), "orderDto");
            var visitor = new ReplaceVisitor();
            var result = visitor.Modify(expressionToTransform.Parameters[0], numParam);
            return result;
            
        }

        public static Expression<Func<OrderDto, bool>> ConvertExpressionWithPrimitivesToOrderDtoExpression(Expression<Func<string,int, bool>> expression)
        {
            // this converts a lot of primitives into an expression that converts the primitive predicates into a dto expression
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
