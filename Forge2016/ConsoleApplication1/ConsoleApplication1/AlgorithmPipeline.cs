using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class AlgorithmPipeline
    {
        public static IEnumerable<Appraiser> FindAppraisersForOrderUsingDistance(Order order)
        {
            var orders = FindClosestOrders(order);
            return Enumerable.Empty<Appraiser>();
        }

        private static IEnumerable<Order> FindClosestOrders(Order order)
        {
            return Repository.GetOpenOrders();
        }

       
    }
}
