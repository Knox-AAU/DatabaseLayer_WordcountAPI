using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.Data.DataAccess
{
    public class RepositoryBase<TEntity, TKey> : ReadOnlyRepository<TEntity, TKey>, IRepositoryBase<TEntity, TKey> where TEntity : DatabaseEntityModel<TKey> where TKey : IEquatable<TKey>
    {
        public event Action ListChanged;

        public RepositoryBase(ArticleContext context) : base(context)
        {
            InternalEntitySet = new EventList<TEntity>(context.Set<TEntity>().ToList());
        }

        public RepositoryBase(DbSet<TEntity> entitySet) : base(entitySet)
        {
            InternalEntitySet = new EventList<TEntity>(entitySet.ToList());
        }

        public RepositoryBase(List<TEntity> internalEntity) : base(internalEntity)
        {
            InternalEntitySet = new EventList<TEntity>(internalEntity);
        }

        protected new EventList<TEntity> InternalEntitySet;

        /// <summary>
        /// Insert the entity into the internal list, unless already existing and invokes functions subscribed to
        /// ListChanged.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void Insert(TEntity entity)
        {
            if (Find(entity) != null)
            {
                throw new ArgumentException("Duplicate entity");
            }

            InternalEntitySet.Add(entity);
            ListChanged?.Invoke();

        }

        /// <summary>
        /// Insert the entity into the internal list, unless already existing and invokes functions subscribed to
        /// ListChanged event.
        /// </summary>
        /// <param name="entities"></param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            InternalEntitySet.AddRange(entities);
            ListChanged?.Invoke();
        }

        /// <summary>
        /// Updates existing entity in internal list and invokes functions subscribed to ListChanged event.
        /// ListChanged.
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="newEntity"></param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void Update(TKey entityKey, TEntity newEntity)
        {
            int index = InternalEntitySet.FindIndex(entity => entity.PrimaryKey.Equals(entityKey));

            if (index == -1)
            {
                throw new ArgumentException("No entity with such ID");
            }

            InternalEntitySet[index] = newEntity;
            ListChanged?.Invoke();
        }

        /// <summary>
        /// Updates existing entity in internal list and invokes functions subscribed to ListChanged event.
        /// ListChanged.
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <param name="newEntity"></param>
        /// <exception cref="ArgumentException"></exception>
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