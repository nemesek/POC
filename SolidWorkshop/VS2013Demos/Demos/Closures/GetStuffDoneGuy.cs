namespace Closures
{
    public class GetStuffDoneGuy : IGetStuffDone
    {
        private const int MyField = 10;

        public int DoSomething(int x)
        {
            return x + MyField;
        }

        public int DoSomethingElse()
        {
            return MyField;
        }
    }
}
