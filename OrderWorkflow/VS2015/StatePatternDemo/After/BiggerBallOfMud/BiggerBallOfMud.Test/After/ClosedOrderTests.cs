using BiggerBallOfMud.After;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.After
{
    [TestClass]
    public class ClosedOrderTests
    {
        [TestMethod]
        public void ClosedOrder_ProcessNextStep_ReturnsOrderWithTerminalStatus()
        {
            // arrange
            var order = new ClosedOrder(1, "38655", null);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.Terminal, result.Status);
        }
    }
}
