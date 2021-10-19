using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace WordCount.DataAccess
{
    public class RepositoryBase<TEntity, TKey> : ReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey> where TEntity : DatabaseEntityModel<TKey> where TKey : IEquatable<TKey>
    {
        protected List<TEntity> EntityList;
        public event Action ListChanged;

        public RepositoryBase(DbSet<TEntity> set) : base(set)
        {
            EntityList = set != null ? set.ToList() : new List<TEntity>();
        }

        public RepositoryBase(ICollection<TEntity> set) : base(set)
        {
            EntityList = set != null ? set.ToList() : new List<TEntity>();
        }


        public virtual void Insert(TEntity entity)
        {
            var a = Find(entity);
            if (a != null)
            {
                throw new ArgumentException("Duplicate entity");
            }

            EntityList.Add(entity);
            ListChanged?.Invoke();
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            EntityList.AddRange(entities);
            ListChanged?.Invoke();
        }

        public virtual void Update(TKey oldEntityId, TEntity newEntity)
        {
            int index = EntityList.FindIndex(entity => entity.PrimaryKey.Equals(oldEntityId));

            if (index == -1)
            {
                // TODO: Proper logging
                return;
            }
            
            EntityList[index] = newEntity;
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
            EntityList.Remove(entity);
            ListChanged?.Invoke();
        }

        /// <summary>
        /// Delete first entity satisfying the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Predicate<TEntity> predicate)
        {
            EntityList.Remove(Find(predicate));
            ListChanged?.Invoke();
        }
    }
}