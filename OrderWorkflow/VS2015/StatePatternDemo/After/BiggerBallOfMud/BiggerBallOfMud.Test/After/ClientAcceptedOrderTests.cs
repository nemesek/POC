using BiggerBallOfMud.After;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.After
{
    [TestClass]
    public class ClientAcceptedOrderTests
    {
        [TestMethod]
        public void ClientAcceptedOrder_ProcessNextStep_ReturnsOrderWithSubmittedStatus()
        {
            // arrange
            var order = new ClientAcceptedOrder(1, "38655", null);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.Submitted, result.Status);
        }
    }
}
