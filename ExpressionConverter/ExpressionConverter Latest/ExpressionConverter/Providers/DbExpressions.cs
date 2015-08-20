using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionConverter.Providers
{

    internal enum DbExpressionType
    {
        Table = 1000, // make sure these don't overlap with ExpressionType
        Column,
        Select,
        Projection
    }

    internal static class DbExpressionExtensions
    {
        internal static bool IsDbExpression(this ExpressionType et)
        {
            return ((int)et) >= 1000;
        }
    }

    internal class TableExpression : Expression
    {
        readonly string _alias;
        readonly string _name;
        internal TableExpression(Type type, string alias, string name): base((ExpressionType)DbExpressionType.Table, type)
        {
            _alias = alias;
            _name = name;
        }
        internal string Alias => _alias;

        internal string Name => _name;
    }

    internal class ColumnExpression : Expression
    {
        string _alias;
        string _name;
        int _ordinal;
        internal ColumnExpression(Type type, string alias, string name, int ordinal): base((ExpressionType)DbExpressionType.Column, type)
        {
            _alias = alias;
            _name = name;
            _ordinal = ordinal;
        }
        internal string Alias => _alias;

        internal string Name => _name;

        internal int Ordinal => _ordinal;
    }

    internal class ColumnDeclaration
    {
        readonly string _name;
        readonly Expression _expression;
        internal ColumnDeclaration(string name, Expression expression)
        {
            _name = name;
            _expression = expression;
        }
        internal string Name => _name;

        internal Expression Expression => _expression;
    }

    internal class SelectExpression : Expression
    {
        readonly string _alias;
        readonly ReadOnlyCollection<ColumnDeclaration> _columns;
        readonly Expression _from;
        readonly Expression _where;
        internal SelectExpression(Type type, string alias, IEnumerable<ColumnDeclaration> columns, Expression from, Expression where): base((ExpressionType)DbExpressionType.Select, type)
        {
            _alias = alias;
            _columns = columns as ReadOnlyCollection<ColumnDeclaration> ?? new List<ColumnDeclaration>(columns).AsReadOnly();
            _from = from;
            _where = where;
        }
        internal string Alias => _alias;

        internal ReadOnlyCollection<ColumnDeclaration> Columns => _columns;

        internal Expression From => _from;

        internal Expression Where => _where;
    }

    internal class ProjectionExpression : Expression
    {
        readonly SelectExpression _source;
        readonly Expression _projector;
        internal ProjectionExpression(SelectExpression source, Expression projector) : base((ExpressionType)DbExpressionType.Projection, source.Type)
        {
            _source = source;
            _projector = projector;
        }
        internal SelectExpression Source => _source;

        internal Expression Projector => _projector;
    }

    internal class DbExpressionVisitor : ExpressionVisitor
    {
        protected override Expression Visit(Expression exp)
        {
            if (exp == null)
            {
                return null;
            }
            switch ((DbExpressionType)exp.NodeType) {
                case DbExpressionType.Table:
                    return VisitTable((TableExpression)exp);
                case DbExpressionType.Column:
                    return VisitColumn((ColumnExpression)exp);
                case DbExpressionType.Select:
                    return VisitSelect((SelectExpression)exp);
                case DbExpressionType.Projection:
                    return VisitProjection((ProjectionExpression)exp);
                default:
                    return base.Visit(exp);
            }
        }
        protected virtual Expression VisitTable(TableExpression table)
        {
            return table;
        }
        protected virtual Expression VisitColumn(ColumnExpression column)

        {
            return column;
        }
        protected virtual Expression VisitSelect(SelectExpression select)
        {
            var from = VisitSource(select.From);
            var where = Visit(select.Where);
            var columns = VisitColumnDeclarations(select.Columns);
            if (from != select.From || where != select.Where || columns != select.Columns)
            {
                return new SelectExpression(select.Type, select.Alias, columns, from, where);
            }
            return select;
        }
        protected virtual Expression VisitSource(Expression source)
        {
            return Visit(source);
        }
        protected virtual Expression VisitProjection(ProjectionExpression proj)
        {
            var source = (SelectExpression)Visit(proj.Source);
            var projector = Visit(proj.Projector);
            if (source != proj.Source || projector != proj.Projector)
            {
                return new ProjectionExpression(source, projector);
            }
            return proj;
        }
        protected ReadOnlyCollection<ColumnDeclaration> VisitColumnDeclarations(ReadOnlyCollection<ColumnDeclaration> columns)
        {
            List<ColumnDeclaration> alternate = null;
            for (int i = 0, n = columns.Count; i < n; i++)
            {
                var column = columns[i];
                var e = Visit(column.Expression);
                if (alternate == null && e != column.Expression)
                {
                    alternate = columns.Take(i).ToList();
                }
                alternate?.Add(new ColumnDeclaration(column.Name, e));
            }
            return alternate?.AsReadOnly() ?? columns;
        }
    }
}
