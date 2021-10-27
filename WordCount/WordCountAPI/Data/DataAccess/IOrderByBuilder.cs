using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCount.DataAccess
{
    public interface IOrderByBuilder<TEntity>
    {
        IOrderByBuilder<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IEnumerable<TEntity> Execute();
    }
}