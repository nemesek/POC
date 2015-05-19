using System;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.Common;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class ManualAssignOrder : Order
    {
        private readonly Func<Guid, Func<OrderWorkflowDto>, bool, IWorkflowOrder> _transitionFunc;

        public ManualAssignOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {
            _transitionFunc = orderWorkflowDto.StateTransitionFunc;
        }
        
        public override OrderStatus Status
        {
            get { return OrderStatus.ManualAssign; }
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
