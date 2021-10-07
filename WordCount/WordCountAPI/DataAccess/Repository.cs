using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityModel
    {
        private readonly DbContext context;
        private readonly List<TEntity> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>().ToList();
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            Save();
        }
        
        public void Insert(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
            Save();
        }

        public void Update(int id, TEntity newEntity)
        {
            TEntity found = dbSet.Find(entity => entity.Id == id);
            found = newEntity;
            Save();
        }
        
        public TEntity GetById(int id)
        {
            return dbSet.Find(entity => entity.Id == id);
        }
        

        /// <summary>
        /// Deletes entity by references.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            Save();
        }
        
        /// <summary>
        /// Delete first entity satisfying the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Predicate<TEntity> predicate)
        {
            dbSet.Remove(Find(predicate));
            Save();
        }
        

        private void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<TEntity> All()
        {
            return dbSet;
        }
        

        public TEntity Find(Predicate<TEntity> predicate)
        {
            return dbSet.Find(predicate);
        }

        public IEnumerable<TEntity> FindAll(Predicate<TEntity> predicate)
        {
            return dbSet.FindAll(predicate);
        }

        public void SaveAsync()
        {
            context.SaveChangesAsync();
        }

        public virtual IEnumerable<TEntity> Get (Predicate<TEntity> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet.AsQueryable();

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
    }
}