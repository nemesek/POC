using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public static class OrderPropertyComparableCalculator
    {
        public static IEnumerable<EuclideanDistanceBetweenTwoOrders> RankPropertyComparables(Order order)
        {
            return Repository
                .GetOrdersInspectedWitinLastNumberOfDays(30)
                .Where(o => o.ZipCode == order.ZipCode)
                .Select(o => new EuclideanDistanceBetweenTwoOrders(order, o, GetEuclideanDistance(order, o)))
                .OrderBy(c => c.Distance);
        }

        public static IEnumerable<EuclideanDistanceBetweenTwoOrders> RankPropertyComparables(Order order, IEnumerable<Appraiser> appraisers)
        {
            return appraisers
                .Select(a => a.LoadOrdersIHaveDoneInPast()
                .Where(o => o.ZipCode == order.ZipCode)
                .Select(o => new EuclideanDistanceBetweenTwoOrders(order, o, GetEuclideanDistance(order, o)))
                .OrderBy(c => c.Distance)
                .FirstOrDefault())
                .Where(d => d != null)
                .OrderBy(d =>  d.Distance);
        }

        private static double GetEuclideanDistance(Order order1, Order order2)
        {
            var squareFeetComponent = GetDistanceSquared(order1.SquareFeet, order2.SquareFeet) / 2200;
            var ageComponent = GetDistanceSquared(order1.YearBuilt, order2.YearBuilt) / 40;
            return Math.Sqrt(squareFeetComponent + ageComponent);
        }

        private static double GetDistanceSquared(int x, int y)
        {
            return Math.Pow(x - y, 2);
        }
    }
}
