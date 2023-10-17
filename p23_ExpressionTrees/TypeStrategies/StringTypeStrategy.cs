using System;
using System.Linq.Expressions;

namespace p23_ExpressionTrees;

public class StringTypeStrategy<TEntity> : ITypeStrategy<TEntity> where TEntity : class
{
    private readonly IOperatorFactory<TEntity> _operatorFactory;

    public StringTypeStrategy(IOperatorFactory<TEntity> operatorFactory)
    {
        _operatorFactory = operatorFactory;
    }
        
    public Type PropType => typeof(string);
    public Expression<Func<TEntity, bool>> GetExpression(Filter filter)
    {
        return _operatorFactory.GetNextExpression(filter.Name, filter.Value, filter.ConditionOperatorKind);
    }
}