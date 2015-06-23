using System;
using BiggerBallOfMud.After;
using BiggerBallOfMud.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.After
{
    [TestClass]
    public class AssignedOrderTests
    {
        [TestMethod]
        public void AssignedOrder_ProcessNextStep_ReturnsOrderWithStatusVendorAcceptedWhenVendorAccepts()
        {
            // arrange
            var vendor = new TestVendor(1, "38655", "Tom Smith") {AcceptAction = true};
            var order = new AssignedOrder(1, "38655", vendor);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.VendorAccepted, result.Status);
        }

        [TestMethod]
        public void AssignedOrder_ProcessNextStep_ReturnsOrderWithStatusUnassignedWhenVendorDeclines()
        {
            // arrange
            var vendor = new TestVendor(1, "38655", "Tom Smith") { AcceptAction = false };
            var order = new AssignedOrder(1, "38655", vendor);

            // act
            var result = order.ProcessNextStep();

            // assert
            Assert.AreEqual(OrderStatus.Unassigned, result.Status);
        }

        [TestMethod]
        public void AssignedOrder_Ctor_ThrowsArgumentNullExceptionWhenVendorIsNull()
        {
            // arrange
            const string expected = "vendor";
            var result = string.Empty;

            // act
            try
            {
                var order = new AssignedOrder(1, "38655", null);
            }
            catch (ArgumentNullException ane)
            {
                result = ane.ParamName;
            }

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}

