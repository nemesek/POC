using System;
using System.Linq.Expressions;

namespace ExpressionConverter.Converters
{
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

        public Expression<Func<OrderDto, bool>> ConvertDomainExpressionToDtoExpression2()
        {
            Expression<Func<OrderDto, bool>> linqExpression = o => o.OrderId == 1;
            //var oldBody = linqExpression.
            var itemToCompare = new OrderDto();
            var param = Expression.Parameter(typeof(OrderDto), "o");
            var key = typeof (OrderDto).GetProperty("DocId");
            var rhs = Expression.MakeMemberAccess(Expression.Constant(itemToCompare), key);
            var lhs = Expression.MakeMemberAccess(param, key);
            var body = Expression.Equal(lhs, rhs);
            var lambda = Expression.Lambda<Func<OrderDto, bool>>(body, param);
            //return result;
            return lambda;
        }

        public Expression<Func<OrderDto, string, int, bool>> ConvertDomainExpressionToDtoExpression(Expression<Func<DomainOrder, string, int, bool>> expressionToTransform)
        {
            // this requires the OrderDto to inherit from DomainOrder
            // we could handle the schema names differing by doing a column mapping on the EF Poco
            var result = ParameterReplacer.Replace<Func<DomainOrder, string, int, bool>, Func<OrderDto, string, int, bool>>
                (expressionToTransform, Expression.Parameter(typeof(DomainOrder)), Expression.Parameter(typeof(OrderDto)));

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
            var orderDtoParameter = Expression.Parameter(typeof(OrderDto), "orderDto");
            var newExpression = Expression.Lambda<Func<OrderDto, bool>>(new ReplaceVisitor().Modify(expression.Body, orderDtoParameter), orderDtoParameter);
            return newExpression;
        }
        static Expression<Func<T2, bool>> PartialApply<T, T2>(Expression<Func<T, T2, bool>> expr, T c)
        {
            var param = Expression.Parameter(typeof(T2), null);
            return Expression.Lambda<Func<T2, bool>>(
                Expression.Invoke(expr, Expression.Constant(c), param),
                param);
        }

        //public static Expression<Func<bool>> PartialApply<T>(Expression<Func<T, bool>> expr)
        //{
        //    var param = Expression.Parameter(typeof(T), null);
        //    return Expression.Lambda<Func<bool>>(Expression.Invoke(expr, param),param);
        //}

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
