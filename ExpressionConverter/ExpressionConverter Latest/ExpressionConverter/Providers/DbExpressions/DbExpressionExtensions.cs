using System.Linq.Expressions;

namespace ExpressionConverter.Providers.DbExpressions
{
    internal static class DbExpressionExtensions
    {
        internal static bool IsDbExpression(this ExpressionType et)
        {
            return ((int)et) >= 1000;
        }
    }
}