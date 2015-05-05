using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class OnHoldOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public OnHoldOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }
        
        public override OrderStatus Status
        {
            get { return OrderStatus.OnHold; }
        }

        public override IWorkflowOrder MakeTransition()
        {
            var cms = new Cms(base.ClientId);
            var assigned = cms.ManualAssign(this);
            if (!assigned) Console.WriteLine("^^^^^^^^Reassignment was not successful^^^^^^^^^^");
            return _transitionFunc(base.OrderId, base.MapToOrderWorkflowDto(), assigned);
        }
    }
}
