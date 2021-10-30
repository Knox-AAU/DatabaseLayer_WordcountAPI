
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WordCount.Data;


namespace WordCount.DataAccess
{
    public sealed class Repository<TEntity, TKey> 
        : RepositoryBase<TEntity, TKey>
        where TEntity : DatabaseEntityModel<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly ArticleContext context;
        private readonly DbSet<TEntity> dbSet;
        
        public Repository(ArticleContext context) : base(context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
            InternalEntitySet.ItemAdded += Add;
            InternalEntitySet.ItemsAdded += Add;
            InternalEntitySet.ItemRemoved += Remove;
            InternalEntitySet.ItemsRemoved += Remove;
        }

        private void Add(IEnumerable<TEntity> obj)
        {
            dbSet.AddRange(obj);
            Save();
        }

        private void Add(TEntity obj)
        {
            dbSet.Add(obj);
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
            dbSet.Update(entity);
            Save();
        }
        
        public void Update(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            Save();
        }
        
        public void Remove(TEntity entities)
        {
            dbSet.Remove(entities);
            Save();
        }
        public void Remove(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            Save();
        }
    }
}