using BiggerBallOfMud.OrderStatuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.OrderStatuses
{
    [TestClass]
    public class ClientAcceptedStatusTests
    {
        [TestMethod]
        public void ClientAcceptedStatus_ProcessNextStep_ReturnsSubmittedStatus()
        {
            // arrange
            var order = new Order();
            var status = new ClientAcceptedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as SubmittedStatus;

            // assert
            Assert.IsNotNull(result);
        }
    }
}
