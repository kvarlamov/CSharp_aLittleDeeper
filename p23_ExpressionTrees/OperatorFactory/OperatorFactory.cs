using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace p23_ExpressionTrees;

public class OperatorFactory<TEntity> : IOperatorFactory<TEntity> where TEntity : class
{
    private readonly IEnumerable<IOperatorStrategy<TEntity>> _operatorStrategies;

    public OperatorFactory(IEnumerable<IOperatorStrategy<TEntity>> operatorStrategies)
    {
        _operatorStrategies = operatorStrategies;
    }
        
    public Expression<Func<TEntity, bool>> GetNextExpression<TValue>(string prop, TValue value, ConditionOperatorKind? conditionOperatorKind) => 
        GetTypeStrategy(conditionOperatorKind).GetNextExpression(prop, value);

    private IOperatorStrategy<TEntity> GetTypeStrategy(ConditionOperatorKind? conditionOperatorKind)
    {
        var strategy = _operatorStrategies.FirstOrDefault(s => s.ConditionOperatorKind == conditionOperatorKind);
        if (strategy == null)
            throw new NotImplementedException($"strategy with provided {nameof(conditionOperatorKind)} not found");

        return strategy;
    }
}