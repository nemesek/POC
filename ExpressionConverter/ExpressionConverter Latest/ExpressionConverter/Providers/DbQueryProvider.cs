using System;
using System.Data.Common;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionConverter.Providers {

    public class DbQueryProvider : QueryProvider
    {
        readonly DbConnection _connection;

        public DbQueryProvider(DbConnection connection)
        {
            _connection = connection;
        }

        public TextWriter Log { get; set; }

        public override string GetQueryText(Expression expression)
        {
            return Translate(expression).CommandText;
        }

        public override object Execute(Expression expression)
        {
            return Execute(Translate(expression));
        }

        private object Execute(TranslateResult query)
        {
            var projector = query.Projector.Compile();

            if (Log != null)
            {
                Log.WriteLine(query.CommandText);
                Log.WriteLine();
            }

            var cmd = _connection.CreateCommand();
            cmd.CommandText = query.CommandText;
            var reader = cmd.ExecuteReader();

            var elementType = TypeSystem.GetElementType(query.Projector.Body.Type);
            return Activator.CreateInstance(
                typeof(ProjectionReader<>).MakeGenericType(elementType),
                BindingFlags.Instance | BindingFlags.NonPublic, null,
                new object[] { reader, projector, this },
                null
                );
        }

        internal class TranslateResult
        {
            internal string CommandText;
            internal LambdaExpression Projector;
        }

        private TranslateResult Translate(Expression expression)
        {
            var projection = expression as ProjectionExpression;
            if (projection == null)
            {
                expression = Evaluator.PartialEval(expression);
                projection = (ProjectionExpression)new QueryBinder().Bind(expression);
            }
            var commandText = new QueryFormatter().Format(projection.Source);
            var projector = new ProjectionBuilder().Build(projection.Projector, projection.Source.Alias);
            return new TranslateResult { CommandText = commandText, Projector = projector };
        }
    } 
}
