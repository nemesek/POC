using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionConverter.Providers
{
    internal class QueryTranslator : ExpressionVisitor
    {
        StringBuilder _stringBuilder;
        ParameterExpression row;
        ColumnProjection projection;

        //internal string Translate(Expression expression)
        //{
        //    _stringBuilder = new StringBuilder();
        //    this.Visit(expression);
        //    return _stringBuilder.ToString();
        //}

        internal TranslateResult Translate(Expression expression)
        {
            _stringBuilder = new StringBuilder();
            this.row = Expression.Parameter(typeof(ProjectionRow), "row");
            this.Visit(expression);
            return new TranslateResult
            {
                CommandText = _stringBuilder.ToString(),
                Projector = this.projection != null ? Expression.Lambda(this.projection.Selector, this.row) : null
            };
        }

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        protected override Expression VisitMethodCall(MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Method.DeclaringType != typeof (Queryable))
            {
                throw new NotSupportedException($"The method '{methodCallExpression.Method.Name}' is not supported");
            }

            if (methodCallExpression.Method.Name == "Where")
            {
                _stringBuilder.Append("SELECT * FROM (");
                this.Visit(methodCallExpression.Arguments[0]);
                _stringBuilder.Append(") AS T WHERE ");
                var lambda = (LambdaExpression)StripQuotes(methodCallExpression.Arguments[1]);
                this.Visit(lambda.Body);
                return methodCallExpression;
            }

            if (methodCallExpression.Method.Name == "Select")
            {
                var lambda = (LambdaExpression)StripQuotes(methodCallExpression.Arguments[1]);
                var projectColumns = new ColumnProjector().ProjectColumns(lambda.Body, this.row);
                _stringBuilder.Append("SELECT ");
                _stringBuilder.Append(projectColumns.Columns);
                _stringBuilder.Append(" FROM (");
                this.Visit(methodCallExpression.Arguments[0]);
                _stringBuilder.Append(") AS T ");
                this.projection = projectColumns;
                return methodCallExpression;
            }

            throw new NotSupportedException($"The method '{methodCallExpression.Method.Name}' is not supported");

        }

        protected override Expression VisitUnary(UnaryExpression unaryExpression)
        {
            switch (unaryExpression.NodeType)
            {
                case ExpressionType.Not:
                    _stringBuilder.Append(" NOT ");
                    this.Visit(unaryExpression.Operand);
                    break;
                default:
                    throw new NotSupportedException($"The unary operator '{unaryExpression.NodeType}' is not supported");
            }
            return unaryExpression;
        }

        protected override Expression VisitBinary(BinaryExpression binaryExpression)
        {
            _stringBuilder.Append("(");
            this.Visit(binaryExpression.Left);
            switch (binaryExpression.NodeType)
            {
                case ExpressionType.And:
                    _stringBuilder.Append(" AND ");
                    break;
                case ExpressionType.Or:
                    _stringBuilder.Append(" OR");
                    break;
                case ExpressionType.Equal:
                    _stringBuilder.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    _stringBuilder.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    _stringBuilder.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _stringBuilder.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    _stringBuilder.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _stringBuilder.Append(" >= ");
                    break;
                default:
                    throw new NotSupportedException($"The binary operator '{binaryExpression.NodeType}' is not supported");
            }
            this.Visit(binaryExpression.Right);
            _stringBuilder.Append(")");
            return binaryExpression;
        }

        protected override Expression VisitConstant(ConstantExpression constantExpression)
        {
            var q = constantExpression.Value as IQueryable;
            if (q != null)
            {
                // assume constant nodes w/ IQueryables are table references
                _stringBuilder.Append("SELECT * FROM ");
                _stringBuilder.Append(q.ElementType.Name);
            }
            else if (constantExpression.Value == null)
            {
                _stringBuilder.Append("NULL");
            }
            else
            {
                switch (Type.GetTypeCode(constantExpression.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        _stringBuilder.Append(((bool)constantExpression.Value) ? 1 : 0);
                        break;
                    case TypeCode.String:
                        _stringBuilder.Append("'");
                        _stringBuilder.Append(constantExpression.Value);
                        _stringBuilder.Append("'");
                        break;
                    case TypeCode.Object:
                        throw new NotSupportedException($"The constant for '{constantExpression.Value}' is not supported");
                    default:
                        _stringBuilder.Append(constantExpression.Value);
                        break;
                }
            }
            return constantExpression;
        }

        protected override Expression VisitMember(MemberExpression memberExpression)
        {
            if (memberExpression.Expression == null || memberExpression.Expression.NodeType != ExpressionType.Parameter)
            {
                throw new NotSupportedException($"The member '{memberExpression.Member.Name}' is not supported");
            }

            _stringBuilder.Append(memberExpression.Member.Name);
            return memberExpression;
        }

        //protected override Expression VisitMemberAccess(MemberExpression memberExpression)
        //{
        //    if (memberExpression.Expression == null || memberExpression.Expression.NodeType != ExpressionType.Parameter)
        //    {
        //        throw new NotSupportedException($"The member '{memberExpression.Member.Name}' is not supported");
        //    }

        //    _stringBuilder.Append(memberExpression.Member.Name);
        //    return memberExpression;
        //}
    }
}
