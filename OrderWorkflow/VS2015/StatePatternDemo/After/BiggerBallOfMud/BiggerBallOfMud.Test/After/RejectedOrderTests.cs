using BiggerBallOfMud.After;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.After
{
    [TestClass]
    public class RejectedOrderTests
    {
        [TestMethod]
        public void RejectedOrder_ProcessNextStep_ReturnsOrderWithStatusReviewSubmission()
        {
            // arrange
            var order = new RejectedOrder(1, "38655", null);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.ReviewSubmission, result.Status);
        }
    }
}
