using System;

namespace ConsoleApplication1
{
    public class Order
    {
        private readonly int _orderId;
        private readonly DateTime _inspectionDate;
        private readonly int _zipCode;
        private readonly int _userId;
        private readonly int _yearBuilt;
        private readonly int _squareFeet;
        public Order(int orderId, DateTime inspectionDate, int zipCode, int userId, int yearBuilt, int squareFeet)
        {
            _orderId = orderId;
            _inspectionDate = inspectionDate;
            _zipCode = zipCode;
            _userId = userId;
            _yearBuilt = yearBuilt;
            _squareFeet = squareFeet;
        }

        public int OrderId => _orderId;
        public DateTime InspectionDate => _inspectionDate;
        public int ZipCode => _zipCode;
        public int UserId => _userId;
        public int YearBuilt => _yearBuilt;
        public int SquareFeet => _squareFeet;

    }
}
