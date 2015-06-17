using System;
using System.Collections.Generic;
using DnxConsole.Domain.Common.Contracts;
using DnxConsole.Domain.OrderWorkflowContext;
using DnxConsole.Domain.OrderWorkflowContext.AutoAssign;
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
        
        public Cms GetWorkflowCms(int cmsId)
        {
            var repodId = cmsId%4;
            var orderFactory = GetOrderWorkflowFactory(repodId);
            var transitionFactory = new StateMachineFactory(orderFactory);
            var autoAssignFactory = new AutoAssignFactory(new VendorRepository());
            return new Cms(cmsId, _eventLogger, _messenger, transitionFactory, autoAssignFactory);
        }

        public Domain.OrderEditContext.Cms GetOrderEditCms(int cmsId)
        {
            var repository = new OrderRepository();
            return new Domain.OrderEditContext.Cms(cmsId, _eventLogger, _messenger, repository);
        }

        public Domain.OrderCreationContext.Cms GetOrderCreationCms(int cmsId)
        {
            var repository = new OrderRepository();
            return new Domain.OrderCreationContext.Cms(cmsId, _eventLogger, _messenger, repository);
        }

        private static WorkflowOrderFactory GetOrderWorkflowFactory(int repoId)
        {
            var repository = WorkflowRepositoryMap[repoId]();
            return new WorkflowOrderFactory(repository);
        }
    }
}
