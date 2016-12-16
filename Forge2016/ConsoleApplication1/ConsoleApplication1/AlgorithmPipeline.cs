using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class AlgorithmPipeline
    {
        public static IEnumerable<Candidate> FindAppraisersForOrderUsingDistance(Order order)
        {
            var orderDistances = FindClosestOrders(order)
                .GroupBy(od => od.OrderTwo.UserId)
                .Select(g => g.First());

            // only look at appraisers who are close
            var appraisers = orderDistances
                .Select(od => od.OrderTwo).Select(o => Repository.GetAppraiser(o.UserId));
            // look at their workstyles
            var appraiserWorkStyleDistances = AppraiserWorkStyleComparableCalculator
                .GetWorkStyleEuclideanDistances(appraisers, order.InspectionDate.DayOfWeek);
            // see if they have any comps
            var propertyDistances = OrderPropertyComparableCalculator
                .RankPropertyComparables(order, appraisers);

            var candidates = appraisers
                .Select(appraiser => BuildCandidate(appraiser, appraiserWorkStyleDistances, propertyDistances, orderDistances))
                .ToList();

            return candidates.OrderBy(c => c.TotalDistance);
        }

        private static Candidate BuildCandidate(
             Appraiser appraiser,
            IEnumerable<AppraiserWorkstyleEuclideanDistance> appraiserWorkStyleDistances,
            IEnumerable<EuclideanDistanceBetweenTwoOrders> propertyDistances,
            IEnumerable<EuclideanDistanceBetweenTwoOrders> orderDistances)
        {
            var workStyle =
                appraiserWorkStyleDistances.Single(ws => ws.Appraiser.UserId == appraiser.UserId);

            var propertyComp = propertyDistances.Single(pc => pc.OrderTwo.UserId == appraiser.UserId);

            var closestOrder = orderDistances.Single(od => od.OrderTwo.UserId == appraiser.UserId);

            return new Candidate(appraiser,closestOrder, propertyComp, workStyle);
        }


        private static IEnumerable<EuclideanDistanceBetweenTwoOrders> FindClosestOrders(Order order)
        {
            return OrderScheduleAndDistanceComparableCalculator.RankOrderScheduleAndDistanceComparables(order);
        }

    }
}
