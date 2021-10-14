using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public sealed class Repository<TEntity, TKey> 
        : ReadOnlyRepository<TEntity, TKey> where TEntity : DatabaseEntityModel<TKey> where TKey : IEquatable<TKey>, 
        IRepository<TEntity, TKey>
    {
        private readonly DbContext context;
        private readonly List<TEntity> entityList;

        public Repository(DbContext context) : base(context)
        {
            this.context = context;
            entityList = context.Set<TEntity>().ToList();
        }

        public void Insert(TEntity entity)
        {
            entityList.Add(entity);
            Save();
        }
        
        public void Insert(IEnumerable<TEntity> entities)
        {
            entityList.AddRange(entities);
            Save();
        }
        
        public void Update(TKey oldEntityId, TEntity newEntity)
        {
            int index = entityList.FindIndex(entity => entity.PrimaryKey.Equals(oldEntityId));

            if (index == -1)
            {
                // TODO: Proper logging
                Console.WriteLine($"No entity found with id {oldEntityId}.");
                return;
            }
            
            entityList[index] = newEntity;
            Save();
        }

        public void Update(TEntity oldEntity, TEntity newEntity)
        {
            Update(oldEntity.PrimaryKey, newEntity);
        }
   
        
        /// <summary>
        /// Deletes entity by references.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            entityList.Remove(entity);
            Save();
        }
        
        /// <summary>
        /// Delete first entity satisfying the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Predicate<TEntity> predicate)
        {
            entityList.Remove(Find(predicate));
            Save();
        }
        
        private void Save()
        {
            context.SaveChanges();
        }
    }
}