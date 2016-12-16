using System;
using ConsoleApplication1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class AlgorithmPipelineTests
    {
        [TestMethod]
        public void AlgorithmPipeline_FindAppraisersForOrder_ReturnsEnumerableOfAppraisers()
        {
            // arrange
            var order = new Order(9999, DateTime.Now.AddDays(5), 38655, 1, 2015, 2000);
            // act
            var result = AlgorithmPipeline.FindAppraisersForOrderUsingDistance(order);

            // assert
            Assert.IsNotNull(result);
        }


    }
}
