using System;
using System.Linq.Expressions;

namespace p23_ExpressionTrees;

public interface IOperatorFactory<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value, ConditionOperatorKind? conditionOperatorKind);
}