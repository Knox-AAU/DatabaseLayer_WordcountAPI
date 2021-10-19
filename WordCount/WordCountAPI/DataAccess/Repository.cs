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
            ListChanged += Save;
        }

        /// <summary>
        /// Saves the elements in the DbSet to the database.
        /// </summary>
        private void Save()
        {
            context.AddRange(InternalEntitySet);
            context.SaveChanges();
        }
    }
}