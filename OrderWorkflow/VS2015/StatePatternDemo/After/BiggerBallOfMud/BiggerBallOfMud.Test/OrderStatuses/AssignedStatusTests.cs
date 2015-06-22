using BiggerBallOfMud.OrderStatuses;
using BiggerBallOfMud.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.OrderStatuses
{
    [TestClass]
    public class AssignedStatusTests
    {
        [TestMethod]
        public void AssignedStatus_ProcessNextStep_ReturnsVendorAcceptedStatusWhenVendorAcceptsOrder()
        {
            // arrange
            var order = new Order();
            var vendor = new TestVendor(0, "38655", "Test User") {AcceptAction = true};
            order.Vendor = vendor;
            var status = new AssignedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as VendorAcceptedStatus;

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AssignedStatus_ProcessNextStep_ReturnsUnassignedStatusWhenVendorRejectsOrder()
        {
            // arrange
            var order = new Order();
            var vendor = new TestVendor(0, "38655", "Test User") {AcceptAction = false};
            order.Vendor = vendor;
            var status = new AssignedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as UnassignedStatus;

            // assert
            Assert.IsNotNull(result);
        }
    }
}
