﻿using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders
{
    public class DansCustomSubmittedOrder : SubmittedOrder
    {
        public DansCustomSubmittedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {

        }

        public override IWorkflowOrder MakeTransition()
        {
            Console.WriteLine("Doing Dan Additional Order Submitted Buisness Logic");
            return base.MakeTransition();
        }
    }
}