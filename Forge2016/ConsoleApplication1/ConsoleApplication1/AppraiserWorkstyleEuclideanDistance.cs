namespace ConsoleApplication1
{
    public class AppraiserWorkstyleEuclideanDistance
    {
        private Appraiser _appraiser;
        private double _euclideanDistance;

        public AppraiserWorkstyleEuclideanDistance(Appraiser appraiser, double euclideanDistance)
        {
            _appraiser = appraiser;
            _euclideanDistance = euclideanDistance;
        }

        public Appraiser Appraiser => _appraiser;
        public double EuclideanDistance => _euclideanDistance;
    }
}
