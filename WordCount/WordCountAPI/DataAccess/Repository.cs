using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityModel
    {
        private readonly DbContext _context;
        private readonly List<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>().ToList();
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            Save();
        }
        
        public void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            Save();
        }

        public void Update(int id, TEntity newEntity)
        {
            TEntity found = _dbSet.Find(entity => entity.Id == id);
            found = newEntity;
            Save();
        }
        
        public TEntity GetById(int id)
        {
            return _dbSet.Find(entity => entity.Id == id);
        }
        

        /// <summary>
        /// Deletes entity by references.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            Save();
        }
        
        /// <summary>
        /// Delete first entity satisfying the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Predicate<TEntity> predicate)
        {
            _dbSet.Remove(Find(predicate));
            Save();
        }
        

        private void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> All()
        {
            return _dbSet;
        }
        

        public TEntity Find(Predicate<TEntity> predicate)
        {
            return _dbSet.Find(predicate);
        }

        public IEnumerable<TEntity> FindAll(Predicate<TEntity> predicate)
        {
            return _dbSet.FindAll(predicate);
        }

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }

        public virtual IEnumerable<TEntity> Get (Predicate<TEntity> filter = null,
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
    }

    public abstract class EntityModel
    {
        public int Id { get; private set; }
    }
}