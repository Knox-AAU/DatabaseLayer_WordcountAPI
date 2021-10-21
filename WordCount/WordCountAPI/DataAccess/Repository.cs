using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public sealed class Repository<TEntity, TKey> 
        : RepositoryBase<TEntity, TKey>
        where TEntity : DatabaseEntityModel<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly DbContext context;
        public event Action ElementsSaved;
        public event Action ElementsRemoved;
        public event Action ElementsUpdated;

        public Repository(DbContext context) : base(context.Set<TEntity>())
        {
            this.context = context;
            ListChanged += Save;
        }

        /// <summary>
        /// Saves the elements in the DbSet to the database.
        /// </summary>
        private void Save()
        {
            context.AddRange(InternalEntitySet);
            ElementsSaved?.Invoke();
        }

        public void Update(TEntity entity)
        {
            context.Update(entity);
            ElementsUpdated?.Invoke();
            Save();
        }
        
        public void Update(IEnumerable<TEntity> entities)
        {
            context.UpdateRange(entities);
            ElementsUpdated?.Invoke();
            Save();
        }
        
        public void Remove(TEntity entities)
        {
            context.Remove(entities);
            ElementsRemoved?.Invoke();
            Save();
        }
        public void Remove(IEnumerable<TEntity> entities)
        {
            context.Remove(entities);
            ElementsRemoved?.Invoke();
            Save();
        }
        
    }
}