using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace p23_ExpressionTrees;

public class EqualOperatorStrategy<TEntity> : IOperatorStrategy<TEntity> where TEntity : class
{
    public ConditionOperatorKind ConditionOperatorKind => ConditionOperatorKind.Equal;
    public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value)
    {
        ParameterExpression param = Expression.Parameter(typeof(TEntity), "x");
        MemberExpression property = Expression.Property(param, prop);
        ConstantExpression constant = Expression.Constant(value, typeof(TValue));
        BinaryExpression equal = Expression.Equal(property, constant);
            
        return Expression.Lambda<Func<TEntity, bool>>(equal, param);
    }

    public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, List<TValue> values) => null;
}