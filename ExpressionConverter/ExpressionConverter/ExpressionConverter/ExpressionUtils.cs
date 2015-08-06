using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionConverter
{
    public static class ExpressionUtils
    {
        // Folder T1
        // Document T2
        // Service T3
        public static Expression<Func<T1, T3>> Combine<T1, T2, T3>(this Expression<Func<T1, T2>> outer, Expression<Func<T2, T3>> inner, bool inline)
        {
            var invoke = Expression.Invoke(inner, outer.Body);
            var body = inline ? new ExpressionRewriter().AutoInline(invoke) : invoke;
            return Expression.Lambda<Func<T1, T3>>(body, outer.Parameters);
        }

        // DomainOrder T1
        // int T2
        // OrderDto T3
        // bool T4
        public static Expression<Func<T3, T2, T4>> Combine<T1, T2, T3, T4>(this Expression<Func<T1, T2, T3>> outer, Expression<Func<T3, T2, T4>> inner, bool inline)
        {
            var invoke = Expression.Invoke(inner, outer.Body);
            var body = inline ? new ExpressionRewriter().AutoInline(invoke) : invoke;
            return Expression.Lambda<Func<T3, T2, T4>>(body, outer.Parameters);
        }
    }
}
