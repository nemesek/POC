using BiggerBallOfMud.OrderStatuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.OrderStatuses
{
    [TestClass]
    public class RejectedStatusTests
    {
        [TestMethod]
        public void RejectedStatus_ProcessNextStep_ReturnsReviewSubmissionStatus()
        {
            // arrange
            var order = new Order();
            var status = new RejectedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as ReviewSubmissionStatus;

            // assert
            Assert.IsNotNull(result);
        }
    }
}
