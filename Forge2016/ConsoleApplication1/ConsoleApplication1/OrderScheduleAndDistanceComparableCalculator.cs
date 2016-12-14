using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class OrderScheduleAndDistanceComparableCalculator
    {
        public static IEnumerable<EuclideanDistanceBetweenTwoOrders> RankOrderScheduleAndDistanceComparables(Order order)
        {
            var orderZip = Repository.GetZip(order.ZipCode);
            var zips = orderZip.LoadZipsWithinTenMiles().Select(z => z.ZipCode);
            return Repository.GetOpenOrders()
                .Where(o => zips.Contains(o.ZipCode))
                .Select(o => new EuclideanDistanceBetweenTwoOrders(order, o, GetEuclideanDistance(order, o, orderZip)))
                .OrderBy(c => c.Distance);
        }
        private static double GetEuclideanDistance(Order order1, Order order2, Zip originZip)
        {
            var distanceBetweenZips = Repository.GetDistanceBetweenZips(order1.ZipCode, order2.ZipCode);
            var populationDensity = originZip.CalculatePopulationDensityForTenMiles()/15000;
            var distanceWithTraffic = (populationDensity*distanceBetweenZips);
            var driveTimeComponent = GetDistanceSquared(0.0, distanceWithTraffic);
            var dayDiff = Math.Pow((order1.InspectionDate - order2.InspectionDate).TotalDays, 2);
            var scheduledDateComponent = GetDistanceSquared(0.0, dayDiff);
            return Math.Sqrt(driveTimeComponent + scheduledDateComponent);
        }

        private static double GetDistanceSquared(double x, double y)
        {
            return Math.Pow(x - y, 2);
        }
    }
}
