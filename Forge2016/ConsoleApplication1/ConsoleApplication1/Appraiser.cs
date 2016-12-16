using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    public class Appraiser
    {
        private readonly int _userId;
        private readonly PatternOfWork _patternOfWork;
        private readonly Dictionary<DayOfWeek, double> _dayMap;

        public Appraiser(int userId, PatternOfWork patternOfWork)
        {
            _userId = userId;
            _patternOfWork = patternOfWork;
            _dayMap = new Dictionary<DayOfWeek, double>
            {
                {DayOfWeek.Sunday, _patternOfWork.SundayPercentage},
                {DayOfWeek.Monday, _patternOfWork.MondayPercentage },
                {DayOfWeek.Tuesday, _patternOfWork.TuesdayPercentage },
                {DayOfWeek.Wednesday, _patternOfWork.WednesdayPercentage },
                {DayOfWeek.Thursday, _patternOfWork.ThursdayPercentage },
                {DayOfWeek.Friday, _patternOfWork.FridayPercentage },
                {DayOfWeek.Saturday, _patternOfWork.SaturdayPercentage }
            };
        }

        public int UserId => _userId;
        public IEnumerable<Order> LoadOrdersIHaveDoneInPast()
        {
            return Repository.GetOrdersInspectedWitinLastNumberOfDays(30).Where(o => o.UserId == _userId);
        }

        public IEnumerable<Order> LoadOrdersAssignedToMeRightNow()
        {
            return Repository.GetOpenOrders().Where(o => o.UserId == _userId);
        }

        public double GetWorkStyleForDay(DayOfWeek dayOfWeek)
        {
            return _dayMap[dayOfWeek];
        }

        public double ChangeInspectionDatePercentage => _patternOfWork.ChangeInspectionDatePercentage;

    }
}
