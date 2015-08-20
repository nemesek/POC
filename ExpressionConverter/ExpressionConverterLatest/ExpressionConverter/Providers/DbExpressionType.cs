using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            return ((int) et) >= 1000;
        }
    }
}
