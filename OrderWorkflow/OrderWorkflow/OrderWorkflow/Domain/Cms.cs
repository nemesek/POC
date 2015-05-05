﻿using System;
using OrderWorkflow.Domain.AutoAssign;
using OrderWorkflow.Domain.Contracts;
using OrderWorkflow.Domain.WorkflowOrders.Services;

namespace OrderWorkflow.Domain
{
    public class Cms
    {
        private readonly int _id;
        private readonly OrderTransitioner _orderTransitioner;

        public Cms(int id): this(id, OrderTransitionerFactory.GetTransitionLogic(id)) {}

        public Cms(int id, OrderTransitioner orderTransitioner)
        {
            _id = id;
            _orderTransitioner = orderTransitioner;
        }


        public IWorkflowOrder CreateNewOrder()
        {
            return _orderTransitioner.CreateNewOrder(FindBestVendor(), _id);
        }

        public Func<ICanBeAutoAssigned, Vendor> FindBestVendor()
        {
            var autoAssign = AutoAssignFactory.CreateAutoAssign(_id);
            return autoAssign.FindBestVendor;
        }

        public Func<ICanBeAutoAssigned, Vendor> ManualAssign()
        {
            return _ => null;
        }

        public bool ManualAssign(IWorkflowOrder order)
        {
            // randomly return false to simulate rejection
            if (!Randomizer.RandomYes()) return false;
            var vendor = new Vendor(0, "38655", "Daniel Garrett");
            order.AssignVendor(vendor);
            return true;
        }

        public bool ReviewAcceptance(IWorkflowOrder order)
        {
            return Randomizer.RandomYes();
        }
    }
}
