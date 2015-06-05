namespace Strategy.Domain.StrategyOCP
{
	public class Custom1AutoAssign : IProcessAutoAssign
	{
		private readonly int _cmsId;
		
		public Custom1AutoAssign(int cmsId)
		{
			_cmsId = cmsId;
		}
		
		public string RunAutoAssignLogic()
		{
			return $"Running custom 1 logic for CMS {_cmsId}";
		}
	}
}