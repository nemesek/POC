using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionConverter.Tests
{
    [TestClass]
    public class ExpressionRewriterTests
    {
        [TestMethod]
        public void ExpressionRewriter_Test1()
        {
            Expression<Func<Folder, Document>> myFirst = gp => gp.Document;
            Expression<Func<Document, string>> mySecond = p => p.Service.Name;
            var outputWithInline = myFirst.Combine(mySecond, false);

            Assert.IsNotNull(outputWithInline);
            var result = outputWithInline
                .Compile()
                .Invoke(new Folder {Document = new Document {Service = new Service {Name = "Test"}}});

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ExpressionRewriter_Test3()
        {
            var service = new Service {Name = "Foo"};
            var document = new Document {Service = service};
            var folder = new Folder {Document = document};
            Expression<Func<Folder, Document>> myFirst = gp => gp.Document;
            //Expression<Func<Document, string>> mySecond = p => p.Service.Name;
            //var outputWithInline = myFirst.Combine(mySecond, false);
            Func<string, bool> myFunc = n => myFirst.Compile().Invoke(folder).Service.Name == n;
            var result = myFunc.Invoke("Test");
            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExpressionRewriter_Test2()
        {
            var domainOrder = new DomainOrder {OrderId = 5, ZipCode = "38655"};
            Expression<Func<DomainOrder, OrderDto>> myFirst = d => GetOrderDto(d);
            Func<string, bool> mySecond = zip => myFirst.Compile().Invoke(domainOrder).ZipCode == zip;
            var result = mySecond.Invoke("38655");
            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public void ExpressionRewriter_Test2()
        //{
        //    Expression<Func<DomainOrder, int, OrderDto>> myFirst = (o, id) => GetOrderDto(o);
        //    Expression<Func<OrderDto, int, bool>> mySecond = (dto, id) => dto.DocId == id;
        //    var outputWithInline = myFirst.Combine(mySecond, false);
        //    Assert.IsNotNull(outputWithInline);
        //    var result = outputWithInline
        //        .Compile()
        //        .Invoke(new OrderDto { DocId = 5 }, 5);

        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result);
        //}

        private static OrderDto GetOrderDto(DomainOrder domainOrder)
        {
            return new OrderDto
            {
                DocId = domainOrder.OrderId,
                OrderId = domainOrder.OrderId,
                ZipCode = domainOrder.ZipCode
            };
        }
    }
}
