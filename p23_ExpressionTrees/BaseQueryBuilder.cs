using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace p23_ExpressionTrees
{
    public abstract class BaseQueryBuilder<TEntity> : IQueryBuilder<TEntity> 
        where TEntity : class
    {
        public List<TEntity> ApplyFilters(IEnumerable<TEntity> entities, List<Filter> filters)
        {
            var initialExpression = GetInitialExpression();
            var queryExpression = GetExpression(initialExpression, filters);

            return entities.Where(queryExpression.Compile()).ToList();
        }

        private Expression<Func<TEntity,bool>> GetExpression(Expression<Func<TEntity,bool>> currentExpression, List<Filter> filters)
        {
            foreach (var filter in filters)
            {
                if (string.IsNullOrEmpty(filter.Name) || string.IsNullOrEmpty(filter.Value))
                    continue;

                var nextExpression = filter.ConditionOperatorKind == null
                    ? GetExpression(GetInitialExpression(), filter.ChildFilters)
                    : GetNextExpression(filter);
                
                if (nextExpression == null)
                    continue;

                currentExpression = CreateNextExpression(currentExpression, nextExpression, filter.ConditionKind);
            }

            return currentExpression;
        }

        protected abstract Expression<Func<TEntity, bool>> GetNextExpression(Filter filter);
        
        private Expression<Func<TEntity, bool>> CreateNextExpression(Expression<Func<TEntity,bool>> currentExpression, Expression<Func<TEntity,bool>> nextExpression, ConditionKind filterConditionKind)
        {
            return filterConditionKind == ConditionKind.Or ? currentExpression.OrElse(nextExpression) : currentExpression.AndAlso(nextExpression);
        }
        
        private Expression<Func<TEntity,bool>> GetInitialExpression() => parameter => true;
    }
}