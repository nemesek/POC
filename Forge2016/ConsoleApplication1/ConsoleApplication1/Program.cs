using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var zip38655 = new Zip(38655);
            Console.WriteLine($"Population of 38655 {zip38655.Population}");
            Console.WriteLine($"Density Within 5 miles {zip38655.CalculatePopulationDensityForFiveMiles()}");
            Console.WriteLine($"Density Within 10 miles {zip38655.CalculatePopulationDensityForTenMiles()}");
            Console.WriteLine("++++++++++++++++++++++++++++++++");
            var zip92602 = new Zip(92602);
            Console.WriteLine($"Population of 92602 {zip92602.Population}");
            Console.WriteLine($"Density Within 5 miles {zip92602.CalculatePopulationDensityForFiveMiles()}");
            Console.WriteLine($"Density Within 10 miles {zip92602.CalculatePopulationDensityForTenMiles()}");
            Console.WriteLine("++++++++++++++++++++++++++++++++");
            var zip75019 = new Zip(75019);
            Console.WriteLine($"Population of 75019 {zip75019.Population}");
            Console.WriteLine($"Density Within 5 miles {zip75019.CalculatePopulationDensityForFiveMiles()}");
            Console.WriteLine($"Density Within 10 miles {zip75019.CalculatePopulationDensityForTenMiles()}");
            Console.WriteLine("++++++++++++++++++++++++++++++++");
            //var pastOrders = Repository.GetOrdersInspectedWitinLastNumberOfDays(15).OrderBy(o => o.InspectionDate);
            //pastOrders.ToList().ForEach(o => Console.WriteLine(o.InspectionDate));
            var comparables = OrderPropertyComparableCalculator.RankPropertyComparables(new Order(1000, DateTime.Now.AddDays(1), 38655, 1, 2013, 2200));
            comparables.ToList().ForEach(c => Console.WriteLine($"YearBuilt: {c.OrderTwo.YearBuilt} SquareFeet: {c.OrderTwo.SquareFeet} EuclideanDistance : {c.Distance}"));
            Console.WriteLine("++++++++++++++++++++++++++++++++");
            var closestOrders = OrderScheduleAndDistanceComparableCalculator.RankOrderScheduleAndDistanceComparables(new Order(1000, DateTime.Now.AddDays(5), 38655, 1, 2013, 2200));
            closestOrders.ToList().ForEach(c => Console.WriteLine($"ID: {c.OrderTwo.OrderId} InspectionDate: {c.OrderTwo.InspectionDate} ZipCode: {c.OrderTwo.ZipCode} EuclideanDistance : {c.Distance}"));
        }
    }
}
