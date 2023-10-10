using System.Collections.Generic;

namespace p23_ExpressionTrees
{
    public interface IQueryBuilder<TEntity>
        where TEntity: class
    {
        List<TEntity> ApplyFilters(IEnumerable<TEntity> entities, List<Filter> filters);
    }
}