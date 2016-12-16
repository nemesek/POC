using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public static class AppraiserWorkStyleComparableCalculator
    {
        public static IEnumerable<AppraiserWorkstyleEuclideanDistance> GetWorkStyleEuclideanDistances(IEnumerable<Appraiser> appraisers, DayOfWeek dayOfWeek)
        {
            var distances = appraisers.Select(a => CalculateAppraiserWorkStyleDistance(a, dayOfWeek)).OrderBy(c => c.EuclideanDistance);
            return distances;
        }

        private static double GetDistanceSquared(double x, double y)
        {
            return Math.Pow(x - y, 2);
        }

        private static AppraiserWorkstyleEuclideanDistance CalculateAppraiserWorkStyleDistance(Appraiser appraiser, DayOfWeek dayOfWeek)
        {
            var dayDelta = 1 - appraiser.GetWorkStyleForDay(dayOfWeek);
            var dayComponent = GetDistanceSquared(0.0, dayDelta);
            var rejectDelta = appraiser.ChangeInspectionDatePercentage;
            var rejectComponent = GetDistanceSquared(0.0, rejectDelta);
            var euclideanDistance = Math.Sqrt(dayComponent + rejectComponent) /10;
            return new AppraiserWorkstyleEuclideanDistance(appraiser, euclideanDistance);
        }
    }
}
