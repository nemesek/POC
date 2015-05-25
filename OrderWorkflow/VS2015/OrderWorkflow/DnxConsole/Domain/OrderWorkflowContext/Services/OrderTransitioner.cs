﻿using System;
using DnxConsole.Domain.Common;
using DnxConsole.Domain.Contracts;

namespace DnxConsole.Domain.OrderWorkflowContext.Services
{
    public class OrderTransitioner
    {
        private readonly Func<ICanBeAutoAssigned, Vendor> _safeAssign;

        public OrderTransitioner(Func<ICanBeAutoAssigned, Vendor> safeAssign)
        {
            _safeAssign = safeAssign;
        }

        // using the strategy pattern here to inject in client specific auto assign logic
        // http://www.dofactory.com/net/strategy-design-pattern
        public virtual IWorkflowOrder GetUnassignedOrder(Func<ICanBeAutoAssigned, Vendor> defaultAssignFunc, Cms cms)
        {
            var dto = new OrderWorkflowDto
            {
                AssignVendorFunc = defaultAssignFunc,
                StateTransitionFunc = BuildTransitionFunc(TransitionToAssigned, TransitionToManualAssign),
                ZipCode = "38655",
                Cms = cms
            };
            return WorkflowOrderFactory.GetWorkflowOrder(Guid.NewGuid(), OrderStatus.Unassigned, dto);
        }

        protected virtual IWorkflowOrder TransitionToAssigned(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var transitionFunc = BuildTransitionFunc(TransitionToVendorAccepted, TransitionOrderBackToUnassigned);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.Assigned, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToVendorAccepted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToReviewAcceptance;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.VendorAccepted, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToReviewAcceptance(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var transitionFunc = BuildTransitionFunc(TransitionToClientAccepted, TransitionOrderBackToUnassigned);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.ReviewAcceptance, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToClientAccepted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToSubmitted;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.ClientAccepted, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToSubmitted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToReviewSubmission;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.Submitted, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToReviewSubmission(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var transitionFunc = BuildTransitionFunc(TransitionToClosed, TransitionToRejected);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.ReviewSubmission, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToRejected(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToReviewSubmission;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.Rejected, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToClosed(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToClosed;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.Closed, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToManualAssign(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var transitionFunc = BuildTransitionFunc(TransitionToAssigned, TransitionToManualAssign);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.ManualAssign, orderDto);
        }

        protected virtual IWorkflowOrder TransitionOrderBackToUnassigned(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.AssignVendorFunc = _safeAssign;
            orderDto.StateTransitionFunc = BuildTransitionFunc(TransitionToAssigned, TransitionToManualAssign);
            return WorkflowOrderFactory.GetWorkflowOrder(orderId, OrderStatus.Unassigned, orderDto);
        }

        private static Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> BuildTransitionFunc(
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> forwardExpression,
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> backwardExpression)
        {
            return (id, dtoFunc, t) => t ? forwardExpression(id, dtoFunc, true) : backwardExpression(id, dtoFunc, false);
        }

    }
}