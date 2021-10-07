using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCount.DataAccess
{
    public sealed class GetArranger<TEntity> : IFilterBuilder<TEntity>, IOrderByBuilder<TEntity>
    {
        private IQueryable<TEntity> query;

        public GetArranger(IQueryable<TEntity> dbSet)
        {
            query = dbSet;
        }

        public IOrderByBuilder<TEntity> Filter(Predicate<TEntity> filter)
        {
            query = query.Where(e => filter(e));
            return this;
        }

        public IOrderByBuilder<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            query = orderBy(query);
            return this;
        }

        public IEnumerable<TEntity> Execute()
        {
            return query;
        }
    }
}