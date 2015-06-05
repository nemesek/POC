namespace Strategy.Domain.StrategyOCP
{
	public class Custom2AutoAssign : IProcessAutoAssign
	{
		private readonly int _cmsId;
		
		public Custom2AutoAssign(int cmsId)
		{
			_cmsId = cmsId;
		}
		
		public string RunAutoAssignLogic()
		{
			return $"Running custom 2 logic for CMS {_cmsId}";
		}
	
	}
}