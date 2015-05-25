﻿using System;
using OrderWorkflow.Domain.Contracts;

namespace OrderWorkflow.Domain.WorkflowOrders.DerivedOrders
{
    public class DansCustomSubmittedOrder : SubmittedOrder
    {
        public DansCustomSubmittedOrder(Guid id, OrderWorkflowDto orderWorkflowDto) : base(id, orderWorkflowDto)
        {

        }

        public override IWorkflowOrder MakeTransition()
        {
            const string output = "Doing Dan Additional Order Submitted Buisness Logic implementation details";
            ConsoleHelper.WriteWithStyle(ConsoleColor.Magenta, ConsoleColor.White, output);
            //ConsoleHelper.SetColors(ConsoleColor.Magenta, ConsoleColor.White);
            //Console.WriteLine("Doing Dan Additional Order Submitted Buisness Logic implementation details");
            //ConsoleHelper.ResetColors();
            return base.MakeTransition();
        }
    }
}