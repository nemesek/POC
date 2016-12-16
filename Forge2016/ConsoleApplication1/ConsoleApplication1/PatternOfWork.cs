using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class PatternOfWork
    {
        public PatternOfWork(IReadOnlyList<double> parameters)
        {
            SundayPercentage = parameters[0];
            MondayPercentage = parameters[1];
            TuesdayPercentage = parameters[2];
            WednesdayPercentage = parameters[3];
            ThursdayPercentage = parameters[4];
            FridayPercentage = parameters[5];
            SaturdayPercentage = parameters[6];
            ChangeInspectionDatePercentage = parameters[7];
        }
        public double SundayPercentage { get; }
        public double MondayPercentage { get; }
        public double TuesdayPercentage { get;}
        public double WednesdayPercentage { get; }
        public double ThursdayPercentage { get; }
        public double FridayPercentage { get; }
        public double SaturdayPercentage { get; }
        public double ChangeInspectionDatePercentage { get; }
    }
}
