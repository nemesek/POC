using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Orders;

namespace OrderWorkflow.Domain
{
    public class OrderStateTransistor
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

        private IOrder GetAssignedOrder(Guid orderId, OrderDto orderDto)
        {
            Func<Guid, OrderDto, bool, IOrder> transitionFunc = (i, o, t) => t ? GetAcceptedOrder(i,o) : TransitionOrderBackToNew(i, o);
            orderDto.ConditionalTransitionFunc = transitionFunc;
            return new AssignedOrder(orderId, orderDto);
        }

        private IOrder GetAcceptedOrder(Guid orderId, OrderDto orderDto)
        {
            orderDto.TransitionFunc = GetClosedOrder;
            return new AcceptedOrder(orderId,orderDto);
        }

        private IOrder GetClosedOrder(Guid orderId,OrderDto orderDto)
        {
            return new ClosedOrder(orderId, orderDto);
        }

        private IOrder GetWithClientOrder(Guid orderId, OrderDto orderDto)
        {
            return new WithClientOrder(orderId, orderDto);
        }

        private IOrder TransitionOrderBackToNew(Guid orderId,OrderDto orderDto)
        {
            var client = new Client(orderDto.ClientId, this);
            var assignmentFunc = client.ManualAssign();
            orderDto.AssignFunc = assignmentFunc;
            orderDto.ConditionalTransitionFunc = GetNewOrderTransitionFunc();
            return new NewOrder(orderId, orderDto);

        }

        private Func<Guid, OrderDto, bool, IOrder> GetNewOrderTransitionFunc()
        {
            return (i, o, t) => t ? GetAssignedOrder(i, o) : GetWithClientOrder(i, o);
        }

    }
}
