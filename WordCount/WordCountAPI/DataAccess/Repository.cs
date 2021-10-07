using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public class Repository<TEntity> : ReadOnlyRepository<TEntity> where TEntity : class
    {
        internal DbContext _context;
        internal List<TEntity> _dbSet;



        public Repository(DbContext context):base(context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>().ToList();
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

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}