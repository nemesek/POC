using System.Linq;
using ConsoleApplication1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class OrderComparableCalculatorTests
    {
        [TestMethod]
        public void ComparableCalculator_ReturnsZeroDistance_WhenAllFeaturesMatch()
        {
            // arrange
            var order = Repository.GetOrdersInspectedWitinLastNumberOfDays(30).First();

            // act
            var comp = OrderComparableCalculator.RankComparables(order).First();

            // assert
            Assert.IsTrue(comp.Distance == 0);
        }

        [TestMethod]
        public void ComparableCalculator_ReturnsPositiveDistance_WhenAllFeaturesDoNotMatch()
        {
            // arrange
            var order = Repository.GetOrdersInspectedWitinLastNumberOfDays(30).First();

            // act
            var comp = OrderComparableCalculator.RankComparables(order).First(c => c.Distance > 0);

            // assert
            Assert.IsTrue(comp.Distance > 0);
        }
    }
}
