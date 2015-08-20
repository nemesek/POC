using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionConverter.Providers
{

    public abstract class ProjectionRow
    {
        public abstract object GetValue(int index);
        public abstract IEnumerable<TE> ExecuteSubQuery<TE>(LambdaExpression query);
    }

    internal class ProjectionBuilder : DbExpressionVisitor
    {
        ParameterExpression _row;
        string _rowAlias;
        static MethodInfo _miGetValue;
        static MethodInfo _miExecuteSubQuery;
        
        internal ProjectionBuilder()
        {
            if (_miGetValue != null) return;
            _miGetValue = typeof(ProjectionRow).GetMethod("GetValue");
            _miExecuteSubQuery = typeof(ProjectionRow).GetMethod("ExecuteSubQuery");
        }

        internal LambdaExpression Build(Expression expression, string alias)
        {
            _row = Expression.Parameter(typeof(ProjectionRow), "row");
            _rowAlias = alias;
            Expression body = Visit(expression);
            return Expression.Lambda(body, _row);
        }

        protected override Expression VisitColumn(ColumnExpression column)
        {
            if (column.Alias == _rowAlias)
            {
                return Expression.Convert(Expression.Call(_row, _miGetValue, Expression.Constant(column.Ordinal)), column.Type);
            }
            return column;
        }

        protected override Expression VisitProjection(ProjectionExpression proj)
        {
            LambdaExpression subQuery = Expression.Lambda(base.VisitProjection(proj), _row);
            Type elementType = TypeSystem.GetElementType(subQuery.Body.Type);
            MethodInfo mi = _miExecuteSubQuery.MakeGenericMethod(elementType);
            return Expression.Convert(
                Expression.Call(_row, mi, Expression.Constant(subQuery)),
                proj.Type
                );
        }
    }

    internal class ProjectionReader<T> : IEnumerable<T>
    {
        Enumerator _enumerator;

        internal ProjectionReader(DbDataReader reader, Func<ProjectionRow, T> projector, IQueryProvider provider)
        {
            _enumerator = new Enumerator(reader, projector, provider);
        }

        public IEnumerator<T> GetEnumerator()
        {
            Enumerator e = _enumerator;
            if (e == null)
            {
                throw new InvalidOperationException("Cannot enumerate more than once");
            }
            _enumerator = null;
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class Enumerator : ProjectionRow, IEnumerator<T>
        {
            readonly DbDataReader _reader;
            T _current;
            readonly Func<ProjectionRow, T> _projector;
            readonly IQueryProvider _provider;

            internal Enumerator(DbDataReader reader, Func<ProjectionRow, T> projector, IQueryProvider provider)
            {
                _reader = reader;
                _projector = projector;
                _provider = provider;
            }

            public override object GetValue(int index)
            {
                if (index < 0) throw new IndexOutOfRangeException();
                return _reader.IsDBNull(index) ? null : _reader.GetValue(index);
            }

            public override IEnumerable<TE> ExecuteSubQuery<TE>(LambdaExpression query)
            {
                var projection = (ProjectionExpression) new Replacer().Replace(query.Body, query.Parameters[0], Expression.Constant(this));
                projection = (ProjectionExpression) Evaluator.PartialEval(projection, CanEvaluateLocally);
                var result = (IEnumerable<TE>)_provider.Execute(projection);
                var list = new List<TE>(result);
                if (typeof(IQueryable<TE>).IsAssignableFrom(query.Body.Type))
                {
                    return list.AsQueryable();
                }
                return list;
            }

            private static bool CanEvaluateLocally(Expression expression)
            {
                return expression.NodeType != ExpressionType.Parameter && !expression.NodeType.IsDbExpression();
            }

            public T Current => _current;

            object IEnumerator.Current => _current;

            public bool MoveNext()
            {
                if (!_reader.Read()) return false;
                _current = _projector(this);
                return true;
            }

            public void Reset() {}

            public void Dispose()
            {
                _reader.Dispose();
            }
        }
    }
}
