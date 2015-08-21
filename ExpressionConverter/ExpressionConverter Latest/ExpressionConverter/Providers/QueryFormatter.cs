using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using ExpressionConverter.Providers.DbExpressions;

namespace ExpressionConverter.Providers
{
    internal class QueryFormatter : DbExpressionVisitor
    {
        StringBuilder _sb;
        int _depth;

        internal string Format(Expression expression)
        {
            _sb = new StringBuilder();
            Visit(expression);
            return _sb.ToString();
        }

        protected enum Identation
        {
            Same,
            Inner,
            Outer
        }

        internal int IdentationWidth { get; set; } = 2;

        private void AppendNewLine(Identation style)
        {
            _sb.AppendLine();
            switch (style)
            {
                case Identation.Inner:
                    _depth++;
                    break;
                case Identation.Outer:
                    _depth--;
                    Debug.Assert(_depth >= 0);
                    break;
            }
            for (int i = 0, n = _depth * IdentationWidth; i < n; i++) {
                _sb.Append(" ");
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            throw new NotSupportedException($"The method '{m.Method.Name}' is not supported");
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    _sb.Append(" NOT ");
                    Visit(u.Operand);
                    break;
                default:
                    throw new NotSupportedException($"The unary operator '{u.NodeType}' is not supported");
            }
            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            _sb.Append("(");
            Visit(b.Left);
            switch (b.NodeType) {
                case ExpressionType.And:
                    _sb.Append(" AND ");
                    break;
                case ExpressionType.Or:
                    _sb.Append(" OR");
                    break;
                case ExpressionType.Equal:
                    _sb.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    _sb.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    _sb.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _sb.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    _sb.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _sb.Append(" >= ");
                    break;
                default:
                    throw new NotSupportedException($"The binary operator '{b.NodeType}' is not supported");
            }
            Visit(b.Right);
            _sb.Append(")");
            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            if (c.Value == null)
            {
                _sb.Append("NULL");
            }
            else
            {
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        _sb.Append(((bool)c.Value) ? 1 : 0);
                        break;
                    case TypeCode.String:
                        _sb.Append("'");
                        _sb.Append(c.Value);
                        _sb.Append("'");
                        break;
                    case TypeCode.Object:
                        throw new NotSupportedException($"The constant for '{c.Value}' is not supported");
                    default:
                        _sb.Append(c.Value);
                        break;
                }
            }
            return c;
        }

        protected override Expression VisitColumn(ColumnExpression column)
        {
            if (!string.IsNullOrEmpty(column.Alias))
            {
                _sb.Append(column.Alias);
                _sb.Append(".");
            }
            _sb.Append(column.Name);
            return column;
        }

        protected override Expression VisitSelect(SelectExpression select)
        {
            _sb.Append("SELECT ");
            for (int i = 0, n = select.Columns.Count; i < n; i++)
            {
                ColumnDeclaration column = select.Columns[i];
                if (i > 0)
                {
                    _sb.Append(", ");
                }
                var c = Visit(column.Expression) as ColumnExpression;
                if (c != null && c.Name == @select.Columns[i].Name) continue;
                _sb.Append(" AS ");
                _sb.Append(column.Name);
            }
            if (select.From != null)
            {
                AppendNewLine(Identation.Same);
                _sb.Append("FROM ");
                VisitSource(select.From);
            }
            if (@select.Where == null) return @select;
            AppendNewLine(Identation.Same);
            _sb.Append("WHERE ");
            Visit(@select.Where);
            return select;
        }

        protected override Expression VisitSource(Expression source)
        {
            switch ((DbExpressionType)source.NodeType)
            {
                case DbExpressionType.Table:
                    var table = (TableExpression)source;
                    _sb.Append(table.Name);
                    _sb.Append(" AS ");
                    _sb.Append(table.Alias);
                    break;
                case DbExpressionType.Select:
                    var select = (SelectExpression)source;
                    _sb.Append("(");
                    AppendNewLine(Identation.Inner);
                    Visit(select);
                    AppendNewLine(Identation.Outer);
                    _sb.Append(")");
                    _sb.Append(" AS ");
                    _sb.Append(select.Alias);
                    break;
                default:
                    throw new InvalidOperationException("Select source is not valid type");
            }
            return source;
        }
    }
}
