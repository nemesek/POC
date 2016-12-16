using System;

namespace ConsoleApplication1
{
    public class Candidate
    {
        private readonly EuclideanDistanceBetweenTwoOrders _orderDistance;
        private readonly EuclideanDistanceBetweenTwoOrders _propertyDistance;
        private readonly AppraiserWorkstyleEuclideanDistance _workstyleDistance;
        private readonly double _totalDistance;
        private readonly Appraiser _appraiser;

        public Candidate(
            Appraiser appraiser,
            EuclideanDistanceBetweenTwoOrders orderDistance, 
            EuclideanDistanceBetweenTwoOrders propertyDistance, 
            AppraiserWorkstyleEuclideanDistance workstyleDistance)
        {
            _appraiser = appraiser;
            _orderDistance = orderDistance;
            _propertyDistance = propertyDistance;
            _workstyleDistance = workstyleDistance;
            _totalDistance = CalculateTotalDistance();
        }

        public EuclideanDistanceBetweenTwoOrders OrderDistance => _orderDistance;
        public EuclideanDistanceBetweenTwoOrders PropertyDistance => _propertyDistance;
        public AppraiserWorkstyleEuclideanDistance WorkstyleDistance => _workstyleDistance;
        public double TotalDistance => _totalDistance;
        public Appraiser Appraiser => _appraiser;

        private double CalculateTotalDistance()
        {
            var orderComponent = GetDistanceSquared(0, _orderDistance.Distance);
            var propertyComponent = GetDistanceSquared(0, _propertyDistance.Distance);
            var workComponent = GetDistanceSquared(0, _workstyleDistance.EuclideanDistance);
            return Math.Sqrt(orderComponent + propertyComponent + workComponent);
        }
        private static double GetDistanceSquared(double x, double y)
        {
            return Math.Pow(x - y, 2);
        }

    }
}
