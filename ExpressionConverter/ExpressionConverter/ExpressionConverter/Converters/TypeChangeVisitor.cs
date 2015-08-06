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
        private readonly Dictionary<Expression, Expression> substitutions;
        public TypeChangeVisitor(Type from, Type to, Dictionary<Expression, Expression> substitutions)
        {
            this.from = from;
            this.to = to;
            this.substitutions = substitutions;
        }
        public override Expression Visit(Expression node)
        { // general substitutions (for example, parameter swaps)
            Expression found;
            if (substitutions != null && substitutions.TryGetValue(node, out found))
            {
                return found;
            }
            return base.Visit(node);
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
    }
}
