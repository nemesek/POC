using System;
using System.Linq.Expressions;
using ExpressionConverter.Converters;
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


        [TestMethod]
        public void ExpressionRewriter_Test4()
        {
            Expression<Func<DomainOrder, string, int, bool>> domainExpression = (o, zip, id) => o.ZipCode == zip && o.OrderId == id;
            var body = domainExpression.Body;
            var left = body.NodeType;
            var compiled = domainExpression.Compile();
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

//        Expression<Func<Folder, Document>> myFirst = gp => gp.Document;
//        Expression<Func<Document, string>> mySecond = p => p.Service.Name;

//        Expression<Func<Folder, string>> outputWithInline = myFirst.Combine(mySecond, false);
//        Expression<Func<Folder, string>> outputWithoutInline = myFirst.Combine(mySecond, true);

//        Expression<Func<Folder, string>> call =
//                ExpressionUtils.Combine<Folder, Document, string>(
//                gp => gp.Document, p => p.GenerateSuffix(p.Service.Name), true);

//            unchecked
//            {
//                Expression<Func<double, double>> mathUnchecked =
//                    ExpressionUtils.Combine<double, double, double>(x => (x * x) + x, x => x - (x / x), true);
//    }
//            checked
//            {
//                Expression<Func<double, double>> mathChecked =
//                    ExpressionUtils.Combine<double, double, double>(x => x - (x * x), x => (x / x) + x, true);
//}
//Expression<Func<int, int>> bitwise =
//    ExpressionUtils.Combine<int, int, int>(x => (x & 0x01) | 0x03, x => x ^ 0xFF, true);
//Expression<Func<int, bool>> logical =
//    ExpressionUtils.Combine<int, bool, bool>(x => x == 123, x => x != false, true);
//Expression<Func<int[][], int>> arrayAccess =
//    ExpressionUtils.Combine<int[][], int[], int>(x => x[0], x => x[0], true);
//Expression<Func<string, bool>> isTest =
//    ExpressionUtils.Combine<string, object, bool>(s => s, s => s is Regex, true);

//Expression<Func<List<int>>> f = () => new List<int>(new int[] { 1, 1, 1 }.Length);
//Expression<Func<string, Regex>> asTest =
//    ExpressionUtils.Combine<string, object, Regex>(s => s, s => s as Regex, true);
//var initTest = ExpressionUtils.Combine<int, int[], List<int>>(i => new[] { i, i, i },
//            arr => new List<int>(arr.Length), true);
//var anonAndListTest = ExpressionUtils.Combine<int, int, List<int>>(
//        i => new { age = i }.age, i => new List<int> { i, i }, true);
    }
}
