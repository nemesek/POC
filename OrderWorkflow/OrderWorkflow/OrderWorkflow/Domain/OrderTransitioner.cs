using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Orders;

namespace OrderWorkflow.Domain
{
    public class OrderTransitioner
    {
       public IOrder CreateNewOrder(Func<IOrderWithZipCode,Vendor> assignFunc, int clientId)
        {
            var dto = new OrderDto
            {
                AssignFunc = assignFunc,
                ConditionalTransitionFunc = GetNewOrderTransitionFunc(),
                ZipCode = "38655",
                ClientId = clientId
            };
            return new NewOrder(Guid.NewGuid(), dto);
        }

        private IOrder TransitionToAssigned(Guid orderId, Func<OrderDto> orderDtoFunc)
        {
            Func<Guid, Func<OrderDto>, bool, IOrder> transitionFunc = (i, o, t) => t ? TransitionToAccepted(i,o) : TransitionOrderBackToNew(i, o);
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = transitionFunc;
            return new AssignedOrder(orderId, orderDto);
        }

        private IOrder TransitionToAccepted(Guid orderId, Func<OrderDto> orderDtoFunc)
        {
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = TransitionToClose;
            return new AcceptedOrder(orderId,orderDto);
        }

        private IOrder TransitionToClose(Guid orderId, Func<OrderDto> orderDtoFunc, bool shouldTransition)
        {
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = TransitionToClose;
            return new ClosedOrder(orderId, orderDto);
        }

        private IOrder TransitionToWithClient(Guid orderId, Func<OrderDto> orderDtoFunc, bool shouldTransition)
        {
            var orderDto = orderDtoFunc();
            orderDto.ConditionalTransitionFunc = TransitionToWithClient;
            return new WithClientOrder(orderId, orderDto);
        }

        private IOrder TransitionOrderBackToNew(Guid orderId, Func<OrderDto> orderDtoFunc)
        {
            var orderDto = orderDtoFunc();
            var client = new Client(orderDto.ClientId, this);
            var assignmentFunc = client.ManualAssign();
            orderDto.AssignFunc = assignmentFunc;
            orderDto.ConditionalTransitionFunc = GetNewOrderTransitionFunc();
            return new NewOrder(orderId, orderDto);

        }

        private Func<Guid, Func<OrderDto>, bool, IOrder> GetNewOrderTransitionFunc()
        {
            return (i, o, t) => t ? TransitionToAssigned(i, o) : TransitionToWithClient(i,o,true);
        }

    }
}
