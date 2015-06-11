using System;
using System.Collections.Generic;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;
using DnxConsole.Infrastructure.DataAccess.Repositories;

namespace DnxConsole.Controllers
{
    public class ContextSelector
    {
        private static readonly Dictionary<OrderContext, Func<int,IOrderContextRepository>>
            RepositoryMap = new Dictionary<OrderContext, Func<int,IOrderContextRepository>>
            {
                {OrderContext.Workflow, (id) => WorkflowRepositoryMap[id]()}
            };

        private static readonly Dictionary<int, Func<IOrderContextRepository>> WorkflowRepositoryMap = new Dictionary<int, Func<IOrderContextRepository>>
        {
            {0, () => new CmsNextOrderWorkflowRepository()},
            {1, () => new CmsNetOrderWorkflowRepository()},
            {2, () => new CmsNextOrderWorkflowRepository()},
            {3, () => new LegacyOrderWorkflowRepository()}
        };

        public IOrderContextRepository GetRepositoryContext(OrderContext context, int cmsId)
        {
            var repoId = cmsId%4;
            return RepositoryMap[context](repoId);
        }
    }
}
