using System;
using System.Collections.Generic;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.OrderWorkflowContext.Contracts;
using DnxConsole.Domain.OrderWorkflowContext.DerivedOrders;
using DnxConsole.Domain.OrderWorkflowContext.OrderStates;

namespace DnxConsole.Domain.OrderWorkflowContext.Services
{
    public class WorkflowOrderFactory
    {
        private readonly IOrderRepository _repository;

        private static readonly Dictionary<OrderStatus, Func<Guid, OrderWorkflowDto, IOrderRepository , Order>>
            OrderStatusMap = new Dictionary<OrderStatus, Func<Guid, OrderWorkflowDto, IOrderRepository, Order>>
        {
            {OrderStatus.Unassigned, (id, dto, repo) => new UnassignedOrder(id, dto, repo)},
            {OrderStatus.Assigned, (id, dto, repo) => new AssignedOrder(id, dto, repo)},
            {OrderStatus.VendorAccepted, (id, dto, repo) => new VendorAcceptedOrder(id, dto, repo)},
            {OrderStatus.Submitted, GetSubmittedOrder},
            {OrderStatus.Rejected, GetRejectedOrder},
            {OrderStatus.ManualAssign, (id, dto, repo) => new ManualAssignOrder(id, dto, repo)},
            {OrderStatus.Closed, (id, dto, repo) => new ClosedOrder(id, dto, repo)},
            {OrderStatus.ReviewSubmission, (id,dto, repo) => new ReviewSubmissionOrder(id, dto, repo)},
            {OrderStatus.ReviewAcceptance, (id, dto, repo) => new ReviewAcceptanceOrder(id, dto, repo)},
            {OrderStatus.ClientAccepted, (id, dto, repo) => new ClientAcceptedOrder(id, dto, repo)}
        };



        public WorkflowOrderFactory(IOrderRepository repository)
        {
            _repository = repository;
        }

        public Order GetWorkflowOrder(Guid orderId, OrderStatus orderStatus, OrderWorkflowDto orderWorkFlowDto)
        {
            var orderfunc = OrderStatusMap[orderStatus];
            var order = orderfunc(orderId, orderWorkFlowDto, _repository);
            return order;
        }

        private static SubmittedOrder GetSubmittedOrder(Guid orderId, OrderWorkflowDto orderDto, IOrderRepository repo)
        {
            var clientId = orderDto.Cms.Id;
            if (clientId == 32) return new DansCustomSubmittedOrder(orderId, orderDto,repo);
            if (clientId % 17 == 0 || clientId % 16 == 0 || clientId % 22 == 0) return new JohnsCustomSubmittedOrder(orderId, orderDto, repo);
            return new SubmittedOrder(orderId, orderDto, repo);
        }
        
        private static RejectedOrder GetRejectedOrder(Guid orderId, OrderWorkflowDto orderDto, IOrderRepository repo)
        {
            return orderDto.Cms.Id %3 == 0 ? new CustomRejectedOrder(orderId, orderDto, repo) : new RejectedOrder(orderId, orderDto,repo);
        }
    }
}
