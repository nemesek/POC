using System;
using System.Collections.Generic;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.AutoAssign
{
    public class AutoAssignFactory
    {
        private static Dictionary<int, Func<IProcessAutoAssign>> _funcDictionary = new Dictionary
            <int, Func<IProcessAutoAssign>>
        {
            {0, () => new DefaultAutoAssign()},
            {1, () => new CmsNetAutoAssign()},
            {2, () => new CmsNextAutoAssign()},
            {3, () => new LegacyAutoAssign()}
        };
        public static IProcessAutoAssign CreateAutoAssign(int id)
        {
            var moddedId = id%4;
            var func = _funcDictionary[moddedId];
            return func();

        }
    }
}
