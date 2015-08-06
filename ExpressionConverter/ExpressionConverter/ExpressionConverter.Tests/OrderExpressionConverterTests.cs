using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
            var orderDto = new OrderDto {OrderId = 1};
            const int orderId = 2;
            var match = result.Compile().Invoke(orderDto, orderId);
            Assert.IsFalse(match);
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
