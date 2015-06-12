using System;
using System.Collections.Generic;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Vendors;

namespace DnxConsole.Domain.OrderWorkflowContext.AutoAssign
{
    public class AutoAssignFactory
    {
        private static readonly Dictionary<int, Func<IVendorRepository,IProcessAutoAssign>> FuncDictionary = new Dictionary
            <int, Func<IVendorRepository,IProcessAutoAssign>>
        {
            {0, (r) => new DefaultAutoAssign(r)},
            {1, (r) => new CmsNetAutoAssign(r)},
            {2, (r) => new CmsNextAutoAssign(r)},
            {3, (_) => new LegacyAutoAssign()}
        };

        private readonly IVendorRepository _repository;

        public AutoAssignFactory(IVendorRepository repository)
        {
            _repository = repository;
        }
        public IProcessAutoAssign CreateAutoAssign(int id)
        {
            var moddedId = id%4;
            var func = FuncDictionary[moddedId];
            return func(_repository);
        }
    }
}
