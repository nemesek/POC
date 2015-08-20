using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionConverter.Converters
{
    public class ExpressionRewriter
    {
        private bool _isLocked, _inline;
        private readonly Dictionary<Expression, Expression> _subst;
        public ExpressionRewriter()
        {
            _subst = new Dictionary<Expression, Expression>();
        }
        public ExpressionRewriter Inline()
        {
            CheckLocked();
            _inline = true;
            return this;
        }
        public Expression Apply(Expression expression)
        {
            _isLocked = true;
            return Walk(expression) ?? expression;
        }

        public ExpressionRewriter Subst(Expression from, Expression to)
        {
            CheckLocked();
            _subst.Add(from, to);
            return this;
        }

        internal Expression AutoInline(InvocationExpression expression)
        {
            _isLocked = true;
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            var lambda = (LambdaExpression)expression.Expression;
            var childScope = new ExpressionRewriter(this);
            var lambdaParams = lambda.Parameters;
            var invokeArgs = expression.Arguments;
            if (lambdaParams.Count != invokeArgs.Count) throw new InvalidOperationException("Lambda/invoke mismatch");
            for (var i = 0; i < lambdaParams.Count; i++)
            {
                childScope.Subst(lambdaParams[i], invokeArgs[i]);
            }
            return childScope.Apply(lambda.Body);
        }

        private ExpressionRewriter(ExpressionRewriter parent)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            _subst = new Dictionary<Expression, Expression>(parent._subst);
            _inline = parent._inline;
        }
        
        
        private void CheckLocked()
        {
            if (_isLocked) throw new InvalidOperationException("You cannot alter the rewriter after Apply has been called");

        }

        private static IEnumerable<Expression> CoalesceTerms(IEnumerable<Expression> sourceWithNulls, IEnumerable<Expression> replacements)
        {
            if (sourceWithNulls == null || replacements == null) yield break;
            using (var left = sourceWithNulls.GetEnumerator())
            using (var right = replacements.GetEnumerator())
            {
                while (left.MoveNext() && right.MoveNext())
                {
                    yield return left.Current ?? right.Current;
                }
            }
        }

        private Expression[] Walk(IEnumerable<Expression> expressions)
        {
            return expressions?.Select(Walk).ToArray();
        }
        private static bool HasValue(Expression[] expressions)
        {
            return expressions != null && expressions.Any(expr => expr != null);
        }

        // returns null if no need to rewrite that branch, otherwise
        // returns a re-written branch
        private Expression Walk(Expression expression)
        {
            if (expression == null) return null;
            Expression tmp;
            if (_subst.TryGetValue(expression, out tmp)) return tmp;
            switch (expression.NodeType)
            {
                case ExpressionType.Constant:
                case ExpressionType.Parameter:
                    {
                        return expression; // never a need to rewrite if not already matched
                    }
                case ExpressionType.MemberAccess:
                    {
                        var me = (MemberExpression)expression;
                        var target = Walk(me.Expression);
                        return target == null ? null : Expression.MakeMemberAccess(target, me.Member);
                    }
                case ExpressionType.Add:
                case ExpressionType.Divide:
                case ExpressionType.Multiply:
                case ExpressionType.Subtract:
                case ExpressionType.AddChecked:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.SubtractChecked:
                case ExpressionType.And:
                case ExpressionType.Or:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                case ExpressionType.Power:
                case ExpressionType.Modulo:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.LeftShift:
                case ExpressionType.RightShift:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                    {
                        var binExp = (BinaryExpression)expression;
                        Expression left = Walk(binExp.Left), right = Walk(binExp.Right);
                        return (left == null && right == null) ? null : Expression.MakeBinary(
                            binExp.NodeType, left ?? binExp.Left, right ?? binExp.Right, binExp.IsLiftedToNull,
                            binExp.Method, binExp.Conversion);
                    }
                case ExpressionType.Not:
                case ExpressionType.UnaryPlus:
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.TypeAs:
                case ExpressionType.ArrayLength:
                    {
                        var unExp = (UnaryExpression)expression;
                        var operand = Walk(unExp.Operand);
                        return operand == null ? null : Expression.MakeUnary(unExp.NodeType, operand,
                            unExp.Type, unExp.Method);
                    }
                case ExpressionType.Conditional:
                    {
                        var ce = (ConditionalExpression)expression;
                        Expression test = Walk(ce.Test), ifTrue = Walk(ce.IfTrue), ifFalse = Walk(ce.IfFalse);
                        if (test == null && ifTrue == null && ifFalse == null) return null;
                        return Expression.Condition(test ?? ce.Test, ifTrue ?? ce.IfTrue, ifFalse ?? ce.IfFalse);
                    }
                case ExpressionType.Call:
                    {
                        var mce = (MethodCallExpression)expression;
                        var instance = Walk(mce.Object);
                        var args = Walk(mce.Arguments);
                        if (instance == null && !HasValue(args)) return null;
                        return Expression.Call(instance, mce.Method, CoalesceTerms(args, mce.Arguments));
                    }
                case ExpressionType.TypeIs:
                    {
                        var tbe = (TypeBinaryExpression)expression;
                        tmp = Walk(tbe.Expression);
                        return tmp == null ? null : Expression.TypeIs(tmp, tbe.TypeOperand);
                    }
                case ExpressionType.New:
                    {
                        var ne = (NewExpression)expression;
                        var args = Walk(ne.Arguments);
                        if (HasValue(args)) return null;
                        return ne.Members == null ? Expression.New(ne.Constructor, CoalesceTerms(args, ne.Arguments))
                            : Expression.New(ne.Constructor, CoalesceTerms(args, ne.Arguments), ne.Members);
                    }
                case ExpressionType.ListInit:
                    {
                        var lie = (ListInitExpression)expression;
                        var ctor = (NewExpression)Walk(lie.NewExpression);
                        var inits = lie.Initializers.Select(init => new
                        {
                            Original = init,
                            NewArgs = Walk(init.Arguments)
                        }).ToArray();
                        if (ctor == null && !inits.Any(init => HasValue(init.NewArgs))) return null;
                        var initArr = inits.Select(init => Expression.ElementInit(
                                init.Original.AddMethod, CoalesceTerms(init.NewArgs, init.Original.Arguments))).ToArray();
                        return Expression.ListInit(ctor ?? lie.NewExpression, initArr);

                    }
                case ExpressionType.NewArrayBounds:
                case ExpressionType.NewArrayInit:
                /* not quite right... leave as not-implemented for now
                {
                    NewArrayExpression nae = (NewArrayExpression)expression;
                    Expression[] expr = Walk(nae.Expressions);
                    if (!HasValue(expr)) return null;
                    return expression.NodeType == ExpressionType.NewArrayBounds
                        ? Expression.NewArrayBounds(nae.Type, CoalesceTerms(expr, nae.Expressions))
                        : Expression.NewArrayInit(nae.Type, CoalesceTerms(expr, nae.Expressions));
                }*/
                case ExpressionType.Invoke:
                case ExpressionType.Lambda:
                case ExpressionType.MemberInit:
                case ExpressionType.Quote:
                    throw new NotImplementedException("Not implemented: " + expression.NodeType);
                default:
                    throw new NotSupportedException("Not supported: " + expression.NodeType);
            }

        }
    }
}
