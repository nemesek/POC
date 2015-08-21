using System;
using System.Data.Common;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using ExpressionConverter.Providers.DbExpressions;
using ExpressionConverter.Providers.Projections;

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
            return Translate(expression).Item1;
        }

        public override object Execute(Expression expression)
        {
            return Execute(Translate(expression));
        }

        private object Execute(Tuple<string, LambdaExpression> commandLambdaExpression)
        {
            var projector = commandLambdaExpression.Item2.Compile();

            if (Log != null)
            {
                Log.WriteLine(commandLambdaExpression.Item1);
                Log.WriteLine();
            }

            var cmd = _connection.CreateCommand();
            cmd.CommandText = commandLambdaExpression.Item1;
            var reader = cmd.ExecuteReader();

            var elementType = TypeSystem.GetElementType(commandLambdaExpression.Item2.Body.Type);
            return Activator.CreateInstance(
                typeof(ProjectionReader<>).MakeGenericType(elementType),
                BindingFlags.Instance | BindingFlags.NonPublic, null,
                new object[] { reader, projector, this },
                null
                );
        }

        private static Tuple<string, LambdaExpression> Translate(Expression expression)
        {
            var projection = expression as ProjectionExpression;
            if (projection == null)
            {
                expression = Evaluator.PartialEval(expression);
                projection = (ProjectionExpression)new QueryBinder().Bind(expression);
            }
            var commandText = new QueryFormatter().Format(projection.Source);
            LambdaExpression projector = new ProjectionBuilder().Build(projection.Projector, projection.Source.Alias);
            return new Tuple<string, LambdaExpression>(commandText, projector);
        }
    } 
}
