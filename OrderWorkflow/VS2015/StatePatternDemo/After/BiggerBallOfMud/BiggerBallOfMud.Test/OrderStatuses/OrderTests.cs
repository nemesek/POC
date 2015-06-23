using BiggerBallOfMud.OrderStatuses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BiggerBallOfMud.Test.OrderStatuses
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void Order_Ctor_ConstructsOrderWithUnassignedStatus()
        {
            // arrange
            

            // act
            var order = new Order();
            var unassignedState = order.Status as UnassignedStatus;

            // assert
            Assert.IsNotNull(unassignedState);
        }
    }
}
