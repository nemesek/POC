namespace AkkaSample.Science
{
    public class ScientificMessage
    {
        private readonly bool _isExperiement;

        public ScientificMessage(bool isExperiement)
        {
            _isExperiement = isExperiement;
        }

        public bool IsExperiment => _isExperiement;
    }
}
