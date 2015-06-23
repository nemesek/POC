using BiggerBallOfMud.After;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.After
{
    [TestClass]
    public class SubmittedOrderTests
    {
        [TestMethod]
        public void SubmittedOrder_ProcessNextStep_ReturnsOrderWithReviewSubmissionStatusByDefault()
        {
            // arrange
            var order = new SubmittedOrder(1, "38655", null);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.ReviewSubmission, result.Status);
        }

        [TestMethod]
        public void SubmittedOrder_ProcessNextStep_ReturnsOrderWithClosedStatusWhenCmsId21()
        {
            // arrange
            var order = new SubmittedOrder(21, "38655", null);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.Closed, result.Status);
        }

        [TestMethod]
        public void SubmittedOrder_ProcessNextStep_ReturnsOrderWithClosedStatusWhenCmsId14()
        {
            // arrange
            var order = new SubmittedOrder(14, "38655", null);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.Closed, result.Status);
        }

        [TestMethod]
        public void SubmittedOrder_ProcessNextStep_ReturnsOrderWithClosedStatusWhenCmsId24()
        {
            // arrange
            var order = new SubmittedOrder(24, "38655", null);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.Closed, result.Status);
        }
    }
}
