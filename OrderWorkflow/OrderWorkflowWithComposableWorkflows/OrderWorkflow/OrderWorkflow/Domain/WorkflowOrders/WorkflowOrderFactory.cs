using System;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class WorkflowOrderFactory
    {
        public static Order GetWorkflowOrder(int clientId, Guid orderId, OrderStatus orderStatus, OrderWorkflowDto orderWorkFlowDto)
        {
            switch (orderStatus)
            {
                case OrderStatus.Unassigned: return GetUnassignedOrder(orderId, orderWorkFlowDto);
                case OrderStatus.Assigned: return GetAssignedOrder(orderId, orderWorkFlowDto);
                case OrderStatus.VendorAccepted: return GetVendorAcceptedOrder(orderId, orderWorkFlowDto);
                case OrderStatus.Submitted: 
                {
                    return clientId %17 == 0 || clientId % 16 == 0 || clientId % 22 == 0 ? GetJohnSubmittedOrder(orderId, orderWorkFlowDto) :GetSubmittedOrder(orderId, orderWorkFlowDto);
                }
                case OrderStatus.Rejected:
                {
                    return clientId%3 == 0 ? GetCustomRejectedOrder(orderId, orderWorkFlowDto) : GetRejectedOrder(orderId, orderWorkFlowDto);
                }
                case OrderStatus.ManualAssign: return GetManualAssignOrder(orderId, orderWorkFlowDto);
                case OrderStatus.Closed: return GetClosedOrder(orderId, orderWorkFlowDto);
                case OrderStatus.ReviewSubmission: return GetReviewSubmissionOrder(orderId, orderWorkFlowDto);
                case OrderStatus.ReviewAcceptance: return GetReviewAcceptanceOrder(orderId, orderWorkFlowDto);
                case OrderStatus.ClientAccepted: return GetClientAcceptedOrder(orderId, orderWorkFlowDto);

                default: throw new ArgumentOutOfRangeException("orderStatus");
            }
        }

        private static AssignedOrder GetAssignedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new AssignedOrder(orderId, orderDto);
        }

        private static VendorAcceptedOrder GetVendorAcceptedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new VendorAcceptedOrder(orderId, orderDto);
        }

        private static ClientAcceptedOrder GetClientAcceptedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new ClientAcceptedOrder(orderId, orderDto);
        }

        private static SubmittedOrder GetSubmittedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new SubmittedOrder(orderId, orderDto);
        }
        private static SubmittedOrder GetJohnSubmittedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new JohnsCustomSubmittedOrder(orderId, orderDto);
        }

        private static RejectedOrder GetRejectedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new RejectedOrder(orderId, orderDto);
        }

        private static CustomRejectedOrder GetCustomRejectedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new CustomRejectedOrder(orderId, orderDto);
        }

        private static ClosedOrder GetClosedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new ClosedOrder(orderId, orderDto);
        }

        private static ManualAssignOrder GetManualAssignOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new ManualAssignOrder(orderId, orderDto);
        }

        private static ReviewSubmissionOrder GetReviewSubmissionOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new ReviewSubmissionOrder(orderId, orderDto);
        }

        private static ReviewAcceptanceOrder GetReviewAcceptanceOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new ReviewAcceptanceOrder(orderId, orderDto);
        }

        private static UnassignedOrder GetUnassignedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new UnassignedOrder(orderId, orderDto);
        }
        
    }
}
