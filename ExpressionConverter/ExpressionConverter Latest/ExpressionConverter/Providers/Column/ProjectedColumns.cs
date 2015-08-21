using System.Collections.ObjectModel;
using System.Linq.Expressions;
using ExpressionConverter.Providers.DbExpressions;

namespace ExpressionConverter.Providers.Column
{
    internal sealed class ProjectedColumns
    {
        readonly Expression _projector;
        readonly ReadOnlyCollection<ColumnDeclaration> _columns;
        internal ProjectedColumns(Expression projector, ReadOnlyCollection<ColumnDeclaration> columns)
        {
            _projector = projector;
            _columns = columns;
        }
        internal Expression Projector => _projector;

        internal ReadOnlyCollection<ColumnDeclaration> Columns => _columns;
    }
}