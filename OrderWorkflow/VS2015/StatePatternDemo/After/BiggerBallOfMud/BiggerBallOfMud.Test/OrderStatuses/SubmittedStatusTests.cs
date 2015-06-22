using BiggerBallOfMud.OrderStatuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.OrderStatuses
{
    [TestClass]
    public class SubmittedStatusTests
    {
        [TestMethod]
        public void SubmittedStatus_ProcessNextStep_ReturnsClosedStatusWhenClientID21()
        {
            // arrange
            var order = new Order() {ClientId = 21};
            var status = new SubmittedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as ClosedStatus;

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubmittedStatus_ProcessNextStep_ReturnsClosedStatusWhenClientID14()
        {
            // arrange
            var order = new Order() { ClientId = 14 };
            var status = new SubmittedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as ClosedStatus;

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubmittedStatus_ProcessNextStep_ReturnsClosedStatusWhenClientID24()
        {
            // arrange
            var order = new Order() { ClientId = 24 };
            var status = new SubmittedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as ClosedStatus;

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubmittedStatus_ProcessNextStep_ReturnsReviewSubmittionStatusByDefault()
        {
            // arrange
            var order = new Order() { ClientId = 1};
            var status = new SubmittedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as ReviewSubmissionStatus;

            // assert
            Assert.IsNotNull(result);
        }
    }
}
