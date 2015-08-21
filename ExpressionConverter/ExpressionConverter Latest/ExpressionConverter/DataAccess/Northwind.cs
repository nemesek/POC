using System.Data.Common;
using System.IO;
using ExpressionConverter.Providers;

namespace ExpressionConverter.DataAccess
{
    public class Northwind
    {
        private readonly DbQueryProvider _provider;
        public Northwind(DbConnection connection)
        {
            _provider = new DbQueryProvider(connection);
            Employees = new Query<Employees>(_provider);
            Orders = new Query<Orders>(_provider);

        }

        public Query<Employees> Employees;
        public Query<Orders> Orders;

        public TextWriter Log
        {
            get { return _provider.Log; }
            set { _provider.Log = value; }
        }
    }
}
