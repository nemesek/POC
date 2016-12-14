using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var order = Repository.GetOpenOrders().First();
            // act
            var result = AlgorithmPipeline.FindAppraisersForOrderUsingDistance(order);

            // assert
            Assert.IsNotNull(result);
        }
    }
}
