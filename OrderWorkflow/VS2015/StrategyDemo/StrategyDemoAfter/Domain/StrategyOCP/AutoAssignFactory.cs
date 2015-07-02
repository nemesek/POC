using System;
using System.Collections.Generic;

namespace Strategy.Domain.StrategyOCP
{
	public class AutoAssignFactory
	{
        private static readonly Dictionary<int, Func<int, IProcessAutoAssign>> FuncDictionary = new Dictionary
            <int, Func<int, IProcessAutoAssign>>
        {
            {0, (id) => new DefaultAutoAssign(id)},
            {1, (id) => new Custom1AutoAssign(id)},
            {2, (id) => new Custom2AutoAssign(id)},
            {3, (id) => new Custom3AutoAssign(id)}
        };
		public static IProcessAutoAssign GetAutoAssignLogic(int cmsId)
		{
			var id = cmsId % 4;
			return FuncDictionary[id](cmsId);
		}

	    //public static Func<string> GetAutoAssignLogicFunc(int cmsId)
	    //{
	    //    var id = cmsId%4;
	    //    return () => FuncDictionary[id](cmsId).RunAutoAssignLogic();
	    //}
		
	}
}