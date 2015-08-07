using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionConverter.Converters
{
    public class TypeChangeVisitor : ExpressionVisitor
    {
        private readonly Type from, to;
        private readonly Dictionary<Expression, Expression> _substitutions;
        public TypeChangeVisitor(Type from, Type to, Dictionary<Expression, Expression> substitutions)
        {
            this.from = from;
            this.to = to;
            this._substitutions = substitutions;
        }
        public override Expression Visit(Expression node)
        { // general substitutions (for example, parameter swaps)
            Expression found;
            if (_substitutions != null && _substitutions.TryGetValue(node, out found))
            {
                //var param = (ParameterExpression)node;
                //return VisitParameter(param);
                return found;
            }

            return base.Visit(node);
        }

        public static Expression<Func<TTo, bool>> Translate<TFrom, TTo>(Expression<Func<TFrom, bool>> expression)
        {
            var param = Expression.Parameter(typeof(TTo), expression.Parameters[0].Name);
            var subst = new Dictionary<Expression, Expression> { { expression.Parameters[0], param } };
            var visitor = new TypeChangeVisitor(typeof(TFrom), typeof(TTo), subst);
            return Expression.Lambda<Func<TTo, bool>>(visitor.Visit(expression.Body), param);
        }

        protected override Expression VisitMember(MemberExpression node)
        { // if we see x.Name on the old type, substitute for new type
            if (node.Member.DeclaringType == from)
            {
                return Expression.MakeMemberAccess(Visit(node.Expression),
                    to.GetMember(node.Member.Name, node.Member.MemberType,
                    BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Single());
            }
            return base.VisitMember(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Name == "a")
                return Expression.Parameter(node.Type, "something_else");
            else
                return node;
        }
    }
}
