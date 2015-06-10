using System;
using System.Collections.Generic;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.DerivedOrders;
using DnxConsole.Infrastructure.DataAccess;

namespace DnxConsole.Domain.OrderWorkflowContext.Services
{
    public class WorkflowOrderFactory
    {
        private static readonly Dictionary<int, Func<IOrderRepository>> RepositoryMap = new Dictionary<int, Func<IOrderRepository>>
        {
            {0, () => new CmsNextOrderWorkflowRepository()},
            {1, () => new CmsNetOrderWorkflowRepository()},
            {2, () => new CmsNextOrderWorkflowRepository()},
            {3, () => new LegacyOrderWorkflowRepository()}
        };
        private static readonly Dictionary<OrderStatus, Func<Guid, OrderWorkflowDto, int, Order>>
            OrderStatusMap = new Dictionary<OrderStatus, Func<Guid, OrderWorkflowDto, int, Order>>
        {
            {OrderStatus.Unassigned, (id, dto, cmsId) => new UnassignedOrder(id,dto,RepositoryMap[cmsId]())},
            {OrderStatus.Assigned, (id, dto, cmsId) => new AssignedOrder(id,dto,RepositoryMap[cmsId]())},
            {OrderStatus.VendorAccepted, (id, dto, cmsId) => new VendorAcceptedOrder(id,dto,RepositoryMap[cmsId]())},
            {OrderStatus.Submitted, GetSubmittedOrder},
            {OrderStatus.Rejected, GetRejectedOrder},
            {OrderStatus.ManualAssign, (id, dto, cmsId) => new ManualAssignOrder(id,dto,RepositoryMap[cmsId]())},
            {OrderStatus.Closed, (id, dto, cmsId) => new ClosedOrder(id,dto,RepositoryMap[cmsId]())},
            {OrderStatus.ReviewSubmission, (id,dto,cmsId) => new ReviewSubmissionOrder(id,dto,RepositoryMap[cmsId]())},
            {OrderStatus.ReviewAcceptance, (id, dto, cmsId) => new ReviewAcceptanceOrder(id, dto,RepositoryMap[cmsId]())},
            {OrderStatus.ClientAccepted, (id, dto, cmsId) => new ClientAcceptedOrder(id, dto, RepositoryMap[cmsId]())}
        };

        public static Order GetWorkflowOrder(Guid orderId, OrderStatus orderStatus, OrderWorkflowDto orderWorkFlowDto)
        {
            var orderfunc = OrderStatusMap[orderStatus];
            var order = orderfunc(orderId, orderWorkFlowDto, orderWorkFlowDto.Cms.Id % 4);
            return order;
        }

        private static SubmittedOrder GetSubmittedOrder(Guid orderId, OrderWorkflowDto orderDto, int repoId)
        {
            var clientId = orderDto.Cms.Id;
            if (clientId == 32) return new DansCustomSubmittedOrder(orderId, orderDto, RepositoryMap[repoId]());
            if (clientId % 17 == 0 || clientId % 16 == 0 || clientId % 22 == 0) return new JohnsCustomSubmittedOrder(orderId, orderDto, RepositoryMap[repoId]());
            return new SubmittedOrder(orderId, orderDto, RepositoryMap[repoId]());
        }
        
        private static RejectedOrder GetRejectedOrder(Guid orderId, OrderWorkflowDto orderDto, int repoId)
        {
            return repoId %3 == 0 ? new CustomRejectedOrder(orderId, orderDto, RepositoryMap[repoId]()) : new RejectedOrder(orderId, orderDto, RepositoryMap[repoId]());
        }
    }
}
