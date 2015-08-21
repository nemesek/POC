using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using ExpressionConverter.Providers.DbExpressions;

namespace ExpressionConverter.Providers.Projections
{
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
