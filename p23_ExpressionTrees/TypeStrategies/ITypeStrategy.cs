using System;
using System.Linq.Expressions;

namespace p23_ExpressionTrees;

public interface ITypeStrategy<TEntity> where TEntity : class
{
    Type PropType { get; }
        
    Expression<Func<TEntity, bool>> GetExpression(Filter filter);
}