namespace ConsoleApplication1
{
    public static class DistanceCalculator
    {
        public static double GetDistanceBetweenZips(int zipOne, int zipTwo)
        {
            return Repository.GetDistanceBetweenZips(zipOne, zipTwo);
        }
    }
}
