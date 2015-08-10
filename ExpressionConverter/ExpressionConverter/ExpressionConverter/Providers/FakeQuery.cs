using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter.Providers
{
    public class FakeQuery<T> : IQueryable<T>
    {
        public FakeQuery() : this(new FakeQueryProvider(), null)
        {
            Expression = Expression.Constant(this); // uses this query as initial expression
        }
        public FakeQuery(IQueryProvider provider, Expression expression)
        {
            Expression = expression;
            Provider = provider;
        }

        public Expression Expression { get; }
        public Type ElementType { get; }
        public IQueryProvider Provider { get; }

        public IEnumerator<T> GetEnumerator()
        {
            // production implementations would parse the expression here, or more likely call 
            // Execute on their query provider and return the result
            return Enumerable.Empty<T>().GetEnumerator();
        }

        public override string ToString()
        {
            return "FakeQuery";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
