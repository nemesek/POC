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
            Console.WriteLine(zip38655.Population);
            Console.WriteLine(zip38655.CalculatePopulationDensityForFiveMiles());
            Console.WriteLine(zip38655.CalculatePopulationDensityForTenMiles());
            Console.WriteLine("++++++++++++++++++++++++++++++++");
            var zip92602 = new Zip(92602);
            Console.WriteLine(zip92602.Population);
            Console.WriteLine(zip92602.CalculatePopulationDensityForFiveMiles());
            Console.WriteLine(zip92602.CalculatePopulationDensityForTenMiles());
            Console.WriteLine("++++++++++++++++++++++++++++++++");
            var zip75019 = new Zip(75019);
            Console.WriteLine(zip75019.Population);
            Console.WriteLine(zip75019.CalculatePopulationDensityForFiveMiles());
            Console.WriteLine(zip75019.CalculatePopulationDensityForTenMiles());
            Console.WriteLine("++++++++++++++++++++++++++++++++");
            var pastOrders = Repository.GetOrdersInspectedWitinLastNumberOfDays(15).OrderBy(o => o.InspectionDate);
            pastOrders.ToList().ForEach(o => Console.WriteLine(o.InspectionDate));
            var comparables = OrderComparableCalculator.RankComparables(new Order(1000, DateTime.Now.AddDays(1), 38655, 1, 2013, 2200));
            comparables.ToList().ForEach(c => Console.WriteLine($"YearBuilt: {c.OrderTwo.YearBuilt} SquareFeet: {c.OrderTwo.SquareFeet} EuclideanDistance : {c.Distance}"));
        }
    }
}
