﻿using System;

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
                case OrderStatus.Accepted: return GetAcceptedOrder(orderId, orderWorkFlowDto);
                case OrderStatus.Submitted: 
                {
                    return clientId %17 == 0 || clientId % 16 == 0 || clientId % 22 == 0 ? GetJohnSubmittedOrder(orderId, orderWorkFlowDto) :GetSubmittedOrder(orderId, orderWorkFlowDto);
                }
                case OrderStatus.Rejected:
                {
                    return clientId%3 == 0 ? GetCustomRejectedOrder(orderId, orderWorkFlowDto) : GetRejectedOrder(orderId, orderWorkFlowDto);
                }
                case OrderStatus.WithClient: return GetWithClientOrder(orderId, orderWorkFlowDto);
                case OrderStatus.Closed:return GetClosedOrder(orderId, orderWorkFlowDto);
                default: throw new ArgumentOutOfRangeException("orderStatus");
            }
        }

        private static AssignedOrder GetAssignedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new AssignedOrder(orderId, orderDto);
        }

        private static AcceptedOrder GetAcceptedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new AcceptedOrder(orderId, orderDto);
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

        private static WithClientOrder GetWithClientOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new WithClientOrder(orderId, orderDto);
        }

        private static UnassignedOrder GetUnassignedOrder(Guid orderId, OrderWorkflowDto orderDto)
        {
            return new UnassignedOrder(orderId, orderDto);
        }
        
    }
}