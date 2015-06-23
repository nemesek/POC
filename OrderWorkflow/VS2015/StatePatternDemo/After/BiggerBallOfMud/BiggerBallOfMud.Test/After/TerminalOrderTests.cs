using BiggerBallOfMud.After;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.After
{
    [TestClass]
    public class TerminalOrderTests
    {
        [TestMethod]
        public void TerminalOrder_ProcessNextStep_ReturnsOrderWithTerminalStatus()
        {
            // arrange
            var order = new TerminalOrder(1, "38655", null);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.Terminal, result.Status);

        }

    }
}
