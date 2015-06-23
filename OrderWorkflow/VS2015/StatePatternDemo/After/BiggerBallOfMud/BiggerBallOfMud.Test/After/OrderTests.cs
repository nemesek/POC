using System;
using BiggerBallOfMud.After;
using BiggerBallOfMud.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.After
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void Order_AssignVendor_ThrowsExceptionWhenOrderNotInUnassignedOrManualAssignStatus()
        {
            // arrange
            const string expected = "Order is not in correct state to be Assigned.";
            var order = new TestOrder(1, "38655", null);
            order.SetStatus(OrderStatus.Submitted);
            var vendor = new TestVendor(0, "38655", "Tom Smith");

            // act
            var result = string.Empty;
            try
            {
                order.AssignVendor(vendor);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}
