using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionConverter.Providers
{

    public abstract class QueryProvider : IQueryProvider
    {
        IQueryable<TS> IQueryProvider.CreateQuery<TS>(Expression expression)
        {
            return new Query<TS>(this, expression);
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            var elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(Query<>).MakeGenericType(elementType), this, expression);
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        TS IQueryProvider.Execute<TS>(Expression expression)
        {
            return (TS)Execute(expression);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return Execute(expression);
        }

        public abstract string GetQueryText(Expression expression);
        public abstract object Execute(Expression expression);
    }
}
