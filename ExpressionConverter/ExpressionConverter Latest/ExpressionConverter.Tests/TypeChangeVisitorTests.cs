using System;
using System.Linq.Expressions;
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
            var converted = TypeChangeVisitor.Translate<DomainOrder, OrderDto>(domainExpression);
            Assert.IsNotNull(converted);
            var match = converted.Compile().Invoke(orderDto);
            Assert.IsTrue(match);
        }

        [TestMethod]
        public void Foo2()
        {
            var zipCode = "38655";
            var orderId = 1;
            var orderDto = new OrderDto { ZipCode = "38655", OrderId = orderId };
            Expression<Func<DomainOrder, bool>> domainExpression = (o) => o.ZipCode == zipCode && o.OrderId == orderId;
            var converted = TypeChangeVisitor.Translate<DomainOrder, OrderDto>(domainExpression);
            Assert.IsNotNull(converted);
            var match = converted.Compile().Invoke(orderDto);
            Assert.IsTrue(match);
        }

        [TestMethod]
        public void Foo3()
        {
            var filter = "12345";
            var vendorDto = new VendorPoco {Id = "12345"};
            Expression<Func<Vendor, bool>> domainExpression = (v) => v.Id == filter;
            var converted = TypeChangeVisitor.Translate<Vendor, VendorPoco>(domainExpression);
            Assert.IsNotNull(converted);
            var match = converted.Compile().Invoke(vendorDto);
            Assert.IsTrue(match);
        }

        [TestMethod]
        public void Foo4()
        {
            var filter = "12345";
            var vendorDto = new VendorPoco { Id = "12345" };
            Expression<Func<Vendor, bool>> domainExpression = (v) => v.Id == filter;
            var converted = TypeChangeVisitor.Translate<Vendor, VendorPoco>(domainExpression);
            Assert.IsNotNull(converted);

        }

        [TestMethod]
        public void Foo5()
        {
            var zipCode = "38655";
            var orderId = 1;
            var orderDto = new OrderDto { ZipCode = "38655", OrderId = orderId };
            Expression<Func<DomainOrder, bool>> domainExpression = (o) => o.ZipCode == zipCode && o.OrderId == orderId;
            var converted = TypeChangeVisitor.Translate<DomainOrder, OrderDto>(domainExpression);
            Assert.IsNotNull(converted);
            //var match = converted.Compile().Invoke(orderDto);
            //Assert.IsTrue(match);

        }


        [TestMethod]
        public void Foo6()
        {
            var zipCode = "38655";
            var orderId = 1;
            var orderDto = new OrderDto { ZipCode = "38655", OrderId = orderId };
            Expression<Func<DomainOrder, bool>> domainExpression = (o) => o.ZipCode == zipCode && o.OrderId == orderId;
            var renamer = new Renamer();
            var result = renamer.Rename(domainExpression);
            Assert.IsNotNull(result);
            //var match = converted.Compile().Invoke(orderDto);
            //Assert.IsTrue(match);

        }


    }
}
