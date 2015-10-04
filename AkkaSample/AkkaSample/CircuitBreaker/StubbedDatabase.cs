namespace AkkaSample.CircuitBreaker
{
    public class StubbedDatabase
    {
        public static bool IsDown { get; set; }

        public static void RunQuery ()
        {
            if (IsDown)
            {
                throw new InfrastructureException();
            }
        }
    }
}
