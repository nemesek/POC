using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ExpressionConverter.Converters;
using ExpressionConverter.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionConverter.Tests
{
    [TestClass]
    public class Demos
    {
        [TestMethod]
        public void Demo_ConvertDomainExpressionToDtoExpression_ReturnsDtoExpressionWhenGivenDomainExpression()
        {
            // uses ParameterReplacer under the covers
            Expression<Func<DomainOrder, int, bool>> expression = (o, id) => o.OrderId == id;
            var target = new OrderExpressionConverter();

            // act
            var result = target.ConvertDomainExpressionToDtoExpression(expression);

            // assert
            Assert.IsNotNull(result);
            var dtoResult = (Expression<Func<OrderDto, int, bool>>)result; // redundant just for demo purposes
            Assert.IsNotNull(dtoResult);
            var orderDto = new OrderDto { OrderId = 1 };
            const int orderId = 2;
            var match = result.Compile().Invoke(orderDto, orderId);
            Assert.IsFalse(match);
        }

        [TestMethod]
        public void Demo_ConvertDomainExpressionToDtoExpression_ReturnsDtoExpressionWhenGivenDomainExpressionWith2Predicates()
        {
            // arrange
            // uses ParameterReplacer under the covers
            Expression<Func<DomainOrder, string, int, bool>> domainExpression = (o, zip, id) => o.ZipCode == zip && o.OrderId == id;
            var target = new OrderExpressionConverter();

            // act
            var result = target.ConvertDomainExpressionToDtoExpression(domainExpression);

            // assert
            Assert.IsNotNull(result);
            var orderDto = new OrderDto { OrderId = 1, ZipCode = "38655" };
            var match = result.Compile().Invoke(orderDto, "38655", 1);
            Assert.IsTrue(match);

        }

        [TestMethod]
        public void Demo_ConvertExpressionWithPrimitivesToOrderDtoExpression_ReturnsExpressionWhereOrderDtoMatchesOnTwoPredicatesWhenGivenPrimitives() 
        {
            // this would be handy if we found a way to strip out the DomainOrder from the first expr
            // uses ReplaceVisitor under the covers
            // arrange
            Expression<Func<string, int, bool>> expression = (zip, id) => zip == "38655" && id == 1;

            // act
            var result = OrderExpressionConverter.ConvertExpressionWithPrimitivesToOrderDtoExpression(expression);

            // assert
            Assert.IsNotNull(result);
            var orderDto = new OrderDto { DocId = 1, ZipCode = "38655" };
            var match = result.Compile().Invoke(orderDto);
            Assert.IsTrue(match);

        }

        [TestMethod]
        public void Demo_TranslateDomainExpressionToOrderDtoExpression() 
        {
            // this scenario is probably more of what we would need and doesn't require inheritance
            // uses TypeChangeVisitor
            // arrange
            var zipCode = "38655";
            var orderId = 1;
            var orderDto = new OrderDto { ZipCode = "38655", OrderId = orderId };
            Expression<Func<DomainOrder, bool>> domainExpression = (o) => o.ZipCode == zipCode && o.OrderId == orderId;

            // act
            var converted = TypeChangeVisitor.Translate<DomainOrder, OrderDto>(domainExpression);

            // assert
            Assert.IsNotNull(converted);
            var match = converted.Compile().Invoke(orderDto);
            Assert.IsTrue(match);
        }

        [TestMethod]
        public void Demo_TranslateDomainExpressionToVendorPocoExpression()
        {
            // uses TypeChangeVisitor doesn't use inheritance
            // arrange
            var filter = "12345";
            var vendorPoco = new VendorPoco { Id = "12345" };
            Expression<Func<Vendor, bool>> domainExpression = (v) => v.Id == filter;

            // act
            var converted = TypeChangeVisitor.Translate<Vendor, VendorPoco>(domainExpression);

            // assert
            Assert.IsNotNull(converted);
            var match = converted.Compile().Invoke(vendorPoco);
            Assert.IsTrue(match);
        }

        [TestMethod]
        public void SimpleExpression()
        {
            var firstArg = Expression.Constant(2);
            var secondArg = Expression.Constant(3);
            var add = Expression.Add(firstArg, secondArg);
            Trace.WriteLine(add);
            Func<int> compiled = Expression.Lambda<Func<int>>(add).Compile();
            var result = compiled();
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void MoreComplicatedExpression()
        {
            Expression<Func<string, string, bool>> expression = (x, y) => x.StartsWith(y);
            var compiled = expression.Compile();
            Assert.IsTrue(compiled("Hi", "Hi"));
            
            // the following is equivalent

            // build up parts of method call
            MethodInfo method = typeof (string).GetMethod("StartsWith", new[] {typeof (string)});
            var target = Expression.Parameter(typeof (string), "x"); // the string your calling StartWith on
            var methodArg = Expression.Parameter(typeof (string), "y");
            Expression[] methodArgs = new[] {methodArg};

            // Creates CallExpression from parts
            Expression call = Expression.Call(target, method, methodArg);

            // Converts call into LambdaExpression
            var lambdaParameters = new[] {target, methodArg};
            var lambda = Expression.Lambda<Func<string, string, bool>>(call, lambdaParameters);

            var compiled2 = lambda.Compile();
            Assert.IsTrue(compiled2("Hi", "Hi"));

        }

        [TestMethod]
        public void DbQueryProvider_ReturnsListOfEmployeesWhenGivenLambdaWithWhereContainingStringLiteral()
        {
            // arrange 
            const string constr = @"Data Source =.\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security = True";
            var employees = new List<Employees>();
            // act
            using (var connection = new SqlConnection(constr))
            {
                connection.Open();
                var db = new Northwind(connection);
                var query = db.Employees.Where(c => c.City == "London");
                var list = query.ToList();
                employees.AddRange(list);
            }

            // Assert
            Assert.IsTrue(employees.Count == 4);
        }

        [TestMethod]
        public void DbQueryProvider_ReturnsListOfEmployeesWhenGivenLambdaWithWhereContainsLocal()
        {
            // arrange 
            const string constr = @"Data Source =.\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security = True";
            var employees = new List<Employees>();
            // act
            using (var connection = new SqlConnection(constr))
            {
                var city = "London"; // local we pass to the where clause
                connection.Open();
                var db = new Northwind(connection);
                var query = db.Employees.Where(c => c.City == city);
                var list = query.ToList();
                employees.AddRange(list);
            }

            // Assert
            Assert.IsTrue(employees.Count == 4); ;
        }

        [TestMethod]
        public void DbQueryProvider_ReturnsListOfEmployeesAndProjectsClassToCorrectType()
        {
            // arrange 
            const string constr = @"Data Source =.\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security = True";
            // act
            using (var connection = new SqlConnection(constr))
            {
                var city = "London"; // local we pass to the where clause
                connection.Open();
                var db = new Northwind(connection);
                var query = db.Employees.Where(c => c.City == city).Select(e => new {Id = e.EmployeeID, City = e.City});
                var list = query.ToList();
                
                // Assert
                Assert.IsTrue(list.Count == 4); ;
            }
        }
    }
}
