﻿using System;
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
                StateTransitionFunc = GetNewOrderTransitionFunc(),
                ZipCode = "38655",
                ClientId = clientId
            };
            return new UnassignedOrder(Guid.NewGuid(), dto);
        }

        protected virtual IWorkflowOrder TransitionToAssigned(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc)
        {
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder>
                transitionFunc = (id, dtoFunc, t) => t ? TransitionToAccepted(id,dtoFunc) : TransitionOrderBackToNew(id, dtoFunc);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return new AssignedOrder(orderId, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToAccepted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToSubmitted;
            return new AcceptedOrder(orderId,orderDto);
        }

        protected virtual IWorkflowOrder TransitionToSubmitted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool moveForward)
        {
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder>
                transitionFunc = (id, dtoFunc, t) => t ? TransitionToClosed(id, dtoFunc, moveForward) : TransitionToRejected(id, dtoFunc, moveForward);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return new SubmittedOrder(orderId, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToRejected(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool moveForward)
        {
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> 
                transitionFunc = (id, dtoFunc, t) => t ? TransitionToClosed(id, dtoFunc, moveForward) : TransitionToRejected(id, dtoFunc, moveForward);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return new RejectedOrder(orderId, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToClosed(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToClosed;
            return new ClosedOrder(orderId, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToWithClient(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToWithClient;
            return new WithClientOrder(orderId, orderDto);
        }

        protected virtual IWorkflowOrder TransitionOrderBackToNew(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc)
        {
            var orderDto = orderDtoFunc();
            var client = new Cms(orderDto.ClientId, this);
            var assignmentFunc = client.ManualAssign();
            orderDto.AssignVendorFunc = assignmentFunc;
            orderDto.StateTransitionFunc = GetNewOrderTransitionFunc();
            return new UnassignedOrder(orderId, orderDto);

        }

        protected virtual Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> GetNewOrderTransitionFunc()
        {
            return (id, dtoFunc, t) => t ? TransitionToAssigned(id, dtoFunc) : TransitionToWithClient(id,dtoFunc,true);
        }

    }
}
