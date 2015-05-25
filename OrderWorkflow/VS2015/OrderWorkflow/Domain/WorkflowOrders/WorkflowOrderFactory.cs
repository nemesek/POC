using System;
using System.Collections.Generic;
using System.Linq;
using OrderWorkflow.Domain.WorkflowOrders.DerivedOrders;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class WorkflowOrderFactory
    {
        private static readonly Dictionary<OrderStatus, Func<Guid, OrderWorkflowDto, Order>> 
            FuncDictionary = new Dictionary<OrderStatus, Func<Guid, OrderWorkflowDto,Order>>
        {
            {OrderStatus.Unassigned, (id, dto) => new UnassignedOrder(id,dto)},
            {OrderStatus.Assigned, (id, dto) => new AssignedOrder(id,dto)},
            {OrderStatus.VendorAccepted, (id, dto) => new VendorAcceptedOrder(id,dto)},
            {OrderStatus.Submitted, GetSubmittedOrder},
            {OrderStatus.Rejected, GetRejectedOrder },
            {OrderStatus.ManualAssign, (id, dto) => new ManualAssignOrder(id,dto)},
            {OrderStatus.Closed, (id, dto) => new ClosedOrder(id, dto)},
            {OrderStatus.ReviewSubmission, (id,dto) => new ReviewSubmissionOrder(id,dto)},
            {OrderStatus.ReviewAcceptance, (id, dto) => new ReviewAcceptanceOrder(id, dto)},
            {OrderStatus.ClientAccepted, (id, dto) => new ClientAcceptedOrder(id, dto)}
        };

        public static Order GetWorkflowOrder(Guid orderId, OrderStatus orderStatus, OrderWorkflowDto orderWorkFlowDto)
        {
            var orderfunc = FuncDictionary[orderStatus];
            var order = orderfunc(orderId, orderWorkFlowDto);
            return order;
        }

        private static SubmittedOrder GetSubmittedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            var clientId = orderDto.Cms.Id;
            if (clientId == 32) return new DansCustomSubmittedOrder(orderId, orderDto);
            if (clientId % 17 == 0 || clientId % 16 == 0 || clientId % 22 == 0) return new JohnsCustomSubmittedOrder(orderId, orderDto);
            return new SubmittedOrder(orderId, orderDto);
        }
        
        private static RejectedOrder GetRejectedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return orderDto.Cms.Id %3 == 0 ? new CustomRejectedOrder(orderId, orderDto) : new RejectedOrder(orderId, orderDto);
        }

    }
}
