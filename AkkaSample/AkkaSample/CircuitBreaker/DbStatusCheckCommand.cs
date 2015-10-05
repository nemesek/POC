namespace AkkaSample.CircuitBreaker
{
    public class DbStatusCheckCommand
    {
       
        public DbStatusCheckCommand(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
