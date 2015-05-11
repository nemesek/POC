using System;
using System.Collections.Generic;
using OrderWorkflow.Domain.WorkflowOrders.DerivedOrders;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class WorkflowOrderFactory
    {
        private static readonly Dictionary<OrderStatus, Func<int,Guid, OrderWorkflowDto, Order>> FuncDictionary = new Dictionary
            <OrderStatus, Func<int,Guid, OrderWorkflowDto,Order>>
        {
            {OrderStatus.Unassigned, (_, id, dto) => new UnassignedOrder(id,dto) },
            {OrderStatus.Assigned, (_,id,dto) => new AssignedOrder(id,dto)},
            {OrderStatus.VendorAccepted, (_, id, dto) => new VendorAcceptedOrder(id,dto)},
            {OrderStatus.Submitted, GetSubmittedOrder},
            {OrderStatus.Rejected, GetRejectedOrder },
            {OrderStatus.ManualAssign, (_, id, dto) => new ManualAssignOrder(id,dto)},
            {OrderStatus.Closed, (_, id, dto) => new ClosedOrder(id, dto)},
            {OrderStatus.ReviewSubmission, (_,id, dto) => new ReviewSubmissionOrder(id,dto)},
            {OrderStatus.ReviewAcceptance, (_, id, dto) => new ReviewAcceptanceOrder(id, dto)},
            {OrderStatus.ClientAccepted, (_, id, dto) => new ClientAcceptedOrder(id, dto)}
        };

        public static Order GetWorkflowOrder(int clientId, Guid orderId, OrderStatus orderStatus, OrderWorkflowDto orderWorkFlowDto)
        {
            var orderfunc = FuncDictionary[orderStatus];
            return orderfunc(clientId, orderId, orderWorkFlowDto);
        }

        private static SubmittedOrder GetSubmittedOrder(int clientId, Guid orderId, OrderWorkflowDto orderDto)
        {
            // todo: uncomment for demo if (clientId == 32) return new DansCustomSubmittedOrder(orderId, orderDto);
            if (clientId % 17 == 0 || clientId % 16 == 0 || clientId % 22 == 0) return new JohnsCustomSubmittedOrder(orderId, orderDto);
            return new SubmittedOrder(orderId, orderDto);
        }
        
        private static RejectedOrder GetRejectedOrder(int clientId, Guid orderId, OrderWorkflowDto orderDto)
        {
            return clientId%3 == 0 ? new CustomRejectedOrder(orderId, orderDto) : new RejectedOrder(orderId, orderDto);
        }
    }
}
