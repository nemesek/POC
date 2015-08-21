using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionConverter.DataAccess;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string constr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\data\Northwind.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True;MultipleActiveResultSets=true";
            const string constr = @"Data Source =.\SQLEXPRESS; Initial Catalog = Northwind; Integrated Security = True; MultipleActiveResultSets=true";
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                Northwind db = new Northwind(con);

                string city = "London";
                var query = from e in db.Employees
                            where e.City == city
                            select new
                            {
                                Name = e.LastName,
                                Orders = from o in db.Orders
                                         where o.EmployeeID == e.EmployeeID
                                         select o
                            };


                foreach (var item in query)
                {
                    Console.WriteLine("{0}", item.Name);
                    foreach (var ord in item.Orders)
                    {
                        Console.WriteLine("\tOrder: {0}", ord.OrderID);
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
