using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter.Providers
{
    public class FakeQueryProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
        {
            Type queryType = typeof (FakeQuery<>).MakeGenericType(expression.Type);
            object[] constructorArgs = new object[] {this, expression};
            return (IQueryable) Activator.CreateInstance(queryType, constructorArgs);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new FakeQuery<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return null;
        }

        public TResult Execute<TResult>(Expression expression)
        {
            // this is where a lot of analysis would normally be done,
            // along with the actual call to the web service, database, or other target platform
            return default(TResult);
        }
    }
}
