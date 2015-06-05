using System;
using System.Collections.Generic;

namespace Strategy.Domain.StrategyOCP
{
	public class AutoAssignFactory
	{
        private static readonly Dictionary<int, Func<IProcessAutoAssign>> FuncDictionary = new Dictionary
            <int, Func<IProcessAutoAssign>>
        {
            {0, () => new DefaultAutoAssign(0)},
            {1, () => new Custom1AutoAssign(1)},
            {2, () => new Custom2AutoAssign(2)},
            //  {3, () => new LegacyAutoAssign()}
        };
		public static IProcessAutoAssign GetAutoAssignLogic(int cmsId)
		{
			var id = cmsId % 3;
			return FuncDictionary[id].Invoke();
		}
		
	}
}