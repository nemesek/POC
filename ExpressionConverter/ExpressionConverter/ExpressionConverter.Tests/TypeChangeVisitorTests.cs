using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpressionConverter.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionConverter.Tests
{
    [TestClass]
    public class TypeChangeVisitorTests
    {
        [TestMethod]
        public void Foo()
        {
            var zipCode = "38655";
            var orderId = 1;
            var orderDto = new OrderDto {ZipCode = "38655", OrderId = orderId};
            Expression<Func<DomainOrder,bool>> domainExpression = (o) => o.ZipCode == zipCode && o.OrderId == orderId;
            var converted = Translate<DomainOrder, OrderDto>(domainExpression);
            Assert.IsNotNull(converted);
            var match = converted.Compile().Invoke(orderDto);
            Assert.IsTrue(match);
        }

        public void Foo2()
        {
            var zipCode = "38655";
            var orderId = 1;
            var orderDto = new OrderDto { ZipCode = "38655", OrderId = orderId };
            Expression<Func<DomainOrder, bool>> domainExpression = (o) => o.ZipCode == zipCode && o.OrderId == orderId;
            var converted = Translate<DomainOrder, OrderDto>(domainExpression);
            Assert.IsNotNull(converted);
            var match = converted.Compile().Invoke(orderDto);
            Assert.IsTrue(match);
        }

        private static Expression<Func<TTo, bool>> Translate<TFrom, TTo>(Expression<Func<TFrom, bool>> expression)
        {
            var param = Expression.Parameter(typeof(TTo), expression.Parameters[0].Name);
            var subst = new Dictionary<Expression, Expression> { { expression.Parameters[0], param } };
            var visitor = new TypeChangeVisitor(typeof(TFrom), typeof(TTo), subst);
            return Expression.Lambda<Func<TTo, bool>>(visitor.Visit(expression.Body), param);
        }
    }
}
