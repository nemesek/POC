using System;
using System.Collections.Generic;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Services;
using DnxConsole.Infrastructure.DataAccess.Repositories;
using DnxConsole.Infrastructure.Services;

namespace DnxConsole.Infrastructure
{
    public class CmsFactory
    {
        private readonly ILogEvents _eventLogger = new EventLogger();
        private readonly ISendExternalMessenges _messenger = new RestfulMessenger();
        private static readonly Dictionary<int, Func<IOrderRepository>> 
            WorkflowRepositoryMap = new Dictionary<int, Func<IOrderRepository>>
        {
            {0, () => new CmsNextOrderWorkflowRepository()},
            {1, () => new CmsNetOrderWorkflowRepository()},
            {2, () => new CmsNextOrderWorkflowRepository()},
            {3, () => new LegacyOrderWorkflowRepository()}
        };

        public Cms GetCms(int cmsId, OrderContext context)
        {
            return GetWorkflowCms(cmsId);
        }

        public Domain.OrderWorkflowContext.Cms GetWorkflowCms(int cmsId)
        {
            var repodId = cmsId%4;
            var orderFactory = GetOrderWorkflowFactory(repodId);
            var transitionFactory = new OrderTransitionerFactory(orderFactory);
            return new Domain.OrderWorkflowContext.Cms(cmsId, _eventLogger, _messenger, transitionFactory);
        }

        public Domain.OrderEditContext.Cms GetOrderEditCms(int cmsId)
        {
            return new Domain.OrderEditContext.Cms(cmsId, _eventLogger, _messenger);
        }

        public Domain.OrderCreationContext.Cms GetOrderCreationCms(int cmsId)
        {
            return new Domain.OrderCreationContext.Cms(cmsId, _eventLogger, _messenger);
        }

        private static WorkflowOrderFactory GetOrderWorkflowFactory(int repoId)
        {
            var repository = WorkflowRepositoryMap[repoId]();
            return new WorkflowOrderFactory(repository);
        }
    }
}
