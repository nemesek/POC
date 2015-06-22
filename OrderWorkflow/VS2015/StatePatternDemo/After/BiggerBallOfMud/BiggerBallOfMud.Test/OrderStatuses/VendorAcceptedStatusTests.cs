using BiggerBallOfMud.OrderStatuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.OrderStatuses
{
    [TestClass]
    public class VendorAcceptedStatusTests
    {
        [TestMethod]
        public void VendorAcceptedStatus_ProcessNextStep_ReturnsClosedStatusWhenClientIdIs5()
        {
            // arrange
            var order = new Order() {ClientId = 5};
            var status = new VendorAcceptedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as ClosedStatus;

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VendorAcceptedStatus_ProcessNextStep_ReturnsReviewAcceptanceStatusByDefault()
        {
            // arrange
            var order = new Order() {ClientId = 1};
            var status = new VendorAcceptedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as ReviewAcceptanceStatus;

            // assert
            Assert.IsNotNull(result);

        }
    }
}
