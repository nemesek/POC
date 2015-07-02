namespace Strategy
{
    public class DefaultAutoAssign : IProcessAutoAssign
    {
        private readonly int _cmsId;
        public DefaultAutoAssign(int cmsId)
        {
            _cmsId = cmsId;
        }
        public string RunAutoAssignLogic()
        {
            return $"Running default Logic for CMS {_cmsId}";
        }
    }
}
