namespace Strategy
{
    public class Custom3AutoAssign : IProcessAutoAssign
    {
        private readonly int _cmsId;

        public Custom3AutoAssign(int cmsId)
        {
            _cmsId = cmsId;
        }

        public string RunAutoAssignLogic()
        {
            return $"Running custom 3 logic for CMS {_cmsId}";
        }
    }
}
