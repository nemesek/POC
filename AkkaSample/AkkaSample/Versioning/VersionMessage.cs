namespace AkkaSample.Versioning
{
    public class VersionMessage
    {
        private readonly bool _useNew;

        public VersionMessage(bool useNew)
        {
            _useNew = useNew;
        }

        public bool UseNew => _useNew;
    }
}
