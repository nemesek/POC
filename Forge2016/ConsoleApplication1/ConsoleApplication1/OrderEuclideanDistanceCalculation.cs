namespace ConsoleApplication1
{
    public class OrderEuclideanDistanceCalculation
    {
        private readonly Order _order1;
        private readonly Order _order2;
        private readonly double _distance;
        public OrderEuclideanDistanceCalculation(Order order1, Order order2, double distance)
        {
            _order1 = order1;
            _order2 = order2;
            _distance = distance;
        }

        public Order OrderOne => _order1;
        public Order OrderTwo => _order2;
        public double Distance => _distance;
    }
}
