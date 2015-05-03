﻿using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Orders;

namespace OrderWorkflow.Domain
{
    public class OrderTransitioner
    {
       public IWorkflowOrder CreateNewOrder(Func<IOrderWithZipCode,Vendor> assignFunc, int clientId)
        {
            var dto = new OrderDto
            {
                AssignFunc = assignFunc,
                ConditionalTransitionFunc = GetNewOrderTransitionFunc(),
                ZipCode = "38655",
                ClientId = clientId
            };
            return new Unassigned(Guid.NewGuid(), dto);
        }

        private IWorkflowOrder TransitionToAssigned(Guid orderId, Func<OrderDto> orderDtoFunc)
        {
            Func<Guid, Func<OrderDto>, bool, IWorkflowOrder>
                transitionFunc = (i, o, t) => t ? TransitionToAccepted(i,o) : TransitionOrderBackToNew(i, o);
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = transitionFunc;
            return new AssignedOrder(orderId, orderDto);
        }

        private IWorkflowOrder TransitionToAccepted(Guid orderId, Func<OrderDto> orderDtoFunc)
        {
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = TransitionToSubmitted;
            return new AcceptedOrder(orderId,orderDto);
        }

        private IWorkflowOrder TransitionToSubmitted(Guid orderId, Func<OrderDto> orderDtoFunc, bool shouldTransition)
        {
            Func<Guid, Func<OrderDto>, bool, IWorkflowOrder>
                transitionFunc = (i, o, t) => t ? TransitionToClosed(i, o, true) : TransitionToRejected(i, o, true);
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = transitionFunc;
            return new SubmittedOrder(orderId, orderDto);
        }

        private IWorkflowOrder TransitionToRejected(Guid orderId, Func<OrderDto> orderDtoFunc, bool shouldTransition)
        {
            Func<Guid, Func<OrderDto>, bool, IWorkflowOrder> 
                transitionFunc = (i, o, t) => t ? TransitionToClosed(i, o, shouldTransition) : TransitionToRejected(i, o, shouldTransition);
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = transitionFunc;
            return new RejectedOrder(orderId, orderDto);
        }

        private IWorkflowOrder TransitionToClosed(Guid orderId, Func<OrderDto> orderDtoFunc, bool shouldTransition)
        {
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = TransitionToClosed;
            return new ClosedOrder(orderId, orderDto);
        }

        private IWorkflowOrder TransitionToWithClient(Guid orderId, Func<OrderDto> orderDtoFunc, bool shouldTransition)
        {
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = TransitionToWithClient;
            return new WithClientOrder(orderId, orderDto);
        }

        private IWorkflowOrder TransitionOrderBackToNew(Guid orderId, Func<OrderDto> orderDtoFunc)
        {
            var orderDto = orderDtoFunc();
            var client = new Client(orderDto.ClientId, this);
            var assignmentFunc = client.ManualAssign();
            orderDto.AssignFunc = assignmentFunc;
            orderDto.ConditionalTransitionFunc = GetNewOrderTransitionFunc();
            return new Unassigned(orderId, orderDto);

        }

        private Func<Guid, Func<OrderDto>, bool, IWorkflowOrder> GetNewOrderTransitionFunc()
        {
            return (i, o, t) => t ? TransitionToAssigned(i, o) : TransitionToWithClient(i,o,true);
        }

    }
}
