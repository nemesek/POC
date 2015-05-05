using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.Services
{
    public class OrderTransitioner
    {
       public virtual IWorkflowOrder CreateNewOrder(Func<ICanBeAutoAssigned,Vendor> assignFunc, int clientId)
        {
            var dto = new OrderWorkflowDto
            {
                AssignVendorFunc = assignFunc,
                StateTransitionFunc = BuildTransitionFunc(TransitionToAssigned, TransitionToOnHold),
                ZipCode = "38655",
                ClientId = clientId
            };
           return WorkflowOrderFactory.GetWorkflowOrder(clientId, Guid.NewGuid(), OrderStatus.Unassigned, dto);
        }

        protected virtual IWorkflowOrder TransitionToAssigned(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var transitionFunc = BuildTransitionFunc(TransitionToVendorAccepted, TransitionOrderBackToUnassigned);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Assigned, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToVendorAccepted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToReviewAcceptance;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.VendorAccepted, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToReviewAcceptance(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var transitionFunc = BuildTransitionFunc(TransitionToClientAccepted, TransitionOrderBackToUnassigned);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.ReviewAcceptance, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToClientAccepted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToSubmitted;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.ClientAccepted, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToSubmitted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToReviewSubmission;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Submitted, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToReviewSubmission(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var transitionFunc = BuildTransitionFunc(TransitionToClosed, TransitionToRejected);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.ReviewSubmission, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToRejected(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToReviewSubmission;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Rejected, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToClosed(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToClosed;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Closed, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToOnHold(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var transitionFunc = BuildTransitionFunc(TransitionToAssigned, TransitionToOnHold);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.OnHold, orderDto);
        }

        protected virtual IWorkflowOrder TransitionOrderBackToUnassigned(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            var client = new Cms(orderDto.ClientId, this);
            var assignmentFunc = client.ManualAssign();
            orderDto.AssignVendorFunc = assignmentFunc;
            orderDto.StateTransitionFunc = BuildTransitionFunc(TransitionToAssigned, TransitionToOnHold); 
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Unassigned, orderDto);
        }

        private Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> BuildTransitionFunc(
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> forwardExpression,
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> backwardExpression)
        {
            return (id, dtoFunc, t) => t ? forwardExpression(id, dtoFunc,true) : backwardExpression(id, dtoFunc,false);
        }

    }
}
