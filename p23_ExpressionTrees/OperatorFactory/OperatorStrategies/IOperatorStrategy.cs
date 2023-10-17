using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace p23_ExpressionTrees;

public interface IOperatorStrategy<TEntity> where TEntity : class
{
    ConditionOperatorKind ConditionOperatorKind { get; }

    Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value);
    Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, List<TValue> values);
}