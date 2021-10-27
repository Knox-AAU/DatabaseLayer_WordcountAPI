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

        public Repository(DbContext context) : base(context.Set<TEntity>())
        {
            this.context = context;
            InternalEntitySet.ItemAdded += Add;
            InternalEntitySet.ItemsAdded += Add;
            InternalEntitySet.ItemRemoved += Remove;
            InternalEntitySet.ItemsRemoved += Remove;
        }

        private void Add(IEnumerable<TEntity> obj)
        {
            context.AddRange(obj);
            Save();
        }

        private void Add(TEntity obj)
        {
            context.Add(obj);
            Save();
        }
        

        /// <summary>
        /// Saves the elements in the DbSet to the database.
        /// </summary>
        private void Save()
        {
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            context.Update(entity);
            Save();
        }
        
        public void Update(IEnumerable<TEntity> entities)
        {
            context.UpdateRange(entities);
            Save();
        }
        
        public void Remove(TEntity entities)
        {
            context.Remove(entities);
            Save();
        }
        public void Remove(IEnumerable<TEntity> entities)
        {
            context.Remove(entities);
            Save();
        }
        
        
        
    }
}