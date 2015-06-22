using BiggerBallOfMud.OrderStatuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.OrderStatuses
{
    [TestClass]
    public class ClosedStatusTests
    {
        [TestMethod]
        public void ClosedStatus_ProcessNextStep_ReturnsNullStatus()
        {
            // arrange
            var order = new Order();
            var status = new ClosedStatus(order);

            // act
            var result = status.ProcessNextStep();

            // assert
            Assert.IsNull(result);
        }
    }
}
