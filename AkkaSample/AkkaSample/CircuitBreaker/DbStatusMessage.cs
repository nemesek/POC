namespace AkkaSample.CircuitBreaker
{
    public class DbStatusMessage
    {
        public DbStatusMessage(bool isAvailable, bool isResponsive)
        {
            IsAvailable = isAvailable;
            IsResponsive = IsAvailable && isResponsive;
        }

        public bool IsAvailable { get; }
        public bool IsResponsive { get; }
    }
}
