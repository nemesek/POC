using System;
using System.Collections.Generic;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.AutoAssign
{
    public class AutoAssignFactory
    {
        private static readonly Dictionary<int, Func<IProcessAutoAssign>> FuncDictionary = new Dictionary
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
            var func = FuncDictionary[moddedId];
            return func();
        }
    }
}
