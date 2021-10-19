using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public class RepositoryBase<TEntity, TKey> : ReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey> where TEntity : DatabaseEntityModel<TKey> where TKey : IEquatable<TKey>
    {
        public event Action ListChanged;
        
        public RepositoryBase(DbContext context) : base(context) { }
        public RepositoryBase(DbSet<TEntity> entitySet) : base(entitySet) { }
        public RepositoryBase(List<TEntity> internalEntity) : base(internalEntity) { }
        
        public virtual void Insert(TEntity entity)
        {
            if (Find(entity) != null)
            {
                throw new ArgumentException("Duplicate entity");
            }

            InternalEntitySet.Add(entity);
            ListChanged?.Invoke();
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            InternalEntitySet.AddRange(entities);
            ListChanged?.Invoke();
        }

        public virtual void Update(TKey oldEntityId, TEntity newEntity)
        {
            int index = InternalEntitySet.FindIndex(entity => entity.PrimaryKey.Equals(oldEntityId));

            if (index == -1)
            {
                throw new ArgumentException("No entity with such ID");
            }
            
            InternalEntitySet[index] = newEntity;
            ListChanged?.Invoke();
        }

        public virtual void Update(TEntity oldEntity, TEntity newEntity)
        {
            Update(oldEntity.PrimaryKey, newEntity);
        }

        /// <summary>
        /// Deletes entity by references.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            InternalEntitySet.Remove(entity);
            ListChanged?.Invoke();
        }

        /// <summary>
        /// Delete first entity satisfying the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Predicate<TEntity> predicate)
        {
            InternalEntitySet.Remove(Find(predicate));
            ListChanged?.Invoke();
        }
    }
}