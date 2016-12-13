using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DistanceCalculatorTests
    {
        [TestMethod]
        public void DistanceCalculator_ReturnsDistanceWhenGivenTwoZips()
        {
            // arrange
            var zip = Repository.GetZip(38655);
            var zipTwo = zip.LoadZipsWithinFiveMiles().First(z => z.ZipCode != zip.ZipCode);

            // act
            var result = DistanceCalculator.GetDistanceBetweenZips(zip.ZipCode, zipTwo.ZipCode);

            // assert
            Assert.IsTrue(result > 0);
            Assert.IsTrue(result < 5);
        }
    }
}
