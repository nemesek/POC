using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionConverter.Providers;

namespace ExpressionConverter.DataAccess
{
    public class Northwind
    {
        public Northwind(DbConnection connection)
        {
            QueryProvider provider = new DbQueryProvider(connection);
            this.Employees = new Query<Employees>(provider);

        }

        public Query<Employees> Employees;
    }
}
