using System;
using System.Collections.Generic;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.Services;
using DnxConsole.Infrastructure.DataAccess.Repositories;
using DnxConsole.Infrastructure.Services;

namespace DnxConsole.Controllers
{
    public class CmsFactory
    {
        private readonly ILogEvents _eventLogger = new EventLogger();
        private readonly ISendExternalMessenges _messenger = new RestfulMessenger();

        //private static readonly Dictionary<OrderContext, Func<int, IOrderContextRepository>>
        //    RepositoryMap = new Dictionary<OrderContext, Func<int, IOrderContextRepository>>
        //{
        //    {OrderContext.Workflow, (id) => WorkflowRepositoryMap[id]()}
        //};

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

        private Cms GetWorkflowCms(int cmsId)
        {
            var repodId = cmsId%4;
            var orderFactory = GetOrderWorkflowFactory(repodId);
            var transitionFactory = new OrderTransitionerFactory(orderFactory);
            return new Cms(cmsId, _eventLogger, _messenger, transitionFactory);
        }

        private WorkflowOrderFactory GetOrderWorkflowFactory(int repoId)
        {
            var repository = WorkflowRepositoryMap[repoId]();
            return new WorkflowOrderFactory(repository);
        }

        private IOrderContextRepository GetRepositoryContext(OrderContext context, int cmsId)
        {
            //var repoId = cmsId % 4;
            //return RepositoryMap[context](repoId);
            return null;
        }
    }
}
