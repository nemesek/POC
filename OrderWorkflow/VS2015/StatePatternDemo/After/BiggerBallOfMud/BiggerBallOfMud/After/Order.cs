using System;
using System.Diagnostics.CodeAnalysis;

namespace BiggerBallOfMud.After
{
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    [SuppressMessage("ReSharper", "ConvertToAutoPropertyWithPrivateSetter")]
    public abstract class Order
    {
        private readonly int _cmsId;
        private readonly string _zipCode;
        private Vendor _vendor;

        protected Order(int cmsId, string zipCode, Vendor vendor)
        {
            _cmsId = cmsId;
            _zipCode = zipCode;
            _vendor = vendor;
        }


        public abstract OrderStatus Status { get; }
        protected int CmsId => _cmsId;
        protected string ZipCode => _zipCode;
        protected Vendor AssignedVendor => _vendor;

        public abstract Order ProcessNextStep();

        protected void AssignVendor(Vendor vendor)
        {
            if (this.Status != OrderStatus.Unassigned && this.Status != OrderStatus.ManualAssign)
            {
                throw new Exception("Order is not in correct state to be Assigned.");
            }

            // some additional business logic if required
            _vendor = vendor;
        }
    }
}
