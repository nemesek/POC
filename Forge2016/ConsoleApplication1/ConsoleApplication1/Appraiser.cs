using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class Appraiser
    {
        private readonly int _userId;

        public Appraiser(int userId)
        {
            _userId = userId;
        }
        public IEnumerable<Order> LoadOrdersIHaveDoneInPast()
        {
            return Repository.GetOrdersInspectedWitinLastNumberOfDays(30).Where(o => o.UserId == _userId);
        }

        public IEnumerable<Order> LoadOrdersAssignedToMeRightNow()
        {
            return Repository.GetOpenOrders().Where(o => o.UserId == _userId);
        }
    }
}
