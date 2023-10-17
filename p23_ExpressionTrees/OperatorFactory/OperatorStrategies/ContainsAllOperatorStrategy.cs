using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace p23_ExpressionTrees;

public class ContainsAllOperatorStrategy<TEntity> : IOperatorStrategy<TEntity> where TEntity : class
{
    public ConditionOperatorKind ConditionOperatorKind => ConditionOperatorKind.Contains;
    public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value) => null;

    public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, List<TValue> values)
    {
        ParameterExpression param = Expression.Parameter(typeof(TEntity), "x");
        MemberExpression property = Expression.Property(param, prop);
        BinaryExpression containsAll = null;

        foreach (var value in values)
        {
            ConstantExpression constant = Expression.Constant(value, typeof(TValue));
            BinaryExpression expr = Expression.Equal(property, constant);
            containsAll = containsAll != null ? Expression.OrElse(containsAll, expr) : expr;
        }
            
            
        return Expression.Lambda<Func<TEntity, bool>>(containsAll!, param);
    }
}