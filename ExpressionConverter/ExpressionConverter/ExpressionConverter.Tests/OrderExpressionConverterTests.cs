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
    public class OrderExpressionConverterTests
    {
        [TestMethod]
        public void OrderExpressionConverter_ConvertDomainExpressionToDtoExpression_ReturnsDtoExpressionWhenGivenDomainExpression()
        {
            Expression<Func<DomainOrder,int, bool>> expression = (o, id ) => o.OrderId == id;
            var target = new OrderExpressionConverter();

            // act
            var result = target.ConvertDomainExpressionToDtoExpression(expression);

            // assert
            Assert.IsNotNull(result);
            var dtoResult = (Expression<Func<OrderDto, int, bool>>) result; // redundant just for demo purposes
            Assert.IsNotNull(dtoResult);
            var orderDto = new OrderDto {OrderId = 1};
            const int orderId = 2;
            var match = result.Compile().Invoke(orderDto, orderId);
            Assert.IsFalse(match);
        }

        [TestMethod]
        public void OrderExpressionConverter_ConvertDomainExpressionToDtoExpression_ReturnsDtoExpressionWhenGivenDomainExpressionWith2Predicates()
        {
            Expression<Func<DomainOrder, string, int, bool>> domainExpression = (o, zip, id) => o.ZipCode == zip && o.OrderId == id;
            var target = new OrderExpressionConverter();
            var result = target.ConvertDomainExpressionToDtoExpression(domainExpression);

            Assert.IsNotNull(result);
            var orderDto = new OrderDto { OrderId = 1, ZipCode = "38655" };
            var match = result.Compile().Invoke(orderDto, "38655", 1);
            Assert.IsTrue(match);

        }

        [TestMethod]
        public void OrderExpressionConverter_ConvertDomainExpressionToDtoExpression_ReturnsDtoExpressionWhenGivenDomainExpression2()
        {
            Expression<Func<DomainOrder, int, bool>> expression = (o, id) => o.OrderId == id;
            var target = new OrderExpressionConverter();

            // act
            var result = target.ConvertDomainExpressionToDtoExpression(expression);

            // assert
            Assert.IsNotNull(result);
            var orderDto = new OrderDto { OrderId = 1 };
            const int orderId = 2;
            var match = result.Compile().Invoke(orderDto, orderId);
            Assert.IsFalse(match);
        }

        [TestMethod]
        public void Converter_ConvertExpression2_ReturnsNewExpression()
        {
            Expression<Func<DomainOrder, int, bool>> expression = (o, id) => o.OrderId == id;
            var target = new OrderExpressionConverter();

            // act
            var result = target.BreakDownIntoPrimitives(expression);

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrderExpressionConverter_ConvertExpressionWithPrimitivesToOrderDtoExpression_ReturnsExpressionWhereOrderDtoMatchesOnTwoPredicatesWhenGivenPrimitives()
        {
            Expression<Func<string,int,bool>> expression = (zip, id) => zip == "38655" && id == 1;
            var result = OrderExpressionConverter.ConvertExpressionWithPrimitivesToOrderDtoExpression(expression);

            Assert.IsNotNull(result);
            var orderDto = new OrderDto {DocId = 1, ZipCode = "38655"};
            var match = result.Compile().Invoke(orderDto);
            Assert.IsTrue(match);

        }
    }
}
