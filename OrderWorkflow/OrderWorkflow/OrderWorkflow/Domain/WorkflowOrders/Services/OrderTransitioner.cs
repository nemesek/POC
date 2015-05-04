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
                StateTransitionFunc = GetUnassignedOrderTransitionFunc(),
                ZipCode = "38655",
                ClientId = clientId
            };
           return WorkflowOrderFactory.GetWorkflowOrder(clientId, Guid.NewGuid(), OrderStatus.Unassigned, dto);
        }

        protected virtual IWorkflowOrder TransitionToAssigned(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc)
        {
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder>
                transitionFunc = (id, dtoFunc, t) => t ? TransitionToAccepted(id,dtoFunc) : TransitionOrderBackToNew(id, dtoFunc);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Assigned, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToAccepted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToSubmitted;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Accepted, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToSubmitted(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool moveForward)
        {
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder>
                transitionFunc = (id, dtoFunc, t) => t ? TransitionToClosed(id, dtoFunc, moveForward) : TransitionToRejected(id, dtoFunc, moveForward);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Submitted, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToRejected(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool moveForward)
        {
            Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> 
                transitionFunc = (id, dtoFunc, t) => t ? TransitionToClosed(id, dtoFunc, moveForward) : TransitionToRejected(id, dtoFunc, moveForward);
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = transitionFunc;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Rejected, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToClosed(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToClosed;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Closed, orderDto);
        }

        protected virtual IWorkflowOrder TransitionToWithClient(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc, bool shouldMoveForward)
        {
            var orderDto = orderDtoFunc();
            orderDto.StateTransitionFunc = TransitionToWithClient;
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.WithClient, orderDto);
        }

        protected virtual IWorkflowOrder TransitionOrderBackToNew(Guid orderId, Func<OrderWorkflowDto> orderDtoFunc)
        {
            var orderDto = orderDtoFunc();
            var client = new Cms(orderDto.ClientId, this);
            var assignmentFunc = client.ManualAssign();
            orderDto.AssignVendorFunc = assignmentFunc;
            orderDto.StateTransitionFunc = GetUnassignedOrderTransitionFunc();
            return WorkflowOrderFactory.GetWorkflowOrder(orderDto.ClientId, orderId, OrderStatus.Unassigned, orderDto);

        }

        protected virtual Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> GetUnassignedOrderTransitionFunc()
        {
            return (id, dtoFunc, t) => t ? TransitionToAssigned(id, dtoFunc) : TransitionToWithClient(id,dtoFunc,true);
        }

    }
}
