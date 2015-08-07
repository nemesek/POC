using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter
{
    public class Vendor
    {
        private readonly string _id;
        private readonly IEnumerable<string> _coveredZips;
        private readonly IEnumerable<int> _services;
        private readonly bool _isDeleted;
        private readonly string _status;
        private readonly bool _isApUser;
        private readonly bool _isInactive;
        private readonly bool _isBanned;
        private readonly DateTime _lastAssignedUtc;
        private readonly int _capacity;
        private readonly int _currentAcceptedOrderCount;
        private readonly IEnumerable<int> _loanIds;
        private readonly decimal _rank;
        private readonly int _companyId;

        public Vendor()
        {

        }
        public string Id => _id;
        public string Status => _status;
        public bool IsDeleted => _isDeleted;
        public bool IsApUser => _isApUser;
        public bool IsInactive => _isInactive;
        public bool IsBanned => _isBanned;
        public DateTime LastAssignedDateUtc => _lastAssignedUtc;
        public decimal Rank => _rank;
        public int CompanyId => _companyId;

        public IEnumerable<int> LoadServicesIPerform()
        {
            return _services;
        }

        public IEnumerable<string> LoadZipsICover()
        {
            return _coveredZips;
        }

        public bool IsAvailableForSchedule(DateTime utcDate)
        {
            if (utcDate.Kind != DateTimeKind.Utc) throw new ArgumentException($"{nameof(utcDate)} does not have utc kind.");
            return true;
        }

        public int HowCloseToCapacity()
        {
            return _capacity - _currentAcceptedOrderCount;
        }

        public bool AtCapacity()
        {
            return _capacity <= _currentAcceptedOrderCount;
        }

        public bool HasPerformedWorkOnLoan(int loanId)
        {
            return _loanIds.Any(l => l == loanId);
        }
    }
}
