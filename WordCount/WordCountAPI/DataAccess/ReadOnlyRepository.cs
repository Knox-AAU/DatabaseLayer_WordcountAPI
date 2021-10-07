using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public class ReadOnlyRepository<TEntity> where TEntity:class
    {
        internal DbContext _context;
        internal IQueryable<TEntity> _dbSet;
        
        public ReadOnlyRepository(DbContext context)
        {
            _dbSet = context.Set<TEntity>().AsNoTracking();
        }

        
        public virtual IEnumerable<TEntity> Get(
            Predicate<TEntity> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(e => filter(e));
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            throw new NotImplementedException();
        }

       
      
    }
}