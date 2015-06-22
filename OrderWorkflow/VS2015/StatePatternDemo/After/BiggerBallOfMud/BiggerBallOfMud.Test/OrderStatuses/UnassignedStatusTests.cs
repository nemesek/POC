using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiggerBallOfMud.OrderStatuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.OrderStatuses
{
    [TestClass]
    public class UnassignedStatusTests
    {
        [TestMethod]
        public void UnassignedStatus_ProcessNextStep_ReturnsAssignedStatusWhenVendorAssigned()
        {
            // arrange
            var order = new Order() {ClientId = 1, ZipCode = "38655"};
            var status = new UnassignedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as AssignedStatus;

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UnassignedStatus_ProcessNextStep_ReturnsManualAssignWhenVendorNotAssigned()
        {
            // arrange
            var order = new Order() {ClientId = 3};
            var status = new UnassignedStatus(order);

            // act
            var newStatus = status.ProcessNextStep();
            var result = newStatus as ManualAssignStatus;

            // assert
            Assert.IsNotNull(result);
        }
    }
}
